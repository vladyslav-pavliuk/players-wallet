using PlayersWallet.Api.Models;

namespace PlayersWallet.Api.Repositories.Interfaces;

/// <summary>
/// Defines methods for managing player data.
/// </summary>
public interface IPlayerRepository
{
    /// <summary>
    /// Retrieves a player by their unique identifier.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>The player object if found; otherwise, null.</returns>
    Task<Player?> GetPlayerAsync(Guid playerId);

    /// <summary>
    /// Registers a new player in the system.
    /// </summary>
    /// <param name="player">The player object to register.</param>
    /// <returns>True if the player was successfully registered; otherwise, false.</returns>
    Task<bool> RegisterPlayerAsync(Player player);
}