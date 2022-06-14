using CommandLine;
using Microservice.Performance.Tests;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http;
using NBomber.Plugins.Http.CSharp;

return Parser.Default.ParseArguments<CommandLineOptions>(args)
    .MapResult(
        opts => 
        {
            try
            {
                RunScenario(opts.Urls, opts.Timeout, opts.Config);
                Console.WriteLine("Done!");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(ex.ToString());
                return -3;
            }
        },
        errors => 
        {
            return -1;
        });

static void RunScenario(IEnumerable<string> urls, int timeout, string config)
{
    IClientFactory<HttpClient> httpClientFactory = HttpClientFactory.Create();

    int index = 1;
    IStep[] steps = urls.Select(x => GetHttpStep($"step_{index++}", x, timeout, httpClientFactory)).ToArray();

    Scenario scenario = ScenarioBuilder.CreateScenario("scenario", steps);

    NBomberContext nBomberContext = NBomberRunner.RegisterScenarios(scenario);

    if (!string.IsNullOrEmpty(config))
    {
        nBomberContext = nBomberContext.LoadConfig(config);
    }

    nBomberContext.Run();
}

static IStep GetHttpStep(string name, string url, int timeout, IClientFactory<HttpClient> httpClientFactory)
{
    return Step.Create(name, clientFactory: httpClientFactory, execute: async context =>
    {
        HttpRequest request = Http.CreateRequest("GET", url)
                        .WithCheck(response =>
                            Task.FromResult(response.IsSuccessStatusCode
                                ? Response.Ok(statusCode: (int)response.StatusCode)
                                : Response.Fail(statusCode: (int)response.StatusCode))

                        );

        Response response = await Http.Send(request, context);
        return response;
    }, timeout: TimeSpan.FromSeconds(timeout));
}