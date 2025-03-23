using Microsoft.AspNetCore.Mvc;
using PlayersWallet.Api.Constants;
using PlayersWallet.Api.Enums;
using PlayersWallet.Api.Exceptions;
using PlayersWallet.Api.Models;
using PlayersWallet.Api.Services.Interfaces;

namespace PlayersWallet.Api.Controllers;

/// <summary>
/// Provides REST API endpoints for managing player wallets and transactions.
/// </summary>
[ApiController]
[Route(RoutePaths.Wallet)]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletController"/> class.
    /// </summary>
    /// <param name="walletService">The wallet service.</param>
    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    /// <summary>
    /// Registers a new player in the system.
    /// </summary>
    /// <param name="request">The register player request.</param>
    /// <returns>200 OK if successful; otherwise, 400 Bad Request.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterPlayer([FromBody] RegisterPlayerRequest request)
    {
        try
        {
            await _walletService.RegisterPlayerAsync(request.PlayerId);
            return Ok();
        }
        catch (PlayerAlreadyRegisteredException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the current balance of a player's wallet.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>The current balance of the wallet.</returns>
    [HttpGet("balance/{playerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBalance(Guid playerId)
    {
        try
        {
            var balance = await _walletService.GetBalanceAsync(playerId);
            return Ok(balance);
        }
        catch (RedisCacheException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Credits a transaction to a player's wallet.
    /// </summary>
    /// <param name="request">The credit transaction request.</param>
    /// <returns>200 OK if the transaction was accepted; otherwise, 400 Bad Request.</returns>
    [HttpPost("transaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreditTransaction([FromBody] CreditTransactionRequest request)
    {
        try
        {
            await _walletService.CreditTransactionAsync(request.PlayerId, request.TransactionId, request.Amount, request.Type);
            return Ok();
        }
        catch (TransactionAlreadyProcessedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InsufficientBalanceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves all transactions associated with a specific player.
    /// </summary>
    /// <param name="playerId">The unique identifier of the player.</param>
    /// <returns>A list of transactions for the player.</returns>
    [HttpGet("transactions/{playerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactions(Guid playerId)
    {
        var transactions = await _walletService.GetTransactionsAsync(playerId);
        return Ok(transactions);
    }
}