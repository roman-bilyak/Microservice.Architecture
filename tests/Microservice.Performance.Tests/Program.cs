using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http;
using NBomber.Plugins.Http.CSharp;

RunScenario("gateway", 200, 60, "http://localhost:51500/api/TS/Test/Get");
//RunScenario("test-service", 200, 60, "http://localhost:51505/api/Test/Get");

Console.ReadLine();

static void RunScenario(string scenarioName, int numberOfThreads, int seconds, params string[] url)
{
    IClientFactory<HttpClient> httpClientFactory = HttpClientFactory.Create();

    int index = 1;
    Scenario scenario = ScenarioBuilder.CreateScenario(scenarioName, Array.ConvertAll(url, x => GetHttpStep($"{scenarioName}_step{index++}", x, httpClientFactory)))
        .WithWarmUpDuration(TimeSpan.FromSeconds(5))
        .WithLoadSimulations
        (
            Simulation.KeepConstant(numberOfThreads, TimeSpan.FromSeconds(seconds))
        );

    NBomberRunner
        .RegisterScenarios(scenario)
        .WithTestSuite("performance")
        .WithTestName(scenarioName)
        .Run();
}

static IStep GetHttpStep(string name, string url, IClientFactory<HttpClient> httpClientFactory)
{
    return Step.Create(name, clientFactory: httpClientFactory, execute: async context =>
    {
        HttpRequest request = Http.CreateRequest("GET", url)
                        .WithCheck(async (response) =>
                            response.IsSuccessStatusCode
                                ? Response.Ok(statusCode: (int)response.StatusCode)
                                : Response.Fail(statusCode: (int)response.StatusCode)
                        );

        Response response = await Http.Send(request, context);
        return response;
    }, timeout: TimeSpan.FromSeconds(5));
}