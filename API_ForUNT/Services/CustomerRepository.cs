using API_ForUNT.Data;
using API_ForUNT.DTO;
using API_ForUNT.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace API_ForUNT.Services
{
    public class CustomerRepository
    {
        public static int Rowifetive;
        private readonly SqlConnectionFactory _connectionFactory;
        public CustomerRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

        }
        // Method to Return ALl Cutomer
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            var customer = new List<Customer>();
            var query = "select Top 10 * from tblCustomers where IsDelete = 0 order by Id desc";
            using (var connection = _connectionFactory.CreateConnection())
            {

                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {

                    using (var reader = command.ExecuteReader())
                    {

                        while (await reader.ReadAsync())
                        {

                            customer.Add(new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EnglishName = reader.GetString(reader.GetOrdinal("EnglishName")),
                                KhmerName = reader.GetString(reader.GetOrdinal("KhmerName")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                AttentionTo = reader.GetString(reader.GetOrdinal("AttentionTo")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                IsDelete = false,

                            });

                        }
                    }
                    return customer;

                }

            }
        }
        public async Task<int> InsertCustomerAsync(CustomerDTO customer)
        {
            var query = "INSERT INTO tblCustomers(EnglishName,KhmerName,Address,AttentionTo,ContactNumber,Email,CreateBy,CreateAt,IsDelete)values(@EnglishName,@KhmerName,@Address,@AttentionTo,@ContactNumber,@Email,@CreateBy,GETDATE(),0);SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnglishName", customer.EnglishName);
                    command.Parameters.AddWithValue("@KhmerName", customer.KhmerName);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@AttentionTo", customer.AttentionTo);
                    command.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@CreateBy", 1);
                    // ExecuteScalar is used here to return the first column of the first row in the result set
                    int customerId = (int)await command.ExecuteScalarAsync();
                    return customerId;
                }
            }
        }

        public async Task UpdateCustomerAsync(CustomerDTO customer)
        {

            var query = "" +
                   "UPDATE tblCustomers " +
                       "SET    EnglishName=@EnglishName," +
                               "KhmerName=@KhmerName," +
                               "Address=@Address," +
                               "AttentionTo=@AttentionTo," +
                               "ContactNumber=@ContactNumber," +
                               "Email=@Email," +
                               "UpdateBy =@UpdateBy," +
                               "UpdateAt=GETDATE() " +
                       "WHERE Id=@Id";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnglishName", customer.EnglishName);
                    command.Parameters.AddWithValue("@KhmerName", customer.KhmerName);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@AttentionTo", customer.AttentionTo);
                    command.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@UpdateBy", 1);
                    command.Parameters.AddWithValue("@Id", customer.Id);
                    await command.ExecuteNonQueryAsync();
                }

            }

        }
        public async Task DeleteCustomerAsync(int  customerid)
        {
            var query = "update tblCustomers set IsDelete = 'True' where id =@Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", customerid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
   
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            var query = "select * from tblCustomers where Id = @CustomerId and IsDelete = 0";
            Customer? customer = null;
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            customer = new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EnglishName = reader.GetString(reader.GetOrdinal("EnglishName")),
                                KhmerName = reader.GetString(reader.GetOrdinal("KhmerName")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                AttentionTo = reader.GetString(reader.GetOrdinal("AttentionTo")),
                                ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                IsDelete = false,
                            };
                        }
                    }
                }
            }
            return customer;
        }
        //public async Task<Customer?> SearchItem(string CustomerName, string Email)
        //{
        //    var customer = new List<Customer>();
        //    var query = "select * from tblCustomers where EnglishName like CONCAT('%',@EnglishName,'%') or ContactNumber=@EnglishName";
        //    using ( var connection = _connectionFactory.CreateConnection())
        //    {
        //        await connection.OpenAsync();
        //        {
        //            using (var command = new SqlCommand(query, connection)) {


        //                using (var reader =  command.ExecuteReader()) 
        //                {
        //                    while (await reader.ReadAsync()) {

        //                        customer.Add(new Customer
        //                        {
        //                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                            EnglishName = reader.GetString(reader.GetOrdinal("EnglishName")),
        //                            KhmerName = reader.GetString(reader.GetOrdinal("KhmerName")),
        //                            Address = reader.GetString(reader.GetOrdinal("Address")),
        //                            AttentionTo = reader.GetString(reader.GetOrdinal("AttentionTo")),
        //                            ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
        //                            Email = reader.GetString(reader.GetOrdinal("Email")),
        //                            IsDelete = false,

        //                        });

        //                    }
        //                }
                    
        //            }

          
        //        }
        //    }
     
        //}
    }
}
