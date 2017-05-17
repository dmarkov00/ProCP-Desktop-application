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
        public async static Task<ApiErrorResult> ConvertToApiErrorResult(this HttpResponseMessage responseResult)
        {
            ApiErrorResult apiErrorResult = new ApiErrorResult(responseResult.StatusCode.ToString(),responseResult.ReasonPhrase);

            string errorMessagesAsJson = await responseResult.Content.ReadAsStringAsync();
            List<string> errorMessagesDeserializedFromJSON = JsonConvert.DeserializeObject<List<string>>(errorMessagesAsJson);

            apiErrorResult.PopulateErrorMessages(errorMessagesDeserializedFromJSON);
            return apiErrorResult;
        }
    }
}
