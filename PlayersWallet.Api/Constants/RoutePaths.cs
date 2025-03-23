namespace PlayersWallet.Api.Constants;

/// <summary>
///     Route prefixes for controllers.
/// </summary>
public static class RoutePaths
{
    /// <summary>The base controller route.</summary>
    private const string Api = "api/";

    #region Calculation management controllers

    /// <summary>The  route path of wallet.</summary>
    public const string Wallet = Api + "wallet";

    #endregion
}