namespace Infrastructure.MongoDb;

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public MongoDbSettings()
    {
        ConnectionString = Environment.GetEnvironmentVariable(EnvConst.MONGODB_CONNECTION_STRING) ?? throw new InvalidOperationException($"ENV {EnvConst.MONGODB_CONNECTION_STRING} NOT SET");
        DatabaseName = Environment.GetEnvironmentVariable(EnvConst.MONGODB_DATABASE_NAME) ?? throw new InvalidOperationException($"ENV {EnvConst.MONGODB_DATABASE_NAME} NOT SET");
    }
}