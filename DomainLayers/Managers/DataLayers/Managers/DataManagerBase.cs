using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.Immutable;
using Villas.DomainLayers.Models;
using Villas.DomainLayers.Managers.DataLayers.Managers.Extensions;
using Villas.DomainLayers.Managers.DataLayers.Managers.Exceptions;

namespace Villas.DomainLayers.Managers.DataLayers.Managers;

public abstract class DataManagerBase
{
    protected DataManagerBase(string connectionString)
    {
        var dbConnection = _sqlClientFactory.CreateConnection();
        dbConnection.ConnectionString = connectionString;
        DbConnection = dbConnection;
    }

    private readonly SqlClientFactory _sqlClientFactory = SqlClientFactory.Instance;

    private DbDataReader _dbDataReader;
    private DbTransaction _dbTransaction;

    public DbConnection DbConnection { get; }

    public Task<ImmutableList<Villa>> GetVillasAsync() => GetVillasAsyncCore();
    protected abstract Task<ImmutableList<Villa>> GetVillasAsyncCore();

    public Task<Villa> GetVillaByIdAsync(int villaId, string propertyName) => GetVillaByIdAsyncCore(villaId, propertyName);
    protected abstract Task<Villa> GetVillaByIdAsyncCore(int villaId, string propertyName);

    public Task<Villa> GetVillaByNameAsync(string villaName, string propertyName) => GetVillaByNameAsyncCore(villaName, propertyName);
    protected abstract Task<Villa> GetVillaByNameAsyncCore(string villaName, string propertyName);

    public Task<int> CreateVillaAsync(Villa villa) => CreateVillaAsyncCore(villa);
    protected abstract Task<int> CreateVillaAsyncCore(Villa villa);

    public Task<int> UpdateVillaAsync(Villa villa) => UpdateVillaAsyncCore(villa);
    protected abstract Task<int> UpdateVillaAsyncCore(Villa villa);

    public Task<int> DeleteVillaAsync(int parameterValue) => DeleteVillaAsyncCore(parameterValue);
    protected abstract Task<int> DeleteVillaAsyncCore(int parameterValue);

    protected async Task<DbDataReader> RetrieveDbDataReaderAsync(DbCommand dbCommand)
    {
        await DbConnection.OpenAsync().ConfigureAwait(false);
        _dbDataReader ??= await dbCommand.ExecuteReaderAsync().ConfigureAwait(false);
        return _dbDataReader;
    }

    protected async Task RollBackIfNotNullAsync() =>
        await _dbTransaction.RollBackIfNotNullAsync().ConfigureAwait(false);

    protected async Task<int> RetrieveDataFromDatabaseAsync(DbCommand dbCommand)
    {
        try
        {
            return await RetrieveDbCommandParameters(dbCommand).ConfigureAwait(false);
        }
        catch (DbException e)
        {
            await RollBackIfNotNullAsync().ConfigureAwait(false);
            if (e.Message.Contains("Cannot insert duplicate "))
                throw new SqlDbInsertDublicateException("The data is exists. Check your data");
            throw new SqlDbException($"Unknow Db Error.", e);
        }
        finally
        {
            await DisposeAsync(dbCommand).ConfigureAwait(false);
        }
    }

    private async Task<int> RetrieveDbCommandParameters(DbCommand dbCommand)
    {
        await DbConnection.OpenAsync().ConfigureAwait(false);
        _dbTransaction = await DbConnection.BeginTransactionAsync(IsolationLevel.Serializable).ConfigureAwait(false);
        dbCommand.Transaction = _dbTransaction;
        await dbCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
        await _dbTransaction.CommitAsync().ConfigureAwait(false);
        return (int)dbCommand.Parameters[0].Value;
    }

    protected async ValueTask DisposeAsync(DbCommand dbCommand)
    {
        await _dbDataReader.DisposeIfNotNullAsync().ConfigureAwait(false);
        await dbCommand.DisposeIfNotNullAsync().ConfigureAwait(false);
        await _dbTransaction.DisposeIfNotNullAsync().ConfigureAwait(false);
        await DbConnection.DisposeIfNotNullAsync().ConfigureAwait(false);
    }
}
