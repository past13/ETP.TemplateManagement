using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETP.TemplatesManagement.ServiceHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplateController : Controller
{
    private readonly ISender _mediator;
    
    public TemplateController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody]CreateTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new CreateTemplateCommand(request.Title, request.AnchorPoint, request.Attributes), cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTemplateById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryTemplateCommand(id), cancellationToken);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTemplate(UpdateTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new UpdateTemplateCommand(request.Id, request.Title, request.Attributes), cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteTemplateById([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new DeleteTemplateCommand(id), cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTemplates(SearchRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryAllTemplatesCommand(request), cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("byAnchors")]
    public async Task<IActionResult> GetTemplatesByAnchor(AnchorPointsRequest anchorPointRequest, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryTemplatesByAnchorCommand(anchorPointRequest), cancellationToken);
        return Ok(result);
    }
}