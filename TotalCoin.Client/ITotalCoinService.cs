using System.Security.Authentication;
using TotalCoin.Client.Domain;

namespace TotalCoin.Client;

public interface ITotalCoinService
{
    /// <returns>Auth token.</returns>
    /// <summary>
    /// Enforces a Login. Refresh the token.
    /// </summary>
    /// <exception cref="AuthenticationException">
    /// Thrown when login data is not valid for the environment.
    /// </exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when TotalCoin URL is not correct or when TotalCoin has an internal problem.
    /// </exception>
    /// <exception cref="ApplicationException">
    /// Thrown when TotalCoin version differs from the client target version.
    /// </exception>
    public Task<string> Login();

    /// <summary>
    /// Creates an closed amount Single use order.
    /// </summary>
    /// /// <param name="externalReference">A value to identify the order on your system.</param>
    /// <param name="amount">Fixed amount of the QrCode.</param>
    /// <param name="concept">The concept to be paid.</param>
    /// <param name="duration">Duration in minutes of the order.</param>
    /// <returns>Id of the Order and raw QrCode.</returns>
    /// <exception cref="AuthenticationException">
    /// Thrown when login data is not valid for the environment.
    /// </exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when TotalCoin URL is not correct or when TotalCoin has an internal problem.
    /// </exception>
    /// <exception cref="ApplicationException">
    /// Thrown when TotalCoin version differs from the client target version.
    /// </exception>
    public Task<Order> GenerateSingleUseQr(string concept,
        string externalReference,
        decimal amount,
        double duration = 10);

    /// <summary>
    /// Creates an open amount Single use order.
    /// </summary>
    /// /// <param name="externalReference">A value to identify the order on your system.</param>
    /// <param name="concept">The concept to be paid.</param>
    /// <param name="duration">Duration in minutes of the order.</param>
    /// <returns>Id of the Order and raw QrCode.</returns>
    /// <exception cref="AuthenticationException">
    /// Thrown when login data is not valid for the environment.
    /// </exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when TotalCoin URL is not correct or when TotalCoin has an internal problem.
    /// </exception>
    /// <exception cref="ApplicationException">
    /// Thrown when TotalCoin version differs from the client target version.
    /// </exception>
    public Task<Order> GenerateSingleUseQr(string concept,
        string externalReference,
        double duration = 10);

    /// <summary>
    /// Gets the data of an existing order, even if its enabled or not.
    /// </summary>
    /// /// <param name="id">The id of an existing order.</param>
    /// <returns>Id of the Order and raw QrCode.</returns>
    /// <exception cref="AuthenticationException">
    /// Thrown when login data is not valid for the environment.
    /// </exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when TotalCoin URL is not correct or when TotalCoin has an internal problem.
    /// </exception>
    /// <exception cref="ApplicationException">
    /// Thrown when TotalCoin version differs from the client target version.
    /// </exception>
    public Task<Order> GetOrder(int id);
}