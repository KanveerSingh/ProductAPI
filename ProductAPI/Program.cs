using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.development.json")
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddUserSecrets<Program>(true)
    .Build();

// Configure MongoDB
var userName = _configuration.GetSection("username").Value ?? "";
var password = _configuration.GetSection("password").Value ?? "";
var connectionString = String.Format(_configuration.GetSection("ConnectionString").Value ?? "", userName, password);

var mongoClient = new MongoClient(new MongoUrl(connectionString));
var database = mongoClient.GetDatabase("ProductDB");

// Register IMongoDatabase for dependency injection
builder.Services.AddSingleton(database);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
