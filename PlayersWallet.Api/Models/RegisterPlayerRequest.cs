namespace PlayersWallet.Api.Models;

/// <summary>
///     Represents a request to register a new player.
/// </summary>
public class RegisterPlayerRequest
{
    /// <summary>
    ///     The unique identifier of the player.
    /// </summary>
    public Guid PlayerId { get; set; }
}