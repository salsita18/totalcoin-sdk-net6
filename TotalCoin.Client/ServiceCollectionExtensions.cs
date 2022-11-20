using Microsoft.Extensions.DependencyInjection;

namespace TotalCoin.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTotalCoin(this IServiceCollection sc, Action<TotalCoinOptions> configureOptions)
    {
        sc.Add(new ServiceDescriptor(typeof(TotalCoinOptions),
            _ =>
            {
                var options = new TotalCoinOptions();
                configureOptions(options);
                return options;
            }, ServiceLifetime.Singleton));

        return sc.AddTransient<ITotalCoinService, TotalCoinService>();
    }
}