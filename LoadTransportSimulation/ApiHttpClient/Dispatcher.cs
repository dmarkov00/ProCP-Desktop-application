using System;
using System.Net.Http;
using Models;
using System.Threading.Tasks;

namespace ApiHttpClient
{
    public class Dispatcher
    {
        private static HttpClient httpClient;
        public Dispatcher()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/api/");
        }
        public async Task<string> LoginUser(Object loginData)
        {
            var a = await Authentication.LoginUserAsync(httpClient, loginData);
            return a;
        }
    }
}
