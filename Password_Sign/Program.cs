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
                Console.WriteLine("error:"+disco.Exception.Message);
            }
            else
            {
                Console.WriteLine("tokenEndPoint"+disco.TokenEndpoint);
            }
            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                Address = disco.TokenEndpoint,
                ClientId = "console.client",
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestPasswordTokenAsync("test1", "123456", "consoleapi");
            if (tokenResponse.IsError)
            {
                Console.WriteLine("error:" + tokenResponse.Error);
            }
            else
            {
                Console.WriteLine("token:" + tokenResponse.AccessToken);
            }
            Console.ReadKey();
        }
    }
}
