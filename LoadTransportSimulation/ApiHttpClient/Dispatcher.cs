using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace ApiHttpClient
{
    public class Dispatcher
    {
        private static readonly HttpClient client = new HttpClient();
        public Dispatcher()
        {
            httpClient = new HttpClient();
            // Base address, every request builds upon it acess different resource
            httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/api/");
       
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

        public async Task<List<IApiCallResult>> GetMany<T>(string requestUri)
        {
            return await apiCRUD.GetManyAsync<T>(requestUri);
        }
        public async Task<string> AssignTruckToDriver(int truckId, string token)
        {
            var values = new Dictionary<string, string>
            {
            { "driver_id", "1" }
            };
            client.DefaultRequestHeaders.Add("api_token", token);
           
            client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://127.0.0.1:8000/api/companies/"+truckId.ToString()+"/assignTruckToDriver", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;

        }

        public async Task<IApiCallResult> Post<T>(string requestUri, T model)
        {
            return await apiCRUD.PostAsync(requestUri, model);
        }
    }
}
