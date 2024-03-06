using Microsoft.Data.SqlClient;
using System.Collections.Immutable;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Managers.DataLayers.Managers;
using Villas.DomainLayers.Managers.DataLayers.Managers.Extensions;
using Villas.DomainLayers.Models;

namespace AcceptanceTests.TestDataGenerators;

[ExcludeFromCodeCoverage]
public static class VillaTestDataGenerator
{
    private static readonly SqlClientFactory _sqlClientFactory = SqlClientFactory.Instance;

    private static DbConnection CreateDbConnection(string connectionString)
    {
        var dbConnection = _sqlClientFactory.CreateConnection();
        dbConnection.ConnectionString = connectionString;
        return dbConnection;
    }

    public static async Task<ImmutableList<Villa>> GetVillasAsync(string connectionString)
    {
        DbConnection dbConnection = default!;
        DbCommand dbCommand = default!;
        try
        {
            dbConnection = CreateDbConnection(connectionString);
            await dbConnection.OpenAsync().ConfigureAwait(false);
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = $"[{nameof(Villa).ToLower()}Schema].[Sp_Get{nameof(Villa)}s]";
            var dbDataReader = await dbCommand.ExecuteReaderAsync().ConfigureAwait(false);
            return await RetrieveVillasAsync(dbDataReader).ConfigureAwait(false);
        }
        finally
        {
            await DisposeAsync(dbDataReader: null, dbConnection: dbConnection, dbCommand: dbCommand, dbTransaction: null).ConfigureAwait(false);
        }
    }

    public static async Task<int> GetVillaIdByNameAsync(string connectionString, string villaName)
    {
        DbConnection dbConnection = default!;
        DbCommand dbCommand = default!;
        DbTransaction dbTransaction = default!;
        try
        {
            dbConnection = CreateDbConnection(connectionString);
            await dbConnection.OpenAsync().ConfigureAwait(false);
            dbCommand = dbConnection.CreateCommand();
            dbTransaction = await dbConnection.BeginTransactionAsync(System.Data.IsolationLevel.Serializable).ConfigureAwait(false);
            dbCommand.Transaction = dbTransaction;
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = @$"Select [Id] From [villaSchema].[Tbl_Villas] Where [Name] = N'{villaName}'";
            var villaId = await dbCommand.ExecuteScalarAsync().ConfigureAwait(false);
            await dbTransaction.CommitAsync().ConfigureAwait(false);
            return (int)villaId;
        }
        catch (DbException)
        {
            await dbTransaction.RollBackIfNotNullAsync().ConfigureAwait(false);
            throw;
        }
        finally
        {
            await DisposeAsync(dbDataReader: null, dbConnection: dbConnection, dbCommand: dbCommand, dbTransaction: dbTransaction).ConfigureAwait(false);
        }
    }

    public static async Task<int> CreateVillaAsync(string connectionString, Villa expectedVilla)
    {
        await CreateVillaToDbAsync(connectionString, expectedVilla).ConfigureAwait(false);
        return await GetVillaIdByNameAsync(connectionString, expectedVilla.Name).ConfigureAwait(false);
    }

    private static async Task  CreateVillaToDbAsync(string connectionString, Villa expectedVilla)
    {
        DbConnection dbConnection = default!;
        DbCommand dbCommand = default!;
        DbTransaction dbTransaction = default!;
        try
        {
            dbConnection = CreateDbConnection(connectionString);
            await dbConnection.OpenAsync().ConfigureAwait(false);
            dbTransaction = await dbConnection.BeginTransactionAsync(System.Data.IsolationLevel.Serializable).ConfigureAwait(false);
            dbCommand = dbConnection.CreateCommand();
            dbCommand.Transaction = dbTransaction;
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = @$"Exec [{nameof(Villa).ToLower()}Schema].[Sp_Create{nameof(Villa)}] 
                @{nameof(expectedVilla.Name)} = N'{expectedVilla.Name}'
                , @{nameof(expectedVilla.Details)} = N'{expectedVilla.Details}'
                , @{nameof(expectedVilla.Rate)} = {expectedVilla.Rate}
                , @{nameof(expectedVilla.Sqft)} = {expectedVilla.Sqft}
                , @{nameof(expectedVilla.Occupancy)} = {expectedVilla.Occupancy}
                , @{nameof(expectedVilla.ImageUrl)} = N'{expectedVilla.ImageUrl}'
                , @{nameof(expectedVilla.Amenity)} = N'{expectedVilla.Amenity}'";
            _ = await dbCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
            await dbTransaction.CommitAsync().ConfigureAwait(false);
        }
        catch (DbException)
        {
            await dbTransaction.RollBackIfNotNullAsync().ConfigureAwait(false);
            throw;
        }
        finally
        {
            await DisposeAsync(dbDataReader: null, dbConnection: dbConnection, dbCommand: dbCommand, dbTransaction: dbTransaction).ConfigureAwait(false);
        }
    }

    private static async Task<ImmutableList<Villa>> RetrieveVillasAsync(DbDataReader dbDataReader)
    {
        var villas = ImmutableList.Create<Villa>();

        try
        {
            while (await dbDataReader.ReadAsync().ConfigureAwait(false))
                villas = villas.Add(MapToVilla(dbDataReader));
        }
        finally
        {
            await DisposeAsync(dbDataReader: dbDataReader, dbConnection: null, dbCommand: null, dbTransaction: null).ConfigureAwait(false);
        } 

        return villas;
    }

    private static Villa MapToVilla(DbDataReader dbDataReader)
    {
        const int IdIdx = 0;
        const int NameIdx = 1;
        const int DetailsIdx = 2;
        const int RateIdx = 3;
        const int SqftIdx = 4;
        const int OccupancyIdx = 5;
        const int ImageUrlIdx = 6;
        const int AmenityIdx = 7;

        return new Villa(
           Id: dbDataReader[IdIdx] != DBNull.Value ? (int)dbDataReader[IdIdx] : 0,
           Name: dbDataReader[NameIdx] != DBNull.Value ? (string)dbDataReader[NameIdx] : string.Empty,
           Details: dbDataReader[DetailsIdx] != DBNull.Value ? (string)dbDataReader[DetailsIdx] : string.Empty,
           Rate: dbDataReader[RateIdx] != DBNull.Value ? (double)dbDataReader[RateIdx] : 0,
           Sqft: dbDataReader[SqftIdx] != DBNull.Value ? (int)dbDataReader[SqftIdx] : 0,
           Occupancy: dbDataReader[OccupancyIdx] != DBNull.Value ? (int)dbDataReader[OccupancyIdx] : 0,
           ImageUrl: dbDataReader[ImageUrlIdx] != DBNull.Value ? (string)dbDataReader[ImageUrlIdx] : string.Empty,
           Amenity: dbDataReader[AmenityIdx] != DBNull.Value ? (string)dbDataReader[AmenityIdx] : string.Empty
           );
    }

    private static async ValueTask DisposeAsync(DbDataReader dbDataReader, DbCommand dbCommand, DbTransaction dbTransaction, DbConnection dbConnection)
    {
        await dbDataReader.DisposeIfNotNullAsync();
        await dbCommand.DisposeIfNotNullAsync();
        await dbTransaction.DisposeIfNotNullAsync();
        await dbConnection.DisposeIfNotNullAsync();
    }
}