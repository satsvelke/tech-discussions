using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using corea.DependencyInj;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;


[Route("/api/v1/[Controller]")]
[Authorize]
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

    public GameController(IGameService gameService, IServiceProvider serviceProvider)
    {
        _gameService = gameService;
        this.serviceProvider = serviceProvider;
    }

    [HttpGet]
    [AllowAnonymous]
    public Task<string> Get()
    {
        return Task.FromResult(GenerateToken());
    }

    // "/api/v1/game"
    [HttpPost]
    public Task<bool> CreateGame(RequestDto requestDto)
    {
        return Task.FromResult(true);
    }



    public string GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fc8Y9SbrcEJODrGIfSDlsRalHU0UzYoU"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
          {
                    new Claim("email", "satsvelke@gmail.com"),
                    new Claim("userid", "1"),
         };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
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