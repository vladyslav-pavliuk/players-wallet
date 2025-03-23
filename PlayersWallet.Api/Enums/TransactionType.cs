namespace PlayersWallet.Api.Enums;

/// <summary>
/// Represents the type of transaction.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Increases the wallet balance.
    /// </summary>
    Deposit,

    /// <summary>
    /// Decreases the wallet balance (e.g., for betting).
    /// </summary>
    Stake,

    /// <summary>
    /// Increases the wallet balance (e.g., for winnings).
    /// </summary>
    Win
}