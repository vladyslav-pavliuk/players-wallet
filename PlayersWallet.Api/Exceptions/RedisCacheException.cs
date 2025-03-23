namespace PlayersWallet.Api.Exceptions;

/// <summary>
///     Exception thrown when there's an issue with Redis caching.
/// </summary>
public class RedisCacheException(string message, Exception innerException) : Exception(message, innerException);