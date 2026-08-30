using System.Net.Http.Headers;
using System.Net.Http.Json;
using Mafqoodi.Dashboard.Models;

namespace Mafqoodi.Dashboard.Services;

public sealed class MafqoodiApiClient(HttpClient http, IHttpContextAccessor accessor)
{
    private void Authorize()
    {
        var token = accessor.HttpContext?.Session.GetString("admin_token");
        http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token) ? null : new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<AuthViewModel?> LoginAsync(string email, string password)
    {
        var response = await http.PostAsJsonAsync("api/auth/login", new { email, password });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<AuthViewModel>();
    }

    public async Task<DashboardStatisticsViewModel?> GetStatisticsAsync()
    {
        Authorize();
        return await http.GetFromJsonAsync<DashboardStatisticsViewModel>("api/admin/statistics");
    }

    public async Task<List<UserViewModel>> GetUsersAsync()
    {
        Authorize();
        return await http.GetFromJsonAsync<List<UserViewModel>>("api/admin/users") ?? [];
    }

    public async Task<bool> SetBanAsync(Guid id, bool isBanned)
    {
        Authorize();
        var response = await http.PatchAsJsonAsync($"api/admin/users/{id}/status", new { isBanned });
        return response.IsSuccessStatusCode;
    }
}
