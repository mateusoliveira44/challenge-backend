using challenge_backend.Application.Options;
using challenge_backend.API.Configuration;
using challenge_backend.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

app.UseDatabaseConfiguration();
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.MapAllEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
