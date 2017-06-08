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
        public ApiCRUD(HttpClient httpClient, string apiToken)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Add("api_token", apiToken);
        }

        private HttpClient httpClient;
        private string result;
        /// <summary>
        /// Method used for deleting a resource by id
        /// </summary>
        /// <param name="requestUri">Access point of the resourse</param>
        /// <param name="id">Pointer to specific resource</param>
        /// <returns></returns>
        public async Task<IApiCallResult> DeleteAsync(string requestUri, string id)
        {
            // Executing delete request to the passed resource url
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                // If the entity is deleted successfully we return null
                return null;
            }
            else
            {
                ApiErrorResult apiErrorResult = await response.ConvertToApiErrorResult();
                return apiErrorResult;
            }
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
        /// Method used for retrieving of multiple entities.
        /// In the case of this method no client error is possible because it is hadled
        /// by the backend of the application, that's why there isn't such error handling present
        /// </summary>
        /// <typeparam name="T">Type of the expected result</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <returns>List of entities</returns>
        public async Task<List<IApiCallResult>> GetManyAsync<T>(string requestUri)
        {           
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();

                List<T> modelDeserializedFromJson = JsonConvert.DeserializeObject<List<T>>(result);

                // We have to cast it to ApiCallResult, to be able to return
                List<IApiCallResult> modelCastedToCallResult = modelDeserializedFromJson.Cast<IApiCallResult>().ToList();

                return modelCastedToCallResult;     
        }

        /// <summary>
        /// Method used the create a resource in the api database
        /// </summary>
        /// <typeparam name="T">The type of the resource to be created</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <param name="modelData">The sent data to be written</param>
        /// <returns>The created entity </returns>
        public async Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(modelData));

            // Attaching headers
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var t = await postContent.ReadAsStringAsync();

            // Making post request with the converted to json data and attached headers
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, postContent);
            var a = await response.Content.ReadAsStringAsync();

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

        /// <summary>
        /// Method used to update certain entity by sending put requst
        /// </summary>
        /// <typeparam name="T">The type of the expected result</typeparam>
        /// <param name="requestUri">Api entry point </param>
        /// <param name="id">Pointer to certain resource</param>
        /// <param name="modelData">The model that is being sent</param>
        /// <returns>The updated entity</returns>
        public async Task<IApiCallResult> PutAsync<T>(string requestUri, string id, T modelData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(modelData));

            // Attaching headers, requered by the api to respond correctly
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var t = await postContent.ReadAsStringAsync(); // to be deleted later on

            // Sending post request with converted to json data and attached headers
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
