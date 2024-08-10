using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using corea.DependencyInj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using corea;
[Route("/api/v1/[Controller]")]
// [Authorize]
// [ServiceFilter(typeof(AuthorizeFilter))]
// [ServiceFilter(typeof(ExceptionFilter))]
public class GameController : Controller
{



    // http verbs - get, post, patch/put , delete 
    // get  -->  "api/game/1" ,, "api/game?id=1"

    // post ->> "api/game" ==> {} ==> the data should be new 

    // patch/put --> at time partial  update --> request body --> {}

    // delete --> no request body ,  "api/game/1" ,, "api/game?id=1"

    // "/api/v1/game"




    private readonly IGameService _gameService;
    private readonly IServiceProvider serviceProvider;
    private readonly IOptions<JwtSetttings> options;

    private readonly IUserRepository userRepository;

    private readonly IMemoryCache memoryCache;
    private readonly IDistributedCache distributedCache;


    public GameController(IGameService gameService, IServiceProvider serviceProvider, IOptions<JwtSetttings> options, IUserRepository userRepository, IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        _gameService = gameService;
        this.serviceProvider = serviceProvider;
        this.options = options;
        this.userRepository = userRepository;
        this.memoryCache = memoryCache;
        this.distributedCache = distributedCache;
    }

    [HttpGet]
    // [ServiceFilter(typeof(ActionFilter))]

    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {




        // var i = 0;

        // dynamic r;
        // r = 0;

        // 1000 
        // var game = await _gameService.Get();

        // send mail user 

        // _ = SendMail(); // 1001
        // ArgumentNullException.ThrowIfNull(requestDto);
        // string? p = requestDto.Id ?? default(string);

        // var cachedUsers = memoryCache.Get("users");

        // if (cachedUsers is null)
        // {
        //     var users = await userRepository.Get(cancellationToken);
        //     memoryCache.Set("users", users);

        //     return Ok(users);
        // }

        var distributedCacheData = distributedCache.GetString("users");

        if (string.IsNullOrWhiteSpace(distributedCacheData))
        {
            var users = await userRepository.Get(cancellationToken);

            distributedCache.SetString("users", JsonConvert.SerializeObject(users), new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            });

            return Ok(users);

        }

        return Ok(JsonConvert.DeserializeObject<IList<User>>(distributedCacheData));
    }


    private Task SendMail()
    {
        //  error
        return Task.CompletedTask;
    }


    // "/api/v1/game"
    [HttpPost]
    public async Task<bool> Createuser([FromBody] UserDto userDto, CancellationToken cancellationToken)
    => await userRepository.Add(new corea.User() { FirstName = userDto.FirstName, LastName = userDto.LastName }, cancellationToken);


    // {
    //     var isAdded = await userRepository.Add(new corea.User() { FirstName = userDto.FirstName, LastName = userDto.LastName }, cancellationToken);

    //     return isAdded;
    // }


    public string GenerateToken()
    {

        var secretKey = options.Value.SecretKey;
        var expirey = options.Value.Expirey;


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
          {
                    new Claim("email", "satsvelke@gmail.com"),
                    new Claim("userid", "1"),
         };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirey),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpPatch]
    public Task<bool> UpdateGame()
    {
        return Task.FromResult(true);
    }


    [HttpDelete]
    public Task<bool> DeleteGame()
    {
        return Task.FromResult(true);
    }

    // [Route("/api/v1/game/{id:int}/{gameid}")]
    // public Task<bool> Add(int id, string gameid)
    // {
    //     return Task.FromResult(true);
    // }

    // [Route("/api/v1/game")]
    // public Task<bool> Add([FromQuery] RequestDto requestDto)
    // {
    //     return Task.FromResult(true);
    // }
}




public class RequestDto
{

    [Required(ErrorMessage = "id is required")]
    public string? Id { get; set; }
}


public class UserDto
{

    [Required(ErrorMessage = "FirstName is required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }
}

public interface IGameService
{
    Task<bool> Get();
}

public class GameService : IGameService
{
    public Task<bool> Get()
    {
        throw new NotImplementedException();
    }
}