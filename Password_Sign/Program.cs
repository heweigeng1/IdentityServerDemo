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
                Console.WriteLine("error:" + disco.Exception.Message);
            }
            else
            {
                Console.WriteLine("tokenEndPoint" + disco.TokenEndpoint);
            }
            TokenResponse tokenResponse = await RequestToken(client, disco);
            //第二种方式
            //TokenResponse tokenResponse =  await RequestToken2(client, disco);
            if (tokenResponse.IsError)
            {
                Console.WriteLine("error:" + tokenResponse.Error);
            }
            else
            {
                Console.WriteLine("token:" + tokenResponse.AccessToken);
            }
            Console.WriteLine("访问资源!");
            var apiclient = new HttpClient();
            apiclient.SetBearerToken(tokenResponse.AccessToken);
            var apiResponse = await apiclient.GetAsync("https://localhost:44370/access/getuserinfo");
            var str = await apiResponse.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            Console.ReadKey();
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="client"></param>
        /// <param name="disco"></param>
        /// <returns></returns>
        private static async Task<TokenResponse> RequestToken2(HttpClient client, DiscoveryDocumentResponse disco)
        {
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                UserName = "test1",
                Password = "123456",
                ClientId = "console.client",
                ClientSecret = "secret",
                Scope = "api1",
            });
            return tokenResponse;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="client"></param>
        /// <param name="disco"></param>
        /// <returns></returns>
        private static async Task<TokenResponse> RequestToken(HttpClient client, DiscoveryDocumentResponse disco)
        {
            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                Address = disco.TokenEndpoint,
                ClientId = "console.client",
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestPasswordTokenAsync("test1", "123456", "api1");
            return tokenResponse;
        }
    }
}
