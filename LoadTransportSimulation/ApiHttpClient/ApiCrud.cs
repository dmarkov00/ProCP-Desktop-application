using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using System.Net.Http;
using Newtonsoft.Json;
using Common.Models;
using Common.Extensions;

namespace ApiHttpClient
{
    public class ApiCRUD
    {
        public ApiCRUD(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private HttpClient httpClient;
        private string result;
        public Task<IApiCallResult> DeleteAsync(string requestUri, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IApiCallResult> GetAsync(string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();
                // Convert from json to IApiCallResult object
                IApiCallResult modelDeserializedFromJson = JsonConvert.DeserializeObject<IApiCallResult>(result);

                return modelDeserializedFromJson;
            }
            else
            {
                ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();

                return apiErrorResult;
            }           
        }

        public Task<List<IApiCallResult>> GetManyAsync(string requestUri)
        {
            throw new NotImplementedException();
        }

        public Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData)
        {
            throw new NotImplementedException();
        }

        public Task<IApiCallResult> PutAsync<T>(string requestUri, T modelData)
        {
            throw new NotImplementedException();
        }
    }
}
