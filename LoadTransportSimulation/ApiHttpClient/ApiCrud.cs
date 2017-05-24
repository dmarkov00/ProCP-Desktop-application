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

        /// <summary>
        /// Method used for retrieving a single entity by id
        /// </summary>
        /// <typeparam name="T">The type of the expected result</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <param name="id">The id needed for the resource to be found</param>
        /// <returns>Entity corresponding to the passed type</returns>
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
        /// <summary>
        /// Method used for retrieving of multiple entities
        /// </summary>
        /// <typeparam name="T">Type of the expected result</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <returns>List of entities</returns>
        public async Task<List<IApiCallResult>> GetManyAsync<T>(string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            // The method doesn't have error handling
            //if (response.IsSuccessStatusCode)
            //{
            // Extracting the json string response
            result = await response.Content.ReadAsStringAsync();

            List<T> modelDeserializedFromJson = JsonConvert.DeserializeObject<List<T>>(result);

            // If it is not casted to IApiResult gives error, because of the return type of the method
            List<IApiCallResult> modelCastedToCallResult = modelDeserializedFromJson.Cast<IApiCallResult>().ToList();

            return modelCastedToCallResult;
            //}
            //else
            //{
            //    ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();

            //    return apiErrorResult;
            //}
        }
        /// <summary>
        /// Method used the create a resoursce in the api database
        /// </summary>
        /// <typeparam name="T">The type of the resource to be created</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <param name="modelData"></param>
        /// <returns></returns>
        public async Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(modelData));

            // Attaching headers
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Making post request with the converted to json data and attached headers
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

        public async Task<IApiCallResult> PutAsync<T>(string requestUri, string id, T modelData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(modelData));

            // Attaching headers
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var t = await postContent.ReadAsStringAsync();
            // Making post request to /login with the converted to json data and attached headers
            HttpResponseMessage response = await httpClient.PutAsync(requestUri + "/" + id, postContent);

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
    }
}
