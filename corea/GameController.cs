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

    public GameController(IGameService gameService, IServiceProvider serviceProvider, IOptions<JwtSetttings> options)
    {
        _gameService = gameService;
        this.serviceProvider = serviceProvider;
        this.options = options;
    }

    [HttpGet]
    // [ServiceFilter(typeof(ActionFilter))]

    public async Task<string> Get()
    {
        // 1000 
        // var game = await _gameService.Get();

        // send mail user 

        // _ = SendMail(); // 1001

        return await Task.FromResult(GenerateToken());
    }


    private Task SendMail()
    {
        //  error
        return Task.CompletedTask;
    }


    // "/api/v1/game"
    [HttpPost]
    public Task<bool> CreateGame(RequestDto requestDto)
    {
        return Task.FromResult(true);
    }


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
    public int Id { get; set; }
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