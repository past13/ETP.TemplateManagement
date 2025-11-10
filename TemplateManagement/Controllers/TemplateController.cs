using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Request;

namespace TemplateManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplateController : Controller
{
    private readonly ISender _mediator;
    
    public TemplateController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost()]
    public async Task<IActionResult> CreateTemplate([FromBody]CreateTemplateRequest request)
    {
        // await _mediator.Send(new CreateTemplateCommand(request.Title));
        
        return Ok();
    }
    
    [HttpPost("byAnchor")]
    public async Task<IActionResult> GetTemplatesByAnchor(AnchorPoint anchorPoint)
    {
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTemplate(UpdateTemplateRequest request)
    {
        // Todo: Validation should ensure attribute integrity.
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplateById(Guid id)
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTemplates()
    {
        // Todo: Returns a paginated list of all stored templates supporting sorting and filtering functionality by anchor point parts.
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTemplateById(Guid id, string title)
    {
        var result = await _mediator.Send(new GetTemplateCommand(id, title));
        
        return Ok(result);
    }
}