namespace PlayersWallet.Api.Exceptions;

/// <summary>
///     Exception thrown when a transaction is already processed.
/// </summary>
public class TransactionAlreadyProcessedException(Guid transactionId)
    : Exception($"Transaction with ID {transactionId} is already processed.");