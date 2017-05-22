using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using System.Net.Http;
using Newtonsoft.Json;
using Common.Models;
using Common.Extensions;
using System.Linq;

namespace ApiHttpClient
{
    public class ApiCRUD
    {
        public ApiCRUD(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Add("api_token", GlobalConstants.testToken2);
        }

        private HttpClient httpClient;
        private string result;
        public Task<IApiCallResult> DeleteAsync(string requestUri, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IApiCallResult> GetAsync<T>(string requestUri, string id)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();
                // Convert from json to IApiCallResult object
                T modelDeserializedFromJson = JsonConvert.DeserializeObject<T>(result);

                return (IApiCallResult)modelDeserializedFromJson;
            }
            else
            {
                ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();

                return apiErrorResult;
            }
        }

        public async Task<IEnumerable<IApiCallResult>> GetManyAsync<T>(string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            // The method doesn't have error handling
            //if (response.IsSuccessStatusCode)
            //{
                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();
                // Convert from json to IApiCallResult object
                List<T> modelDeserializedFromJson = JsonConvert.DeserializeObject<List<T>>(result);

                IEnumerable<IApiCallResult> modelCastedToCallResult = modelDeserializedFromJson.Cast<IApiCallResult>();
                return modelCastedToCallResult;
            //}
            //else
            //{
            //    ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();

            //    return apiErrorResult;
            //}
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
