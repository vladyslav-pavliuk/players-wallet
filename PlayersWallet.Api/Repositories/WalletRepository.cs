using PlayersWallet.Api.Models;
using PlayersWallet.Api.Repositories.Interfaces;

namespace PlayersWallet.Api.Repositories;

/// <summary>
/// Provides an in-memory implementation of the <see cref="IWalletRepository"/> interface.
/// </summary>
public class WalletRepository : IWalletRepository
{
    private readonly Dictionary<Guid, Wallet> _wallets = new();

    /// <inheritdoc />
    public Task<Wallet?> GetWalletAsync(Guid playerId)
    {
        _wallets.TryGetValue(playerId, out var wallet);
        return Task.FromResult(wallet);
    }

    /// <inheritdoc />
    public Task<bool> UpdateWalletAsync(Wallet wallet)
    {
        _wallets[wallet.PlayerId] = wallet;
        return Task.FromResult(true);
    }
}