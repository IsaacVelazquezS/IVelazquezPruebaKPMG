using DL;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

public class LoginController : Controller
{
    private readonly HttpClient _httpClient;

    public LoginController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5213/");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO Login)
    {
        if (string.IsNullOrWhiteSpace(Login.Username) ||
            string.IsNullOrWhiteSpace(Login.Password))
        {
            ViewBag.Error = "Debe ingresar usuario y contraseña";
            return View(Login);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(Login),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            "api/auth/login",
            content
        );

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var token = JsonDocument
                .Parse(json)
                .RootElement
                .GetProperty("token")
                .GetString();

            Response.Cookies.Append(
                "AuthToken",
                token!,
                new CookieOptions
                {
                    HttpOnly = true,                
                    Secure = false,                 
                    SameSite = SameSiteMode.Strict, 
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                }
            );

            return RedirectToAction("Dashboard", "Dashboard");
        }
        else
        {
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(Login);
        }
    }
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login");
    } 
    public IActionResult Forbidden()
    {
        return View();
    }
}
