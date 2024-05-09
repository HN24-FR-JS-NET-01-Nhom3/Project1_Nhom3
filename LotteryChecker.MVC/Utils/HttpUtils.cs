using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Utils;

public class HttpUtils<TEntity> where TEntity : class
{

    private static async Task<HttpResponseMessage> Send(HttpMethod method, string url, string? body = null, string? accessToken = null)
    {
        using (var client = new HttpClient())
        {
            if (!accessToken.IsNullOrEmpty())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);

            if (!body.IsNullOrEmpty())
            {
                request.Content = new StringContent(body ?? "");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return await client.SendAsync(request);
        }
    }

    private static async Task<TEntity?> Get(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TEntity>(responseData);
    }

    public static async Task<TEntity?> SendRequest(HttpMethod method, string url, string? body = null, string? accessToken = null)
    {
        try
        {
            var response = await Send(method, url, body, accessToken);
            return await Get(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}