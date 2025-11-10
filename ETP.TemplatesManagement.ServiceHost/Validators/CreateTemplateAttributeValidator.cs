using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class CreateTemplateAttributeValidator : AbstractValidator<TemplateAttributeRequest>
{
    public CreateTemplateAttributeValidator()
    {
        RuleFor(x => x.AttributeName)
            .NotEmpty().WithMessage("AttributeName is required")
            .MaximumLength(100).WithMessage("AttributeName must be less than 100 characters");

        RuleFor(x => x.AttributeDescription)
            .NotEmpty().WithMessage("AttributeDescription is required")
            .MaximumLength(100).WithMessage("AttributeDescription must be less than 100 characters");
    }
}