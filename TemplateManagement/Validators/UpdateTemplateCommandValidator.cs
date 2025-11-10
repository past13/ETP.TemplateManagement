using FluentValidation;
using TemplateManagement.Commands;

namespace TemplateManagement.Validators;

public class UpdateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Template name is required")      
            .MaximumLength(Constants.TEMPLATE_TITLE_MAX_LENGTH).WithMessage("Template name must be less than 100 characters"); 
        
        RuleFor(x => x.Attributes)
            .NotNull().WithMessage("Attributes list cannot be null")
            .Must(attrs => attrs != null && attrs.Select(a => a.AttributeName).Distinct().Count() == attrs.Count)
            .WithMessage("All AttributeIds must be unique within the template");

        RuleForEach(x => x.Attributes)
            .SetValidator(new CreateTemplateAttributeValidator());
    }
}
