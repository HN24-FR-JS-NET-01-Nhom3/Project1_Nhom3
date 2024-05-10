using System.Net.Http.Headers;
using LotteryChecker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Utils
{
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly List<string> _role;

        public CustomAuthorizeAttribute(string role)
        {
            _role = role.Split(", ").ToList();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.API_AUTHEN}/get-role");
            var accessToken = context.HttpContext.Request.Cookies["AccessToken"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var roles = JsonConvert.DeserializeObject<string[]>(content);

                    if (!_role.Any(r => (roles ?? []).Any(rr => rr == r)))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
                else
                {
                    // Nếu có lỗi khi gọi API GetRole, chuyển hướng người dùng đến trang lỗi
                    context.Result = new RedirectResult("/lottery");
                    return;
                }
            }
            else
            {
                // Nếu không có AccessToken, chuyển hướng người dùng đến trang đăng nhập
                context.Result = new ChallengeResult();
                return;
            }
        }
    }
}
