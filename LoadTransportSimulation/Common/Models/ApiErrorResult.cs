using System.Collections.Generic;
namespace Common.Models
{
    public class ApiErrorResult : IApiCallResult
    {
        public ApiErrorResult(string statusCode, string reasonPhrase)
        {
            ErrorMessages = new List<string>();
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }
        public string StatusCode { get; private set; }

        //Gets or sets the reason phrase which typically is sent by servers together with the status code. 
        public string ReasonPhrase { get; private set; }

        public List<string> ErrorMessages { get; private set; }
        public void PopulateErrorMessages(List<string> errorMessages)
        {
            foreach (string errorMessage in errorMessages)
            {
                ErrorMessages.Add(errorMessage);
            }
        }

    }
}
