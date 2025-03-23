namespace PlayersWallet.Api.Exceptions;

/// <summary>
///     Exception thrown when a player is already registered.
/// </summary>
public class PlayerAlreadyRegisteredException(Guid playerId)
    : Exception($"Player with ID {playerId} is already registered.");