using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiHttpClient
{
    public interface IApiCRUD
    {
        Task<IApiCallResult> GetAsync(string requestUri);
        Task<List<IApiCallResult>> GetManyAsync(string requestUri);
        Task<IApiCallResult> PostAsync<T>(string requestUri, T modelData);
        Task<IApiCallResult> PutAsync<T>(string requestUri, T modelData);
        Task<IApiCallResult> DeleteAsync(string requestUri, int id);
    }
}
