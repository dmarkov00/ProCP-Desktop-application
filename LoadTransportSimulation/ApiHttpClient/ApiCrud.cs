using System;
using System.Threading.Tasks;
using Common;

namespace ApiHttpClient
{
    public static class ApiCrud : IApiCRUD
    {
        public async static Task<IApiCallResult> DeleteAsync(string requestUri, int id)
        {
            throw new NotImplementedException();
        }

        public Task<IApiCallResult> GetAsync(string requestUri)
        {
            throw new NotImplementedException();
        }

        public async static Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData)
        {
            throw new NotImplementedException();
        }

        public Task<IApiCallResult> PutAsync<T>(string requestUri, T modelData)
        {
            throw new NotImplementedException();
        }
    }
}
