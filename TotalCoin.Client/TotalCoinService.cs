using TotalCoin.Client.Domain;
using TotalCoin.Client.Dtos;

namespace TotalCoin.Client;

internal class TotalCoinService : ITotalCoinService
{
    private string? _token;
    private DateTime _expiresAt;

    private readonly TotalCoinOptions _options;

    public TotalCoinService(TotalCoinOptions options)
    {
        _token = null;
        _expiresAt = DateTime.MinValue;
        _options = options;
    }

    public async Task<string> Login()
    {
        using var client = new Client(_options);

        var request = new Login.Request(_options.UserName,
            _options.Password,
            _options.Company);

        var res = await client.Login(request);

        _expiresAt = DateTime.Now.AddMinutes(res.ExpiresIn - 1);
        _token = res.Token;

        return res.Token;
    }

    public async Task<Order> GetOrder(int id)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

        using var client = new Client(_options, await GetToken());

        var order = await client.GetOrder(id);

        return new Order(order.Id, order.QrCode, order.IsEnabled, order.ExpirationDate);
    }

    public async Task<Order> GenerateSingleUseQr(string concept,
        string externalReference,
        decimal amount,
        double duration = 10)
    {
        using var client = new Client(_options, await GetToken());

        var request = new CreateOrder(concept,
            DateTime.Now.AddMinutes(duration),
            externalReference,
            "dynamic",
            amount,
            "closed",
            null,
            null);

        var order = await client.CreateOrder(request);

        return new Order(order.Id, order.QrCode, order.IsEnabled, order.ExpirationDate);
    }

    public async Task<Order> GenerateSingleUseQr(string concept,
        string externalReference,
        double duration = 10)
    {
        using var client = new Client(_options, await GetToken());

        var request = new CreateOrder(concept,
            DateTime.Now.AddMinutes(duration),
            externalReference,
            "dynamic",
            null,
            "open",
            null,
            null);

        var order = await client.CreateOrder(request);

        return new Order(order.Id, order.QrCode, order.IsEnabled, order.ExpirationDate);
    }

    private async Task<string> GetToken()
    {
        return DateTime.Now <= _expiresAt ? _token! : await Login();
    }
}
