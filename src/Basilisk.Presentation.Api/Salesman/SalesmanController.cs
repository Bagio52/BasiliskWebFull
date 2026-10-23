using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Api.Salesman;
[Route("api/salesman")]
[ApiController]
public class SalesmanController : ControllerBase
{
    private readonly SalesmanService _service;

    public SalesmanController(SalesmanService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult InsertSalesman(SalesmanDto dto)
    {
        return Ok(_service.InsertSalesman(dto));
    }
}
