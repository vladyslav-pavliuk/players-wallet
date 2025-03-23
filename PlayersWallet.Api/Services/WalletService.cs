using System.Globalization;
using Microsoft.Extensions.Caching.Distributed;
using PlayersWallet.Api.Enums;
using PlayersWallet.Api.Exceptions;
using PlayersWallet.Api.Models;
using PlayersWallet.Api.Repositories.Interfaces;
using PlayersWallet.Api.Services.Interfaces;

namespace PlayersWallet.Api.Services;

/// <summary>
///     Provides business logic for managing player wallets and transactions.
/// </summary>
public class WalletService : IWalletService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<WalletService> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WalletService" /> class.
    /// </summary>
    /// <param name="playerRepository">The player repository.</param>
    /// <param name="walletRepository">The wallet repository.</param>
    /// <param name="transactionRepository">The transaction repository.</param>
    /// <param name="cache">The distributed cache (Redis).</param>
    /// <param name="logger">The logger.</param>
    public WalletService(
        IPlayerRepository playerRepository,
        IWalletRepository walletRepository,
        ITransactionRepository transactionRepository,
        IDistributedCache cache,
        ILogger<WalletService> logger)
    {
        _playerRepository = playerRepository;
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _cache = cache;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<bool> RegisterPlayerAsync(Guid playerId)
    {
        _logger.LogInformation($"Attempting to register player with ID: {playerId}.");

        var player = new Player { Id = playerId };
        var result = await _playerRepository.RegisterPlayerAsync(player);

        if (!result)
        {
            _logger.LogWarning($"Player {playerId} is already registered.");
            throw new PlayerAlreadyRegisteredException(playerId);
        }
        
        var wallet = new Wallet { PlayerId = playerId, Balance = 0 };
        await _walletRepository.UpdateWalletAsync(wallet);

        _logger.LogInformation($"Player {playerId} registered successfully.");
        return true;
    }

    /// <inheritdoc />
    public async Task<decimal> GetBalanceAsync(Guid playerId)
    {
        _logger.LogInformation($"Fetching balance for player {playerId}.");

        var cacheKey = $"Balance_{playerId}";

        try
        {
            var cachedBalance = await _cache.GetStringAsync(cacheKey);

            if (cachedBalance != null)
            {
                _logger.LogInformation($"Cache hit for player {playerId}. Balance retrieved from cache.");
                return decimal.Parse(cachedBalance);
            }

            _logger.LogInformation($"Cache miss for player {playerId}. Fetching balance from database.");

            var wallet = await _walletRepository.GetWalletAsync(playerId) ?? new Wallet { PlayerId = playerId, Balance = 0 };

            await _cache.SetStringAsync(cacheKey, wallet.Balance.ToString(CultureInfo.InvariantCulture), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            _logger.LogInformation($"Balance for player {playerId} cached successfully.");
            return wallet.Balance;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving balance for player {playerId}.");
            throw new RedisCacheException("Failed to retrieve balance from cache.", ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> CreditTransactionAsync(Guid playerId, Guid transactionId, decimal amount, TransactionType type)
    {
        _logger.LogInformation($"Processing transaction {transactionId} for player {playerId}.");

        var existingTransaction = await _transactionRepository.GetTransactionAsync(transactionId);
        if (existingTransaction != null)
        {
            _logger.LogInformation($"Transaction {transactionId} already processed. Returning existing status: {existingTransaction.IsAccepted}.");
            throw new TransactionAlreadyProcessedException(transactionId);
        }

        var wallet = await _walletRepository.GetWalletAsync(playerId) ?? new Wallet { PlayerId = playerId, Balance = 0 };

        if (type == TransactionType.Stake && wallet.Balance < amount)
        {
            _logger.LogWarning($"Transaction {transactionId} rejected: Insufficient balance for player {playerId}.");
            throw new InsufficientBalanceException(playerId, amount);
        }

        decimal newBalance = type switch
        {
            TransactionType.Deposit => wallet.Balance + amount,
            TransactionType.Stake => wallet.Balance - amount,
            TransactionType.Win => wallet.Balance + amount,
            _ => wallet.Balance
        };

        wallet.Balance = newBalance;
        await _walletRepository.UpdateWalletAsync(wallet);

        var cacheKey = $"Balance_{playerId}";
        await _cache.RemoveAsync(cacheKey);
        _logger.LogInformation($"Invalidated cache for player {playerId}.");

        var transaction = new Transaction
        {
            Id = transactionId,
            PlayerId = playerId,
            Amount = amount,
            Type = type,
            IsAccepted = true
        };

        await _transactionRepository.AddTransactionAsync(transaction);
        _logger.LogInformation($"Transaction {transactionId} processed successfully for player {playerId}.");

        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid playerId)
    {
        _logger.LogInformation($"Fetching transactions for player {playerId}.");

        var transactions = await _transactionRepository.GetTransactionsByPlayerIdAsync(playerId);

        var transactionsAsync = transactions.ToList();
        _logger.LogInformation(!transactionsAsync.Any()
            ? $"No transactions found for player {playerId}."
            : $"Retrieved {transactionsAsync.Count} transactions for player {playerId}.");

        return transactionsAsync;
    }
}