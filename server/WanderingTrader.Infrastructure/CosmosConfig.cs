namespace WanderingTrader.Infrastructure;

public class CosmosConfig
{
    internal static readonly string Position = "CosmosConfig";

    public string Endpoint { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string DbName { get; set; } = string.Empty;
}