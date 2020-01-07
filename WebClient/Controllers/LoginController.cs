using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("login/{action}")]
    public class LoginController : Controller
    {
        /// <summary>
        /// 请求授权服务器.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var disco = await client.GetAsync("https://localhost:44348/");
            return new JsonResult(disco);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDocument()
        {
            var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348/");

            //var tokenEndpoint = disco.TokenEndpoint;
            //var keys = disco.KeySet.Keys;

            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                ClientId = "client",
                Address = "https://localhost:44348/",
                ClientSecret = "secret".Sha256()
            });
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("api1");
            return new JsonResult(tokenResponse);
        }

        public async Task<IActionResult> GetToken()
        {
            var client = new HttpClient();

            var disco = await client.GetAsync("https://localhost:44348/");
            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                ClientId = "client",
                ClientSecret = "secret".Sha256(),
                Address = "https://localhost:44348"
            });
            var token =await tokenClient.RequestClientCredentialsTokenAsync("api1");
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:44348/connect/token",

                ClientId = "client",
                ClientSecret = "secret".Sha256(),
                Scope = "api1"
            });

            return new JsonResult(token);
        }
    }
}