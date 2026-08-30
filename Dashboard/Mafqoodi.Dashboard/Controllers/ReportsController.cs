using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Dashboard.Services;

namespace Mafqoodi.Dashboard.Controllers;

public sealed class ReportsController(MafqoodiApiClient api) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!IsAuthenticated()) return RedirectToAction("Login", "Account");
        return View(await api.GetReportsAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(Guid id, string status)
    {
        if (!IsAuthenticated()) return Unauthorized();
        await api.SetReportStatusAsync(id, status);
        return RedirectToAction(nameof(Index));
    }

    private bool IsAuthenticated() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("admin_token"));
}
