namespace APBD_Tutorial9.Infrastructure.Repositories;

public abstract class RepositoryBase
{
    protected readonly string _connectionString;

    public RepositoryBase(string connectionString)
    {
        _connectionString = connectionString;
    }
}