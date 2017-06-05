using Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Gets an HttpResponseMessage object, extracts some properties and fills the up 
        /// in an ApiErrorResult object
        /// </summary>
        /// <param name="responseResult">The passed HttpResponseMessage </param>
        /// <returns>ApiErrorResult object</returns>
        public async static Task<ApiErrorResult> ConvertToApiErrorResult(this HttpResponseMessage responseResult)
        {          
            ApiErrorResult apiErrorResult = new ApiErrorResult(((int)responseResult.StatusCode).ToString(),responseResult.ReasonPhrase);

            // Gets error messages as JSON string
            string errorMessagesAsJson = await responseResult.Content.ReadAsStringAsync();

            // Convert error messages to list of strings
            List<string> errorMessagesDeserializedFromJSON = JsonConvert.DeserializeObject<List<string>>(errorMessagesAsJson);

            apiErrorResult.PopulateErrorMessages(errorMessagesDeserializedFromJSON);

            return apiErrorResult;
        }
    }
}
