using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Commands;

namespace ETP.TemplatesManagement.ServiceHost.Validators;

public class QueryTemplatesCommandValidator : AbstractValidator<QueryAllTemplatesCommand>
{
    public QueryTemplatesCommandValidator()
    {
        RuleFor(x => x)
            .Must(HaveAtLeastOneProperty)
            .WithMessage("At least one search/filter property must be provided.");

        RuleFor(x => x.Count)
            .GreaterThan(0)
            .When(x => x.Count.HasValue)
            .WithMessage("Count must be greater than 0.");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .When(x => x.Page.HasValue)
            .WithMessage("Page must be greater than 0.");

        RuleFor(x => x.SortOrder)
            .Must(x => x.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                       x.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .When(x => !string.IsNullOrWhiteSpace(x.SortOrder))
            .WithMessage("SortOrder must be either 'asc' or 'desc'.");
    }
    
    private static bool HaveAtLeastOneProperty(QueryAllTemplatesCommand command)
    {
        return !string.IsNullOrWhiteSpace(command.SearchTerm) ||
               command.Count.HasValue ||
               command.Page.HasValue ||
               !string.IsNullOrWhiteSpace(command.SortColumn) ||
               !string.IsNullOrWhiteSpace(command.SortOrder);
    }
}