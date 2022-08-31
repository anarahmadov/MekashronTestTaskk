using MekashronTestTaskk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Json;
using System.Web;
using System.Xml.Serialization;

namespace MekashronTestTaskk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormViewModel loginFormViewModel)
        {
            HttpClient client = new HttpClient();
            const string url = "http://isapi.mekashron.com/soapclient/soapclient.php";
            Dictionary<string, string?> param = new() { { "URL", "http://isapi.icu-tech.com/icutech-test.dll%2Fwsdl%2FIICUTech" } };
            var requestURI = new Uri(QueryHelpers.AddQueryString(url, param));
            KeyValuePair<string, string>[] keyValuePair =
            {
                new ("func", "Login"),
                new ("params", $"UserName={loginFormViewModel.UserName}&Password={loginFormViewModel.Password}")
            };

            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                using (var content = new FormUrlEncodedContent(keyValuePair))
                    response = await client.PostAsync(requestURI, content);

                object? businessResponse = await response.Content.ReadFromJsonAsync(typeof(Response));
                UserEntityResponse? userEntity = JsonSerializer.Deserialize<UserEntityResponse?>(((Response)businessResponse).ret);

                ViewBag.ResponseEntity = userEntity;

                if (userEntity?.ResultCode == -1)
                    ViewBag.IsSucceed = false;
                else
                    ViewBag.IsSucceed = true;

                return View();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message, url);

                ViewBag.IsSucceed = false;

                return View();
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}