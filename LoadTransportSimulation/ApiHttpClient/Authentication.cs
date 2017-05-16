using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHttpClient
{
    public static class Authentication
    {
        /// <summary>
        /// Used to login a certain user in the app
        /// </summary>
        /// <param name="httpClient">Instance of HttpClient</param>
        /// <param name="loginData">Data sent from the form</param>
        /// <returns>Returns an instance of the user that is logged or error message</returns>
        public async static Task<Object> LoginUserAsync(HttpClient httpClient, Object loginData)
        {
            // Converting the form data from c# object to json
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(loginData));

            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Making post request to /login with the converted to json data
            HttpResponseMessage response = await httpClient.PostAsync("login", postContent);

            // Extracting the json string response
            string result = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                // Convert from json to User object
                User userDeserializedFromJSON = JsonConvert.DeserializeObject<User>(result);

                return userDeserializedFromJSON;
            }
            else
            {
                // TODO: implement extension method for error handling
                return new { };
            }
        }
    }
}
