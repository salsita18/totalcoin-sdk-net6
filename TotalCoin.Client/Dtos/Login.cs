using System.Text.Json.Serialization;

namespace TotalCoin.Client.Dtos;

internal class Login
{
    public class Request
    {
        public string UserName { get; }
        public string Password { get; }
        public string Company { get; }

        public Request(string userName, string password, string company)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrWhiteSpace(company))
            {
                throw new ArgumentNullException(nameof(company));
            }

            UserName = userName;
            Password = password;
            Company = company;
        }
    }

    public class Response
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}