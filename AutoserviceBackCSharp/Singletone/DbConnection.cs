namespace AutoserviceBackCSharp.Singletone
{
    public class DbConnection
    {
        public string ConnectionString { get; set; }
        public DbConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
