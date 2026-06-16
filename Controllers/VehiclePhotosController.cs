using Microsoft.AspNetCore.Mvc;
using WheelPixAPI.Models;
using WheelPixAPI.Services;

namespace WheelPixAPI.Controllers;

[ApiController]
[Route("api/vehicle-photos")]
public sealed class VehiclePhotosController : ControllerBase
{
    private readonly IVehiclePhotoService _vehiclePhotoService;

    public VehiclePhotosController(IVehiclePhotoService vehiclePhotoService)
    {
        _vehiclePhotoService = vehiclePhotoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(VehiclePhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehiclePhotoResponse>> Get(
        [FromQuery] string make,
        [FromQuery] string model,
        [FromQuery] int? year,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(make) || string.IsNullOrWhiteSpace(model))
        {
            return BadRequest("The 'make' and 'model' query parameters are required.");
        }

        var request = new VehiclePhotoRequest(make.Trim(), model.Trim(), year);
        var response = await _vehiclePhotoService.GetVehiclePhotosAsync(request, cancellationToken);
        return Ok(response);
    }
}
