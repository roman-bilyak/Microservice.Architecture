using Microservice.IdentityService.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddTestUsers(Config.TestUsers.ToList())
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential(); //not recommended for production - you need to store your key material somewhere secure

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseIdentityServer();

app.Run();