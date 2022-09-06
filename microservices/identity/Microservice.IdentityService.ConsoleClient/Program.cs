using IdentityModel.Client;

HttpClient client = new HttpClient();

TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
{
    Address = "https://localhost:9101/connect/token",

    ClientId = "console",
    ClientSecret = "secret",

    UserName = "alice",
    Password = "alice",

    Scope = "api"
});

Console.WriteLine(tokenResponse.IsError ? tokenResponse.Error : tokenResponse.Json);
Console.WriteLine("\n\n");

Console.ReadKey();