﻿using FluentValidation;

namespace Products.Application.Categories.Commands.Validations
{
    public class CategoryValidation<T> : AbstractValidator<T>  where T : CategoryCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .LessThanOrEqualTo(0);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }
    }
}
