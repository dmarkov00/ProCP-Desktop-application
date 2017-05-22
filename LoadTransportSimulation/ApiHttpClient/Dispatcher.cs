using System;
using System.Net.Http;
using Models;
using System.Threading.Tasks;
using Common;
using System.Net.Http.Headers;

namespace ApiHttpClient
{
    public class Dispatcher
    {
        public Dispatcher()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/api/");
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            apiCRUD = new ApiCRUD(httpClient);

        }
        private static HttpClient httpClient;
        private static ApiCRUD apiCRUD;
        public async Task<IApiCallResult> LoginUser(Object loginData)
        {
            return await Authentication.LoginUserAsync(httpClient, loginData);
        }
        public async Task<IApiCallResult> Get<T>(string requestUri, string id)
        {
            return await apiCRUD.GetAsync<T>(requestUri, id);
        }


    }
}
