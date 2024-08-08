using System.Threading.Tasks;
public class Middleware
{
    private readonly RequestDelegate next;

    public Middleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;

        if (request is not null)
        {
            Console.WriteLine("hi from middleware");
        }

        await next(context);
    }
}