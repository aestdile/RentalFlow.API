﻿using FluentValidation;
using RentalFlow.API.Application.DTOs.HostDTOs;

namespace RentalFlow.API.Application.Validators.HostDTOsValidators;

public class HostUpdateDtoValidator : AbstractValidator<HostUpdateDto>
{
    public HostUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Date of birth is too old.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998\d{9}$").WithMessage("Phone number must be in format +998XXXXXXXXX");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender must be a valid enum value.");
    }
}
