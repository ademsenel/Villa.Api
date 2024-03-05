using Ras.Core.Data.CommandFactories;
using System.Data;
using System.Data.Common;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.DataLayers.Managers.CommandFactories;

internal static class CommandFactoryVillas
{
    internal static DbCommand CreateCommandForCreateVilla(DbConnection dbConnection, Villa villa, CommandType commandType, string commandText)
    {
        var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandType = commandType;
        dbCommand.CommandText = commandText;
        CommandFactory.AddReturnValueParameter(dbCommand);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Id)}", ParameterDirection.Input, DbType.Int32, villa.Id, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Name)}", ParameterDirection.Input, DbType.String, villa.Name, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Details)}", ParameterDirection.Input, DbType.String, villa.Details, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Rate)}", ParameterDirection.Input, DbType.Double, villa.Rate, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Sqft)}", ParameterDirection.Input, DbType.Int32, villa.Sqft, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Occupancy)}", ParameterDirection.Input, DbType.Int32, villa.Occupancy, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.ImageUrl)}", ParameterDirection.Input, DbType.String, villa.ImageUrl, 0);
        CommandFactory.AddCommandParameter(dbCommand, $"@{nameof(Villa.Amenity)}", ParameterDirection.Input, DbType.String, villa.Amenity, 0);
        return dbCommand;
    }

    internal static DbCommand CreateCommandForGetVilla(DbConnection dbConnection, CommandType commandType, string commandText, string objName, object objValue)
    {
        var dbCommand = CreateCommandForGetVilla(dbConnection, commandType, commandText);
        DbType dbType = DbType.Int32;
        if (objValue != null)
        {
            if (objValue.GetType().Name.Equals(nameof(String), StringComparison.OrdinalIgnoreCase))
                dbType = DbType.String;
            else if (objValue.GetType().Name.Equals(nameof(Int32), StringComparison.OrdinalIgnoreCase))
                dbType = DbType.Int32;
        }
        CommandFactory.AddCommandParameter(dbCommand, $"@{objName}", ParameterDirection.Input, dbType, objValue, 0);
        return dbCommand;
    }

    internal static DbCommand CreateCommandForGetVilla(DbConnection dbConnection, CommandType commandType, string commandText)
    {
        var dbCommand = CommandFactory.GetDbCommand(dbConnection, commandType, commandText);
        CommandFactory.AddReturnValueParameter(dbCommand);
        return dbCommand;
    }
}