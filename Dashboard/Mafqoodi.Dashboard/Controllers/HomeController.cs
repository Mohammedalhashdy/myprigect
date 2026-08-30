using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Dashboard.Models;
using Mafqoodi.Dashboard.Services;

namespace Mafqoodi.Dashboard.Controllers;

public sealed class HomeController(MafqoodiApiClient api) : Controller
{
    [HttpGet]
    public IActionResult Index() => RedirectToAction("Login", "Account");

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        if (!IsAuthenticated()) return RedirectToAction("Login", "Account");
        var stats = await api.GetStatisticsAsync();
        return View(stats ?? new DashboardStatisticsViewModel(0, 0, 0, 0));
    }

    private bool IsAuthenticated() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("admin_token"));
}
