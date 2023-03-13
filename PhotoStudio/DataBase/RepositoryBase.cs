using Npgsql;

namespace PhotoStudio.DataBase;

public class RepositoryBase
{
    public RepositoryBase()
    {
        GetConnectionString();
    }

    private string? _connectionString;
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
    private void GetConnectionString() =>
        _connectionString = "host=127.0.0.1;port=5432;Username=danilaPro;Password=1603;Database=PhotoStudio;";
}