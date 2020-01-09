using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Password_Sign
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Exception.Message);
            }
            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                Address = disco.TokenEndpoint,
                ClientId = "console client",
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestPasswordTokenAsync("test1", "123456", "consoleapi");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            Console.WriteLine(tokenResponse.AccessToken);
        }
    }
}
