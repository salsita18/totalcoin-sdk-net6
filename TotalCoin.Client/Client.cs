using System.Net;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json;
using TotalCoin.Client.Dtos;

namespace TotalCoin.Client;

internal class Client : HttpClient
{
    public static string InvalidUrl = "Invalid URL, please check your baseUrl configuration.";
    public static string VersionMissMatch = $"Please check TotalCoin's current version. Your client targets version: {BuildInfo.TotalCoinTarget}.";
    public static string BadAuthData = "TotalCoin rejected your credentials. Check your configuration.";
    public static string TotalCoinError = "TotalCoin couldn't handle your request.";

    public Client(TotalCoinOptions options)
    {
        BaseAddress = new Uri(options.BaseUrl);
        DefaultRequestHeaders.Add("client-type", BuildInfo.ClientType);
        DefaultRequestHeaders.Add("client-version", BuildInfo.ClientVersion);
        DefaultRequestHeaders.Add("client-target", BuildInfo.TotalCoinTarget);
    }

    public Client(TotalCoinOptions options, string token) : this(options)
    {
        DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<Login.Response> Login(Login.Request request)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, "api/auth/login");
        AddContent(message, request);

        return await ProcessResponse<Login.Response>(await SendAsync(message));
    }

    public async Task<QrOrderDto> GetOrder(int id)
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, $"api/iep/orders/{id}");

        return await ProcessResponse<QrOrderDto>(await SendAsync(message));
    }

    public async Task<QrOrderDto> CreateOrder(CreateOrder request)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, "api/iep/orders");
        AddContent(message, request);

        return await ProcessResponse<QrOrderDto>(await SendAsync(message));
    }

    private static void AddContent(HttpRequestMessage message, object content)
    {
        message.Content = new StringContent(JsonSerializer.Serialize(content));
        message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
    }

    public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        try
        {
            return base.SendAsync(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "There was an error communicating with TotalCoin. See inner exception for more info.",
                ex);
        }
    }


    private static async Task<T> ProcessResponse<T>(HttpResponseMessage responseMessage)
    {
        if (responseMessage.IsSuccessStatusCode)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(content);

            if (result is null)
            {
                throw new ApplicationException(VersionMissMatch);
            }

            return result;
        }

        if ((int)responseMessage.StatusCode is 456 or (int)HttpStatusCode.Unauthorized)
        {
            throw new AuthenticationException(BadAuthData);
        }

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
        {
            throw new HttpRequestException(InvalidUrl);
        }

        throw new HttpRequestException($"{JsonSerializer.Serialize(responseMessage)} {TotalCoinError}", null, responseMessage.StatusCode);
    }
}
