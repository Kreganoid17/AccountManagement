using System.Net.Http.Json;

namespace HttpClientLibrary.HttpClientService;

public class HttpClientHelper(HttpClient _httpClient) : IHttpClientHelper
{
    public async Task<List<T>> HttpGetAllAsync<T>(string RequestUri)
    {
        var response = await _httpClient.GetAsync(RequestUri);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<T>>();

        return result!;
    }

    public async Task<T> HttpGetByIdAsync<T>(string RequestUri)
    {
        var response = await _httpClient.GetAsync(RequestUri);
        response.EnsureSuccessStatusCode();                   

        var result = await response.Content.ReadFromJsonAsync<T>();

        return result!;
    }

    public async Task<HttpResponseMessage> HttpPostAsync<T>(string RequestUri, T model)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(RequestUri, model);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<HttpResponseMessage> HttpPutAsync<T>(string RequestUri, T model)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(RequestUri, model);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<HttpResponseMessage> HttpDeleteAsync(string RequestUri)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(RequestUri);
        response.EnsureSuccessStatusCode();

        return response;
    }
}
