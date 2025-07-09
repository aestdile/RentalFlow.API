using FluentValidation;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;

namespace RentalFlow.API.Application.Validators.HomeRequestDTOsValidators;

public class HomeRequestDtoValidator : AbstractValidator<HomeRequestDto>
{
    public HomeRequestDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.GuestId)
            .GreaterThan(0).WithMessage("GuestId must be greater than 0.");

        RuleFor(x => x.HomeId)
            .GreaterThan(0).WithMessage("HomeId must be greater than 0.");

        RuleFor(x => x.RequestMessage)
            .MaximumLength(1000).WithMessage("Request message can't exceed 1000 characters.");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date must be today or later.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
    }
}
