using Formula1Import.Contracts.ExternalServices;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Formula1Import.Infrastructure.ExternalApis;

public class SlackClient(
    HttpClient httpClient,
    IConfiguration configuration)
    : ISlackClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public void SendMessage(string message)
    {
        var jsonPayload = JsonSerializer.Serialize(new { text = message });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        Task.Run(async () =>
        {
            await _httpClient.PostAsync(_configuration["SlackUrl"], content);
        });
    }
}
