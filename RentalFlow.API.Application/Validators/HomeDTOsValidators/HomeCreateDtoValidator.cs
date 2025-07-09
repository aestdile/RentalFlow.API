using FluentValidation;
using RentalFlow.API.Application.DTOs.HomeDTOs;

namespace RentalFlow.API.Application.Validators.HomeDTOsValidators;

public class HomeCreateDtoValidator : AbstractValidator<HomeCreateDto>
{
    public HomeCreateDtoValidator()
    {
        RuleFor(x => x.HostId)
            .GreaterThan(0).WithMessage("HostId must be a positive number.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must be at most 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters.");

        RuleFor(x => x.NoofBedRooms)
            .GreaterThanOrEqualTo(0).WithMessage("Number of bedrooms must be 0 or more.")
            .LessThanOrEqualTo(20).WithMessage("Number of bedrooms can't exceed 20.");

        RuleFor(x => x.NoofBathRooms)
            .GreaterThanOrEqualTo(0).WithMessage("Number of bathrooms must be 0 or more.")
            .LessThanOrEqualTo(20).WithMessage("Number of bathrooms can't exceed 20.");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.HomeType)
            .IsInEnum().WithMessage("HomeType must be a valid enum value.");
    }
}
