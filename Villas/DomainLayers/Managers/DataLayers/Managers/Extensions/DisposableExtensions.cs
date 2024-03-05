namespace Villas.DomainLayers.Managers.DataLayers.Managers;

public static class DisposableExtensions
{
    public static async ValueTask DisposeIfNotNullAsync(this IAsyncDisposable disposable)
    {
        if (disposable != null)
            await disposable.DisposeAsync().ConfigureAwait(false);
    }
}
