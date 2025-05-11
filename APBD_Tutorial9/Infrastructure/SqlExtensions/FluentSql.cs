using System.Data;
using Microsoft.Data.SqlClient;

namespace APBD_Tutorial9.Infrastructure.SqlExtensions;

public class FluentSql : IAsyncDisposable
{
    private readonly SqlConnection _connection;
    private readonly SqlCommand _command;

    private FluentSql(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _command = _connection.CreateCommand();
        _command.CommandType = CommandType.Text;
    }

    public static FluentSql From(string connectionString)
    {
        return new FluentSql(connectionString);
    }

    public FluentSql WithSql(string sql)
    {
        _command.CommandText = sql;
        return this;
    }

    public FluentSql WithParameter(string name, object? value)
    {
        var param = _command.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        _command.Parameters.Add(param);
        return this;
    }
    
    public FluentSql WithParameters(params (string name, object? value)[] parameters)
    {
        foreach (var (name, value) in parameters)
        {
            var param = _command.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            _command.Parameters.Add(param);
        }

        return this;
    }

    public async Task<SqlDataReader> ExecuteReaderAsync()
    {
        await _connection.OpenAsync();
        return await _command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }
    
    public async Task<object?> ExecuteScalarAsync()
    {
        await _connection.OpenAsync();
        return await _command.ExecuteScalarAsync();
    }
    
    public async Task ExecuteNonQueryAsync()
    {
        await _connection.OpenAsync();
        await _command.ExecuteNonQueryAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _connection.DisposeAsync();
    }
}