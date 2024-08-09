namespace corea.DependencyInj;

public class ClassB : IClassB
{
    public Task<dynamic> Get(string body, string StoredProcedure)
    {
        throw new NotImplementedException();
    }
}

public interface IClassB
{
    Task<dynamic> Get(string body, string StoredProcedure);
}
