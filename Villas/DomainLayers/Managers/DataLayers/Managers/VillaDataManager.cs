using System.Collections.Immutable;
using System.Data;
using System.Data.Common;
using Villas.DomainLayers.Managers.DataLayers.Managers.CommandFactories;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.DataLayers.Managers;

internal sealed class VillaDataManager(string connectionString) : DataManagerBase(connectionString)
{
    private DbCommand CommandFactory(Villa villa, string storeProcedureName) =>
        CommandFactoryVillas.CreateCommandForCreateVilla(
        DbConnection, villa, CommandType.StoredProcedure, storeProcedureName);

    private DbCommand CommandFactory(string storeProcedureName, string objName, object objValue) =>
        CommandFactoryVillas.CreateCommandForGetVilla(
        DbConnection, CommandType.StoredProcedure, storeProcedureName, objName, objValue);

    private DbCommand CommandFactory(string storeProcedureName) =>
        CommandFactoryVillas.CreateCommandForGetVilla(
        DbConnection, CommandType.StoredProcedure, storeProcedureName);

    protected override async Task<ImmutableList<Villa>> GetVillasAsyncCore() =>
        await RetrieveVillas(CommandFactory($"villaSchema.Sp_Get{nameof(Villa)}s")).ConfigureAwait(false);

    protected override async Task<Villa> GetVillaByIdAsyncCore(int villaId, string propertyName) =>
        await RetrieveVilla(CommandFactory($"villaSchema.Sp_Get{nameof(Villa)}By{propertyName}", propertyName, villaId)).ConfigureAwait(false);

    protected override async Task<Villa> GetVillaByNameAsyncCore(string villaName, string propertyName) =>
        await RetrieveVilla(CommandFactory($"villaSchema.Sp_Get{nameof(Villa)}By{propertyName}", propertyName, villaName)).ConfigureAwait(false);

    protected override async Task<int> CreateVillaAsyncCore(Villa villa) =>
        await RetrieveDataFromDatabaseAsync(CommandFactory(villa, $"villaSchema.Sp_Create{nameof(Villa)}")).ConfigureAwait(false);

    protected override async Task<int> UpdateVillaAsyncCore(Villa villa) =>
        await RetrieveDataFromDatabaseAsync(CommandFactory(villa, $"villaSchema.Sp_Update{nameof(Villa)}")).ConfigureAwait(false);

    protected override async Task<int> DeleteVillaAsyncCore(int villaId) =>
        await RetrieveDataFromDatabaseAsync(CommandFactory($"villaSchema.Sp_Delete{nameof(Villa)}", nameof(Villa.Id), villaId)).ConfigureAwait(false);

    private async Task<ImmutableList<Villa>> RetrieveVillas(DbCommand dbCommand)
    {
        var dbDataReader = await RetrieveDbDataReaderAsync(dbCommand).ConfigureAwait(false);

        var villas = ImmutableList.Create<Villa>();
        try
        {
            while (await dbDataReader.ReadAsync().ConfigureAwait(false))
            {
                villas = villas.Add(MapToVilla(dbDataReader));
            }
        }
        catch (DbException e)
        {
            throw new SqlDbException($"Unknow {nameof(Villa)}Db Error.", e);
        }
        finally
        {
            await DisposeAsync(dbCommand).ConfigureAwait(false);
        }

        return villas;
    }

    private async Task<Villa> RetrieveVilla(DbCommand dbCommand)
    {
        var dbDataReader = await RetrieveDbDataReaderAsync(dbCommand).ConfigureAwait(false);

        try
        {
            return await dbDataReader.ReadAsync().ConfigureAwait(false)
                ? MapToVilla(dbDataReader)
                : null;
        }
        catch (DbException e)
        {
            throw new SqlDbException($"Unknow {nameof(Villa)}Db Error.", e);
        }
        finally
        {
            await DisposeAsync(dbCommand).ConfigureAwait(false);
        }
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
}
