using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;

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
                new ApiResource("api1", "我的 API")
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
                }
        };

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
                }
            };
        }
    }
}
