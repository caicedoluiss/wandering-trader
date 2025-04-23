namespace WanderingTrader.WebAPI;

internal static class Utils
{
    internal static string BuildEndpointPath(params string?[] pathParts)
    {
        return string.Join("/", pathParts.Where(x => !string.IsNullOrEmpty(x)).Select(x => x!.TrimStart('/').TrimEnd('/')).Distinct());
    }
}