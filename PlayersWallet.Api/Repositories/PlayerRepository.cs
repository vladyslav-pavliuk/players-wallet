using PlayersWallet.Api.Models;
using PlayersWallet.Api.Repositories.Interfaces;

namespace PlayersWallet.Api.Repositories;

/// <summary>
/// Provides an in-memory implementation of the <see cref="IPlayerRepository"/> interface.
/// </summary>
public class PlayerRepository : IPlayerRepository
{
    private readonly Dictionary<Guid, Player> _players = new();

    /// <inheritdoc />
    public Task<Player?> GetPlayerAsync(Guid playerId)
    {
        _players.TryGetValue(playerId, out var player);
        return Task.FromResult(player);
    }

    /// <inheritdoc />
    public Task<bool> RegisterPlayerAsync(Player player)
    {
        return Task.FromResult(_players.TryAdd(player.Id, player));
    }
}