using FluentValidation;
using RentalFlow.API.Application.DTOs.HomeDTOs;

namespace RentalFlow.API.Application.Validators.HomeDTOsValidators;

public class HomeDtoValidator : AbstractValidator<HomeDto>
{
    public HomeDtoValidator()
    {
        RuleFor(x => x.HostId)
            .GreaterThan(0).WithMessage("HostId must be a valid positive number.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address can't exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters.");

        RuleFor(x => x.NoofBedRooms)
            .InclusiveBetween(0, 20).WithMessage("Bedrooms must be between 0 and 20.");

        RuleFor(x => x.NoofBathRooms)
            .InclusiveBetween(0, 20).WithMessage("Bathrooms must be between 0 and 20.");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.HomeType)
            .IsInEnum().WithMessage("HomeType is invalid.");
    }
}
