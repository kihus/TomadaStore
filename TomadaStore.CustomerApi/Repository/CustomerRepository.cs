using Dapper;
using Infrastructure.Data.SQL.Context;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Entities;

namespace TomadaStore.CustomerApi.Repository;

public class CustomerRepository(
    ILogger<CustomerRepository> logger,
    SqlConnectionDb connection
    ) : ICustomerRepository
{
    private readonly ILogger<CustomerRepository> _logger = logger;
    private readonly SqlConnectionDb _connection = connection;

    public async Task<List<CustomerResponseDto>> GetAllCustomerAsync()
    {
        var selectSql = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Status  
                          FROM Customers";

        try
        {
            using (var con = _connection.GetConnectionString())
            {
                
                var customers = await con.QueryAsync<CustomerResponseDto>(selectSql);
                return [.. customers];
            }

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
            using var con = _connection.GetConnectionString();
            var customer = await con.QueryFirstOrDefaultAsync<CustomerResponseDto>(selectSql, new { id });
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
            using var con = _connection.GetConnectionString();
            await con.ExecuteAsync(insertSql, new { customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber, Status = customer.Status.ToString() });
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

    public async Task UpdateStatusCustomerAsync(int id)
    {
        var updateSql = @"UPDATE Customers SET Active = CASE Active WHEN 0 THEN 1
                                                                    WHEN 1 THEN 0
                          WHERE Id = @Id";
        try
        {
            using var con = _connection.GetConnectionString();
            await con.ExecuteAsync(updateSql, new { id });
        }
        catch (SqlException sqlEx)
        {
            throw new Exception(sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
