using PlayersWallet.Api.Models;

namespace PlayersWallet.Api.Repositories.Interfaces;

/// <summary>
/// Defines methods for managing wallet data.
/// </summary>
public interface IWalletRepository
{
    /// <summary>
    /// Retrieves a wallet by the player's unique identifier.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>The wallet object if found; otherwise, null.</returns>
    Task<Wallet?> GetWalletAsync(Guid playerId);

    /// <summary>
    /// Updates a wallet's balance in the system.
    /// </summary>
    /// <param name="wallet">The wallet object to update.</param>
    /// <returns>True if the wallet was successfully updated; otherwise, false.</returns>
    Task<bool> UpdateWalletAsync(Wallet wallet);
}