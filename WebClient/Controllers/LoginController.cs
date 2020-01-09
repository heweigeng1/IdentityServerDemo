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
        /// 获取文档
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDocument()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348/");

            return new JsonResult(disco);
        }

        /// <summary>
        /// 获取accessToken 的方法1
        /// 普通获取方式.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAccessToken1()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348/");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            return new JsonResult(tokenResponse.AccessToken);
        }

        /// <summary>
        /// 获取accessToken的方法2
        /// 使用TokenClient获取
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAccessToken2()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348/");

            //var tokenEndpoint = disco.TokenEndpoint;
            //var keys = disco.KeySet.Keys;

            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                ClientId = "client",
                Address = disco.TokenEndpoint,
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("api1");
            return new JsonResult(tokenResponse.AccessToken);
        }

        /// <summary>
        /// 访问带有访问限制的接口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ToAuthorize()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44348/");

            //var tokenEndpoint = disco.TokenEndpoint;
            //var keys = disco.KeySet.Keys;

            var tokenClient = new TokenClient(client, new TokenClientOptions
            {
                ClientId = "client",
                Address = disco.TokenEndpoint,
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("api1");

            var apiclient = new HttpClient();
            apiclient.SetBearerToken(tokenResponse.AccessToken);
            var apiResponse = await apiclient.GetAsync("https://localhost:44370/access/getuserinfo");
            var str = await apiResponse.Content.ReadAsStringAsync();
            return new JsonResult(str);
        }
    }
}