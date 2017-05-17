using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Common.Extensions;
using Common.Models;
namespace ApiHttpClient
{
    public static class Authentication
    {
        private static string result;
        /// <summary>
        /// Used to login a certain user in the app
        /// </summary>
        /// <param name="httpClient">Instance of HttpClient</param>
        /// <param name="loginData">Data sent from the form</param>
        /// <returns>Returns an instance of the user that is logged or error message</returns>
        public async static Task<IApiCallResult> LoginUserAsync(HttpClient httpClient, Object loginData)
        {
            // Converting the form data from c# object to json and setting it in the content to be sent
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(loginData));

            // Attaching headers
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Making post request to /login with the converted to json data and attached headers
            HttpResponseMessage response = await httpClient.PostAsync("login", postContent);

            if (response.IsSuccessStatusCode)
            {
                // Extracting the json string response
                result = await response.Content.ReadAsStringAsync();

                // Convert from json to User object
                User userDeserializedFromJSON = JsonConvert.DeserializeObject<User>(result);

                return userDeserializedFromJSON;
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
