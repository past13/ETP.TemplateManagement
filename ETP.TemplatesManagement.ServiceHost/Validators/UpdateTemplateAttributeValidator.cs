using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class UpdateTemplateAttributeValidator : AbstractValidator<TemplateAttributeRequest>
{
    public UpdateTemplateAttributeValidator()
    {
        RuleFor(a => a.AttributeId)
            .NotNull().WithMessage("AttributeId cannot be null"); 

        RuleFor(a => a.AttributeName)
            .NotEmpty().WithMessage("AttributeName cannot be empty")
            .MaximumLength(10)
            .WithMessage($"AttributeName must be less than 10 characters");

        RuleFor(a => a.AttributeDescription)
            .NotEmpty().WithMessage("AttributeDescription cannot be empty")
            .MaximumLength(10)
            .WithMessage($"AttributeDescription must be less than 10 characters");
    }
}