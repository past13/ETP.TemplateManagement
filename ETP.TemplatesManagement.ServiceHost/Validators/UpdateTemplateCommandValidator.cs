using ETP.TemplatesManagement.ServiceHost.Commands;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class UpdateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Template title is required")
            .MaximumLength(Constants.TEMPLATE_TITLE_MAX_LENGTH)
            .WithMessage($"Template title must be less than {Constants.TEMPLATE_TITLE_MAX_LENGTH} characters");

        RuleFor(x => x.Attributes)
            .NotNull().WithMessage("Attributes list cannot be null")
            .Must(attrs => attrs.Any())
            .WithMessage("Template must contain at least one attribute")
            .Must(attrs => attrs.Select(a => a.AttributeId).Where(id => id != Guid.Empty).Distinct().Count() 
                           == attrs.Count(a => a.AttributeId != Guid.Empty))
            .WithMessage("All AttributeIds must be unique within the template")
            .Must(attrs => attrs.Select(a => a.AttributeName.ToLowerInvariant()).Distinct().Count() == attrs.Count)
            .WithMessage("All AttributeNames must be unique within the template");

        RuleForEach(x => x.Attributes)
            .SetValidator(new UpdateTemplateAttributeValidator());
    }
}
