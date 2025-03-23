using PlayersWallet.Api.Models;

namespace PlayersWallet.Api.Repositories.Interfaces;

/// <summary>
/// Defines methods for managing transaction data.
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Retrieves a transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId">The unique identifier of the transaction.</param>
    /// <returns>The transaction object if found; otherwise, null.</returns>
    Task<Transaction?> GetTransactionAsync(Guid transactionId);

    /// <summary>
    /// Adds a new transaction to the system.
    /// </summary>
    /// <param name="transaction">The transaction object to add.</param>
    /// <returns>True if the transaction was successfully added; otherwise, false.</returns>
    Task<bool> AddTransactionAsync(Transaction transaction);

    /// <summary>
    /// Retrieves all transactions associated with a specific player.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>A list of transactions for the player.</returns>
    Task<IEnumerable<Transaction>> GetTransactionsByPlayerIdAsync(Guid playerId);
}