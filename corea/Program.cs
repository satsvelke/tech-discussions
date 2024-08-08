using System.Text;
using corea.DependencyInj;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddScoped<IGameService, GameService>();
//builder.Services.AddTransient<IGameService, GameService>();

builder.Services.AddScoped<IClassB, ClassB>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    var key = "fc8Y9SbrcEJODrGIfSDlsRalHU0UzYoU";
    var k = Encoding.ASCII.GetBytes(key);

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(k)
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");


app.UseMiddleware<Middleware>();




await app.RunAsync();

// use --> 
// map -> optional -> 
// run --> is terminal 

// routing --> its way of mapping http request to  specific endpoint 
// conventional => handled globally ==> 
// attribute ==> 

// AddSingleton -- only one instance 
// AddScoped -- there is 1 instance per request, it will maintain the state  --> 
// AddTransient -- there will be new instance call 


// dependecy injection 
// depedency inversion principle - D from SOLID 

// classA -- higher module
// private readonly IClassB classb 
// 
//    --> var classb = new ClassB();

// classB : IClassB -- lower module 