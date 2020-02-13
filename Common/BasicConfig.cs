using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Common
{
    public static class BasicConfig
    {
        /// <summary>
        /// API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "我的 API"),
                new ApiResource("consoleapi","控制台API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
            };
        }

        /// <summary>
        /// 客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients() => new List<Client>
        {
                new Client
                {
                    ClientId = "client",
                    // 没有交互性用户，使用 clientid/secret 实现认证。
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // 用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "console.client",
                    ClientName="console use",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    // 用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // 客户端有权访问的范围（Scopes）
                   AllowedScopes  = { "api1","consoleapi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone
                    },
                },
                new Client
                {
                    ClientId = "mvc client",
                    ClientName="mvc use",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris={ "http://localhost:5002/signin-oidc"},
                    FrontChannelLogoutUri="http://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris={"http://localhost:5002/signout-callback-oidc"},
                    AlwaysIncludeUserClaimsInIdToken=false,
                    AllowOfflineAccess=true,//是否允许离线访问
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                    }
                }
        };

        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Password="123456",
                    Username="test1"
                },
                new TestUser
                {
                    SubjectId="1",
                    Password="223456",
                    Username="test2"
                },
                new TestUser{
                SubjectId="2",
                Username="test3",
                Password="123456",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name,"tom cat"),
                        new Claim(JwtClaimTypes.FamilyName,"lao mao"),
                        new Claim(JwtClaimTypes.GivenName,"mao"),
                        new Claim(JwtClaimTypes.PhoneNumber,"13435959470"),
                        new Claim(JwtClaimTypes.Address,"bei jing"),
                        new Claim(JwtClaimTypes.Email,"laomao@163.com")
                    }
                },
            };
        }
    }
}
