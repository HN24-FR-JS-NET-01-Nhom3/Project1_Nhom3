using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Utils;

public class HttpUtils<TEntity> where TEntity : class
{
	public static async Task<IEnumerable<TEntity>?> GetAll(string url, string accessToken)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("Content-Type", "application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			try
			{
				var response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
	
	public static async Task<TEntity?> Get(string url, string accessToken)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("Content-Type", "application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			try
			{
				var response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}

	public static async Task<TEntity?> Post(string url, string body, string accessToken)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("Content-Type", "application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Post, url);
				request.Content = new StringContent(body);
				
				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
	
	public static async Task<TEntity?> Put(string url, string body, string accessToken)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("Content-Type", "application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Put, url);
				request.Content = new StringContent(body);
				
				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
	
	public static async Task<TEntity?> Delete(string url, string accessToken)
	{
		using (var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("Content-Type", "application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Delete, url);
				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
}