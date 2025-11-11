using MediatR;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class QueryAllTemplatesCommand : IRequest<Result<List<TemplateDto>>>
{
    public string? SearchTerm { get; set; }
    public int? Count { get; set; }
    public int? Page { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    
    public QueryAllTemplatesCommand(SearchRequest request)
    {
        SearchTerm = request.SearchTerm;
        Count = request.Count;
        Page = request.Page;
        SortColumn = request.SortColumn;
        SortOrder = request.SortOrder;
    }
}