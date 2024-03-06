using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Managers.DataLayers.Managers.Extensions;

[ExcludeFromCodeCoverage]
public static class DbTransactionExtension
{
    public static async Task RollBackIfNotNullAsync(this DbTransaction dbTransaction) =>
            await dbTransaction?.RollbackAsync();
}
