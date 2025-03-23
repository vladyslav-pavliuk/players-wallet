using PlayersWallet.Api.Models;
using PlayersWallet.Api.Repositories.Interfaces;

namespace PlayersWallet.Api.Repositories;

/// <summary>
/// Provides an in-memory implementation of the <see cref="ITransactionRepository"/> interface.
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly Dictionary<Guid, Transaction> _transactions = new();
    private readonly Dictionary<Guid, List<Transaction>> _playerTransactions = new();

    /// <inheritdoc />
    public Task<Transaction?> GetTransactionAsync(Guid transactionId)
    {
        _transactions.TryGetValue(transactionId, out var transaction);
        return Task.FromResult(transaction);
    }

    /// <inheritdoc />
    public Task<bool> AddTransactionAsync(Transaction transaction)
    {
        if (!_transactions.TryAdd(transaction.Id, transaction))
            return Task.FromResult(false);

        if (!_playerTransactions.ContainsKey(transaction.PlayerId))
            _playerTransactions[transaction.PlayerId] = [];

        _playerTransactions[transaction.PlayerId].Add(transaction);
        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Transaction>> GetTransactionsByPlayerIdAsync(Guid playerId)
    {
        _playerTransactions.TryGetValue(playerId, out var transactions);
        return Task.FromResult(transactions?.AsEnumerable() ?? []);
    }
}