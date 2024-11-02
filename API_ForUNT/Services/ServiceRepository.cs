using API_ForUNT.Data;
using API_ForUNT.DTO;
using API_ForUNT.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_ForUNT.Services
{
    public class ServiceRepository
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        public ServiceRepository(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<Service>> GetAllService()
        {
            var service = new List<Service>();
            var query = "select Top 10 * from tblservice order by ServiceId desc";
            using (var connection = _sqlConnectionFactory.CreateConnection())
            {

                await connection.OpenAsync();
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {


                            while (await reader.ReadAsync())
                            {

                                service.Add(new Service
                                {
                                    SeviceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                                    SeviceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                                    ServiceNameKhmer = reader.GetString(reader.GetOrdinal("ServiceNameKhmer")),
                                    Price = reader.GetDouble(reader.GetOrdinal("Price")),
                                    Currency = reader.GetString(reader.GetOrdinal("currency")),



                                });
                            }
                        }
                        return service;

                    }

                }

            }
        }
        public async Task<int> InsertService(ServiceDTO service)
        {
            var query = "INSERT INTO tblService(ServiceName,CreateBy,CreateAt,ServiceNameKhmer,Price,currency)Values(@ServiceName,@CreateBy,GETDATE(),@ServiceNameKhmer,@Price,@currency);SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ServiceName", service.SeviceName);
                    command.Parameters.AddWithValue("@ServiceNameKhmer", service.ServiceNameKhmer);
                    command.Parameters.AddWithValue("@Price", service.Price);
                    command.Parameters.AddWithValue("@currency", service.Currency);
            
                    command.Parameters.AddWithValue("@CreateBy", 1);
                    // ExecuteScalar is used here to return the first column of the first row in the result set
                    int serviceId = (int)await command.ExecuteScalarAsync();
                    return serviceId;
                }
            }
        }
    }
}
