using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Utils;

public class HttpUtils<TEntity> where TEntity : class
{
    private static async Task<HttpResponseMessage> Send(HttpMethod method, string url, object? data = null, string? accessToken = null)
    {
        using (var client = new HttpClient())
        {
            if (!accessToken.IsNullOrEmpty())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            // Log data type
            Console.WriteLine($"Data Type: {data?.GetType().FullName}");

            return await client.SendAsync(request);
        }
    }


    private static async Task<TEntity?> Get(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(responseData);
        }
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            // Lấy thông tin về lỗi Bad Request từ ReasonPhrase
            var reason = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Bad Request occurred: " + reason);
            return null;
        }

        // Xử lý các mã lỗi HTTP khác
        Console.WriteLine("HTTP error occurred: " + response.StatusCode);
        return null;
    }



    public static async Task<TEntity?> SendRequest(HttpMethod method, string url, object? body = null, string? accessToken = null)
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