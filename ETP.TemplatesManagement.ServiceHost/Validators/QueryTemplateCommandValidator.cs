using ETP.TemplatesManagement.ServiceHost.Commands;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class QueryTemplateCommandValidator : AbstractValidator<QueryTemplateCommand>
{
    public QueryTemplateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Template name is required")      
            .MaximumLength(100).WithMessage("Template name must be less than 100 characters"); 
    }
}
