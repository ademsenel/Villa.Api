using System.Data;
using System.Data.Common;

namespace Ras.Core.Data.CommandFactories;

public static class CommandFactory
{
    public static void AddReturnValueParameter(DbCommand dbCommand) =>
       AddCommandParameter(dbCommand, "@Return", ParameterDirection.ReturnValue, DbType.Int32, null, 0);

    public static void AddCommandParameter(DbCommand dbCommand, string parameterName, ParameterDirection parameterDirection, DbType dbType, object value, int size)
    {
        var dbParameter = dbCommand.CreateParameter();
        dbParameter.ParameterName = parameterName;
        dbParameter.Direction = parameterDirection;
        dbParameter.DbType = dbType;
        dbParameter.Value = value;
        dbParameter.Size = size;
        dbCommand.Parameters.Add(dbParameter);
    }

    public static DbCommand GetDbCommand(DbConnection dbConnection, CommandType commandType, string commandText)
    {
        var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandType = commandType;
        dbCommand.CommandText = commandText;
        return dbCommand;
    }
}