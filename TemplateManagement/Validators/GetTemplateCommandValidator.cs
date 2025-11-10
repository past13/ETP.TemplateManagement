using FluentValidation;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Entities;

namespace TemplateManagement.Validators;

public class GetTemplateCommandValidator : AbstractValidator<GetTemplateCommand>
{
    public GetTemplateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Template name is required")      
            .MaximumLength(100).WithMessage("Template name must be less than 100 characters"); 
    }
}
