using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientOAuthApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string userName = "TaxServiceClient",
                   password = "5FnXmqrNaPLxNBDh";
            string token = await GetAuthorizeToken(userName, password);
            await GetInfo<object>(token, "api/get-product/4");
        }
        public static async Task<string> GetAuthorizeToken(string userName, string password)
        {
            string accessToken = string.Empty;
            Dictionary<string, string> paramsAndValues = new Dictionary<string, string>
            {
                { "username", userName },
                { "password", password },
                { "grant_type", "password" }
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                HttpContent requestParams = new FormUrlEncodedContent(paramsAndValues);
                response = await client.PostAsync("sonatoken", requestParams).ConfigureAwait(false);
                Token tokenResponse = new Token();
                if (response.IsSuccessStatusCode)
                {
                    string jsonMessage = await response.Content.ReadAsStringAsync();
                    tokenResponse = JsonConvert.DeserializeObject<Token>(jsonMessage);
                }
                if (tokenResponse.AccessToken != null)
                    accessToken = tokenResponse.AccessToken;
            }
            return accessToken;
        }

        public static async Task<T> GetInfo<T>(string authorizeToken, string path)
        { 
            T result = default;
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(authorizeToken))
                {
                    string authorization = authorizeToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                }
                client.BaseAddress = new Uri("https://localhost:44386/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync(path).ConfigureAwait(false);
                if (response.IsSuccessStatusCode) 
                {
                   string jsonMessage = await response.Content.ReadAsStringAsync();
                   result = JsonConvert.DeserializeObject<T>(jsonMessage);
                }
            }
            return result;
        }

    }
}
