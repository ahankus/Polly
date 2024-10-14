class Program
{
    static async Task Main(string[] args)
    {
        // URL of the API endpoint
        string url = "https://localhost:7049/WeatherForecast/Error";

        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    // Send a GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response Data:");
                        Console.WriteLine(responseData);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}