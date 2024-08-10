using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace corea.DapperDbContext;

public class DapperContext : IDapperContext
{
    private readonly IConfiguration configuration;
    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(configuration.GetConnectionString("DefaultDapper"));
    }
}

public interface IDapperContext
{
    IDbConnection CreateConnection();
}