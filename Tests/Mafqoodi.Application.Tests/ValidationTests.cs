using Mafqoodi.Application.DTOs;
using Mafqoodi.Application.Validation;

namespace Mafqoodi.Application.Tests;

public sealed class ValidationTests
{
    [Fact]
    public async Task Register_requires_valid_email_and_password()
    {
        var validator = new RegisterRequestValidator();
        var result = await validator.ValidateAsync(new RegisterRequest("User", "bad", "123", null, "personal"));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == "Email");
        Assert.Contains(result.Errors, x => x.PropertyName == "Password");
    }

    [Fact]
    public async Task Report_requires_supported_type()
    {
        var validator = new CreateReportRequestValidator();
        var result = await validator.ValidateAsync(new CreateReportRequest("x", "desc", "loc", null, null, "other", null, null, null, null, null));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == "ReportType");
    }
}
