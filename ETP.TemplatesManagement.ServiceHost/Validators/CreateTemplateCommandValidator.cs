using ETP.TemplatesManagement.ServiceHost.Commands;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
{
    public CreateTemplateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Template name is required")      
            .MaximumLength(100).WithMessage("Template name must be less than 100 characters"); 
        
        //Todo: Add Anchor Validation
        
        RuleFor(x => x.Attributes)
            .NotNull().WithMessage("Attributes list cannot be null")
            .Must(attrs => attrs != null && attrs.Select(a => a.AttributeName).Distinct().Count() == attrs.Count)
            .WithMessage("All AttributeIds must be unique within the template");
        
        RuleForEach(x => x.Attributes)
            .SetValidator(new CreateTemplateAttributeValidator());
    }
}
