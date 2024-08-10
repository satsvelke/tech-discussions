using System.Text;
using corea.DapperDbContext;
using corea.DependencyInj;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddScoped<IGameService, GameService>();
//builder.Services.AddTransient<IGameService, GameService>();

builder.Services.AddSingleton<IDapperContext, DapperContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Default");
    options.SchemaName = "dbo";
    options.TableName = "TestCache";
});


builder.Services.AddOptions();

builder.Services.Configure<JwtSetttings>(builder.Configuration.GetSection("JwtSettings"));

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

// O - object from  c# class R  -->> relational (database table )  M---> mapping  
/// relational -->> database 
/// ADO.net --> mapping is manual --> 
/// Dapper --> micro-ORM --> automatic 
/// EF --> ORM --> automatic --> Code First --> 
/// 2, database first --> 
/// 3. Model first --> 
/// 


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