namespace HttpClientLibrary.HttpClientService;

public interface IHttpClientHelper
{
    Task<HttpResponseMessage> HttpDeleteAsync(string RequestUri);

    Task<List<T>> HttpGetAllAsync<T>(string RequestUri);

    Task<T> HttpGetByIdAsync<T>(string RequestUri);

    Task<HttpResponseMessage> HttpPostAsync<T>(string RequestUri, T model);

    Task<HttpResponseMessage> HttpPutAsync<T>(string RequestUri, T model);

}
