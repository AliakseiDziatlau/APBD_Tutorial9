using Microsoft.Data.SqlClient;

namespace APBD_Tutorial9.Infrastructure.Repositories.DatabaseUtils;

public static class DbUtils
{
    public static async Task<List<T>> MapListAsync<T>(
        SqlDataReader reader,
        Func<SqlDataReader, T> mapFunc)
    {
        var result = new List<T>();
        while (await reader.ReadAsync())
        {
            result.Add(mapFunc(reader));
        }
        return result;
    }

    public static async Task<T?> MapSingleAsync<T>(
        SqlDataReader reader,
        Func<SqlDataReader, T> mapFunc)
        where T : class
    {
        if (await reader.ReadAsync())
            return mapFunc(reader);

        return null;
    }
}