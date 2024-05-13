using System.Net;
using System.Net.Http.Headers;
using System.Text;
using LotteryChecker.Common.Models.Http;
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

            return await client.SendAsync(request);
        }
    }


    private static async Task<Response<TEntity>> Get(HttpResponseMessage response)
    {
        try
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<TEntity>>(responseData);
        }
        catch (Exception ex)
        {
            throw new BadHttpRequestException(ex.Message);
        }
    }



    public static async Task<Response<TEntity>> SendRequest(HttpMethod method, string url, object? body = null, string? accessToken = null)
    {
        try
        {
            var response = await Send(method, url, body, accessToken);
            return await Get(response);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}