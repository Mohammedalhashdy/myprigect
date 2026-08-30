using FluentValidation;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.Validation;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(128);
        RuleFor(x => x.PhoneNumber).MaximumLength(30);
        RuleFor(x => x.AccountType).NotEmpty().MaximumLength(30);
    }
}

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public sealed class CreateReportRequestValidator : AbstractValidator<CreateReportRequest>
{
    public CreateReportRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(4000);
        RuleFor(x => x.LocationName).MaximumLength(300);
        RuleFor(x => x.ReportType).Must(x => x is "lost" or "found" or "مفقود" or "معثور");
        RuleFor(x => x.RewardAmount).GreaterThanOrEqualTo(0).When(x => x.RewardAmount.HasValue);
        RuleFor(x => x.ImageData).MaximumLength(10_000_000).When(x => x.ImageData is not null);
    }
}
