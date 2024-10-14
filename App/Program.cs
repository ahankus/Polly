using Polly;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://localhost:7049/WeatherForecast/Error";

        var retryPolicyWithIncreaseTime = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, seconds => TimeSpan.FromSeconds(2),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

        var httpClient = new HttpClient();

        await retryPolicyWithIncreaseTime.ExecuteAsync(async () =>
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Request successful!");
        });

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Request successful!");
        });
    }
}