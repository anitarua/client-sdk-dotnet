using Amazon.Lambda.Core;
using System;
using Momento.Sdk;
using Momento.Sdk.Auth;
using Momento.Sdk.Config;
using Momento.Sdk.Config.Middleware;
using Microsoft.Extensions.Logging;
using Momento.Sdk.Responses;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MomentoMetricsMiddlewareCDKExample;

public class Function
{
    private static readonly ILogger Logger;

    private static readonly ILoggerFactory LoggerFactory;

    static Function()
    {
        LoggerFactory = InitializeLogging();
        Logger = LoggerFactory.CreateLogger<Function>();
    }

    public async Task FunctionHandler()
    {
        Console.WriteLine("Hello World!");

        ICredentialProvider authProvider = new EnvMomentoTokenProvider("MOMENTO_API_KEY");
        TimeSpan DEFAULT_TTL = TimeSpan.FromSeconds(60);

        ICacheClient client = new CacheClient(Configurations.Laptop.V1(LoggerFactory).WithMiddlewares(new List<IMiddleware>{new ExperimentalMetricsLoggingMiddleware(LoggerFactory)}), authProvider, DEFAULT_TTL);
        Console.WriteLine("Created CacheClient with LoggerFactory!");

        Console.WriteLine("Issuing 5 minutes of set and get requests to generate data for the dashboard example");
        for (int i = 0; i < 60*5; i++) 
        {
            string KEY = string.Format("metrics-example-{0}", i);
            await client.SetAsync("cache", KEY, "VALUE");
            await client.GetAsync("cache", KEY);
            System.Threading.Thread.Sleep(1000);
        }
    }

    private static ILoggerFactory InitializeLogging()
    {
        return Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            });
            builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
        });
    }
}
