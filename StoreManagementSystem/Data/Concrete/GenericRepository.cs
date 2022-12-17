using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using StoreManagementSystem.Data.Abstract;

namespace StoreManagementSystem.Data.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly string _tableName;
    private readonly IConfigurationRoot _configuration;
    public GenericRepository(string tableName)
    {
        _tableName = tableName;
        
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
        _configuration = configurationBuilder.Build();
    }

    private SqlConnection SqlConnection() => new(_configuration.GetConnectionString("DefaultConnection"));

    protected IDbConnection CreateConnection()
    {
        var connection = SqlConnection();
        connection.Open();
        return connection;
    }

    private static IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<T>($"select * from {_tableName}");
    }

    public async Task DeleteRowAsync(int id)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync($"delete from {_tableName} where Id=@Id", new { Id = id });
    }

    public async Task<T> GetAsync(int id)
    {
        using var connection = CreateConnection();

        var result =
            await connection.QuerySingleOrDefaultAsync<T>
            ($"select * from {_tableName} where Id = @Id", new { Id = id });

        if (result == null)
            throw new KeyNotFoundException($"{_tableName} with id {id} could not be found!");

        return result;
    }

    public async Task<int> SaveRangeAsync(IEnumerable<T> list)
    {
        var inserted = 0;
        var query = GenerateInsertQuery();
        using var connection = CreateConnection();
        inserted += await connection.ExecuteAsync(query, list);
        return inserted;
    }

    public async Task<T> UpdateAsync(int id, T entity)
    {
        var updateQuery = GenerateUpdateQuery();

        using var connection = CreateConnection();
        
        await connection.ExecuteAsync(updateQuery, entity);

        return await GetAsync(id);
    }

    private string GenerateUpdateQuery()
    {
        var updateQuery = new StringBuilder($"update {_tableName} set ");
        var properties = GenerateListOfProperties(GetProperties);

        properties.ForEach(property =>
        {
            if (!property.Equals("Id"))
            {
                updateQuery.Append($"{property}=@{property},");
            }
        });

        updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
        updateQuery.Append(" where Id=@Id");

        return updateQuery.ToString();
    }
    
    public async Task<T> InsertAsync(T entity)
    {
        var insertQuery = GenerateInsertQuery();

        using var connection = CreateConnection();
        
        var id = await connection.QuerySingleAsync<int>(insertQuery, entity);

        return await GetAsync(id);
    }
    
    private string GenerateInsertQuery()
    {
        var insertQuery = new StringBuilder($"insert into {_tableName} ");
            
        insertQuery.Append("(");

        var properties = GenerateListOfProperties(GetProperties);
        properties.ForEach(prop =>
        {
            if(prop != "Id")
                insertQuery.Append($"[{prop}],");
        });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") OUTPUT INSERTED.Id VALUES (");

        properties.ForEach(prop =>
        {
            if(prop != "Id")
                insertQuery.Append($"@{prop},");
        });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(")");

        return insertQuery.ToString();
    }
    
    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {
        return (from prop in listOfProperties let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
            where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore" select prop.Name).ToList();
    }
}