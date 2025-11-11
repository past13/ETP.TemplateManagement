using ETP.TemplatesManagement.ServiceHost.Commands;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class QueryTemplateCommandValidator : AbstractValidator<QueryTemplateCommand>
{
    public QueryTemplateCommandValidator()
    {
    }
}
