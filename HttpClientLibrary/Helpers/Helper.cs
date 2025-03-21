namespace HttpClientLibrary.Helpers;

public static class Helper
{
    public static string GET = "GET";
    public static string POST = "POST";
    public static string PUT = "PUT";
    public static string DELETE = "DELETE";
    public static string HttpClientName = "HttpClientHelper";

    public static HttpResponseMessage ValidateResponse(this HttpResponseMessage response,
        string url,
        string message)
    {
        if (response.IsSuccessStatusCode)
        {
            return response;
        }
        else 
        {
            throw new HttpRequestException($"Failed to {message} from {url}. Status code: {response.StatusCode}");
        }
    }
}
