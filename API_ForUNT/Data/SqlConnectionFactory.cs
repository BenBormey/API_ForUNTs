using Microsoft.Data.SqlClient;
namespace API_ForUNT.Data
{
    public class SqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("API_UNT"));
    }
}
