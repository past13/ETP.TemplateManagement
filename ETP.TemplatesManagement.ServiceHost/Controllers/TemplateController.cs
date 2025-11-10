using ETP.TemplatesManagement.SDK.Dto;
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
    public async Task<IActionResult> CreateTemplate([FromBody]CreateTemplateRequest request)
    {
        var result = await _mediator.Send(new CreateTemplateCommand(request.Title, request.AnchorPoint, request.Attributes));
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTemplateById(Guid id)
    {
        var result = await _mediator.Send(new QueryTemplateCommand(id));
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTemplate(UpdateTemplateRequest request)
    {
        var result = await _mediator.Send(new UpdateTemplateCommand(request.Id, request.Title, request.Attributes));
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteTemplateById([FromQuery] Guid id)
    {
        var result = await _mediator.Send(new DeleteTemplateCommand(id));
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTemplates(SearchAnchorPointRequest request)
    {
        // Todo: Returns a paginated list of all stored templates supporting sorting and filtering functionality by anchor point parts.
        // var result = await _mediator.Send(new QueryTemplatesCommand(request));
        return Ok(null);
    }
    
    [HttpPost("byAnchor")]
    public async Task<IActionResult> GetTemplatesByAnchor(AnchorPointRequest anchorPoint)
    {
        return Ok();
    }
}