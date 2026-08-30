using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Dashboard.Models;
using Mafqoodi.Dashboard.Services;

namespace Mafqoodi.Dashboard.Controllers;

public sealed class AccountController(MafqoodiApiClient api) : Controller
{
    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel(string.Empty, string.Empty));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var auth = await api.LoginAsync(model.Email, model.Password);
        if (auth is null || !string.Equals(auth.Role, "admin", StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(string.Empty, "بيانات المدير غير صحيحة.");
            return View(model);
        }
        HttpContext.Session.SetString("admin_token", auth.Token);
        return RedirectToAction("Dashboard", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
