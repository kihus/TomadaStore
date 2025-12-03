using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerApi.Data;
using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Entities;

namespace TomadaStore.CustomerApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDb connection)
        {
            _logger = logger;
            _connection = connection.GetConnectionString();
        }

        public async Task<List<CustomerResponseDto>> GetAllCustomerAsync()
        {
            var selectSql = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Status  
                              FROM Customers";

            try
            {
                var customers = await _connection.QueryAsync<CustomerResponseDto>(selectSql);

                return [.. customers];

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.StackTrace}");

                throw new Exception(sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.StackTrace}");
                throw new Exception($"Could not find {ex.Message}");
            }
        }

        public async Task<CustomerResponseDto?> GetCustomerById(int id)
        {
            var selectSql = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Status  
                              FROM Customers
                              WHERE Id = @Id";

            try
            {
                var customer = await _connection.QueryFirstOrDefaultAsync<CustomerResponseDto>(selectSql, new { id });
                return customer;
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.StackTrace}");

                throw new Exception(sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.StackTrace}");
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Status) 
                                  VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Status);";

            try
            {
                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber, Status = customer.Status.ToString() });

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.StackTrace}");

                throw new Exception(sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.StackTrace}");
                throw new Exception(ex.Message);
            }
        }
    }
}
