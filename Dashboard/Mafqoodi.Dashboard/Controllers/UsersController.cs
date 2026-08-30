using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Dashboard.Services;

namespace Mafqoodi.Dashboard.Controllers;

public sealed class UsersController(MafqoodiApiClient api) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("admin_token")))
            return RedirectToAction("Login", "Account");
        return View(await api.GetUsersAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleBan(Guid id, bool isBanned)
    {
        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("admin_token"))) return Unauthorized();
        await api.SetBanAsync(id, isBanned);
        return RedirectToAction(nameof(Index));
    }
}
