using corea;
using corea.DapperDbContext;
using Dapper;

public class UserRepository : IUserRepository
{
    private readonly IDapperContext dapperContext;

    public UserRepository(IDapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }


    public async Task<bool> Add(User user, CancellationToken cancellationToken)
    {
        using (var connection = dapperContext.CreateConnection())
        {
            var i = await
                 connection.ExecuteAsync(new CommandDefinition("adduser", new { user.FirstName, user.LastName },
                     commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken));

            return i > 0;
        }
    }

    public async Task<IList<User>> Get(CancellationToken cancellationToken)
    {
        using (var connection = dapperContext.CreateConnection())
        {
            var query = await connection.QueryAsync<corea.User>(new CommandDefinition("getusers",
                       commandType: System.Data.CommandType.StoredProcedure, cancellationToken: cancellationToken));

            return query.ToList();
        }
    }
}

public class UserRepository2 : IUserRepository
{
    public Task<bool> Add(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<User>> Get(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public interface IUserRepository
{
    Task<bool> Add(User user, CancellationToken cancellationToken);
    Task<IList<User>> Get(CancellationToken cancellationToken);
}
