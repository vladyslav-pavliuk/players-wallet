namespace PlayersWallet.Api.Exceptions;

/// <summary>
///     Exception thrown when a player's balance is insufficient for a transaction.
/// </summary>
public class InsufficientBalanceException(Guid playerId, decimal amount)
    : Exception($"Player with ID {playerId} has insufficient balance for the transaction amount {amount}.");