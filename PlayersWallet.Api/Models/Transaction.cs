using PlayersWallet.Api.Enums;

namespace PlayersWallet.Api.Models;

/// <summary>
/// Represents a transaction that modifies a player's wallet balance.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Unique identifier for the transaction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the player associated with the transaction.
    /// </summary>
    public Guid PlayerId { get; set; }

    /// <summary>
    /// Amount of money involved in the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Type of the transaction (Deposit, Stake, or Win).
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Indicates whether the transaction was accepted or rejected.
    /// </summary>
    public bool IsAccepted { get; set; }
}