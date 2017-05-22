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
            httpClient.DefaultRequestHeaders.Add("api_token", GlobalConstants.testToken);
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

        public async Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(modelData));

            // Attaching headers
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Making post request to /login with the converted to json data and attached headers
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, postContent);

            if (response.IsSuccessStatusCode)
            {
                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();

                // Convert from json to T object
                T userDeserializedFromJSON = JsonConvert.DeserializeObject<T>(result);

                return (IApiCallResult)userDeserializedFromJSON;
            }
            else
            {
                // Calling custom extension method that converts HttpResponseMessage to ApiErrorResult
                ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();

                return apiErrorResult;
            }
        }

        public Task<IApiCallResult> PutAsync<T>(string requestUri, T modelData)
        {
            throw new NotImplementedException();
        }
    }
}
