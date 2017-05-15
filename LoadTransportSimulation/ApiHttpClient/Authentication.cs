using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHttpClient
{
    public static class Authentication
    {
        public async static Task LoginUserAsync(HttpClient httpClient, Object loginData)
        {
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(loginData));
            string ab = postContent.ToString();
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync("login", postContent);


            string a = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
