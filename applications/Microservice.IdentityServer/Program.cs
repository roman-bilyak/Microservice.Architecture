using Microservice.IdentityService.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
    })
    .AddTestUsers(Config.TestUsers.ToList())
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(builder.Configuration.GetSection("IdentityServer:ApiResources"))
    .AddInMemoryApiScopes(builder.Configuration.GetSection("IdentityServer:ApiScopes"))
    .AddInMemoryClients(builder.Configuration.GetSection("IdentityServer:Clients"))
    .AddDeveloperSigningCredential(); //not recommended for production - you need to store your key material somewhere secure

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.Run();