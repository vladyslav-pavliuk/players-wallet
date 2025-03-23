using System.Text.Json.Serialization;
using PlayersWallet.Api.Enums;

namespace PlayersWallet.Api.Models;

/// <summary>
///     Represents a request to credit a transaction to a player's wallet.
/// </summary>
public class CreditTransactionRequest
{
    /// <summary>
    ///     The unique identifier of the player.
    /// </summary>
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     The unique identifier of the transaction.
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    ///     The amount of money involved in the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     The type of the transaction (Deposit, Stake, or Win).
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType Type { get; set; }
}
