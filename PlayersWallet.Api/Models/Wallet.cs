namespace PlayersWallet.Api.Models;

/// <summary>
/// Represents a player's wallet, which holds the balance.
/// </summary>
public class Wallet
{
    /// <summary>
    /// Unique identifier of the player who owns the wallet.
    /// </summary>
    public Guid PlayerId { get; set; }

    /// <summary>
    /// Current balance in the wallet.
    /// </summary>
    public decimal Balance { get; set; }
}