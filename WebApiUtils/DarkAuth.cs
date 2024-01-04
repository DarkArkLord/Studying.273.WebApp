using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WebApiUtils.ApiAddresses;
using IdentityServer4.Models;
using System.Net.Http;
using IdentityModel.Client;

namespace WebApiUtils
{
    public static class DarkAuth
    {
        public static void SetIdentityServiceConfig(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityServer(i => i.IssuerUri = ApiDictionary.IdentityServer)
                .AddDeveloperSigningCredential(true)
                .AddInMemoryApiScopes(ApiScopes)
                .AddInMemoryClients(Clients);
        }

        private static string ScopeName => "darkapi";
        private static string UserName => "main";
        private static string Password => "123456789qQ";

        private static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new(ScopeName, "For all API")
            };

        private static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = UserName,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret(Password.Sha256())},
                    AllowedScopes = { ScopeName }
                }
            };

        public static void SetApiAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.BackchannelHttpHandler = DarkHttpClient.CreateHandler();
                    options.RequireHttpsMetadata = false;
                    options.Authority = ApiDictionary.IdentityServer;
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKeyValidator = null,
                        IssuerSigningKeyResolverUsingConfiguration = null,
                        RequireSignedTokens = false,
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", ScopeName);
                });
            });
        }

        public static void SetHttpAuth(HttpClient client)
        {
            var tempClient = new HttpClient(DarkHttpClient.CreateHandler());

            var disco = tempClient.GetDiscoveryDocumentAsync(ApiDictionary.IdentityServer).Result;
            var tokenResponse = tempClient.RequestClientCredentialsTokenAsync(new()
            {
                Address = disco.TokenEndpoint,
                ClientId = UserName,
                ClientSecret = Password,
                Scope = ScopeName
            }).Result;

            client.SetBearerToken(tokenResponse.AccessToken!);
        }
    }
}
