using PlayersWallet.Api.Enums;
using PlayersWallet.Api.Models;

namespace PlayersWallet.Api.Services.Interfaces;

/// <summary>
/// Defines the contract for managing player wallets and transactions.
/// </summary>
public interface IWalletService
{
    /// <summary>
    /// Registers a new player in the system with an initial balance of 0.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>True if the player was successfully registered; otherwise, false.</returns>
    Task<bool> RegisterPlayerAsync(Guid playerId);

    /// <summary>
    /// Retrieves the current balance of a player's wallet.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>The current balance of the wallet.</returns>
    Task<decimal> GetBalanceAsync(Guid playerId);

    /// <summary>
    /// Credits a transaction to a player's wallet.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <param name="transactionId">The unique identifier of the transaction.</param>
    /// <param name="amount">The amount of money involved in the transaction.</param>
    /// <param name="type">The type of the transaction (Deposit, Stake, or Win).</param>
    /// <returns>True if the transaction was accepted; otherwise, false.</returns>
    Task<bool> CreditTransactionAsync(Guid playerId, Guid transactionId, decimal amount, TransactionType type);

    /// <summary>
    /// Retrieves all transactions associated with a specific player.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>A list of transactions for the player.</returns>
    Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid playerId);
}