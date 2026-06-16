using WheelPixAPI.Infrastructure.Scraping;
using WheelPixAPI.Models;

namespace WheelPixAPI.Services;

public sealed class VehiclePhotoService : IVehiclePhotoService
{
    private readonly IVehiclePhotoScraper _scraper;

    public VehiclePhotoService(IVehiclePhotoScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<VehiclePhotoResponse> GetVehiclePhotosAsync(VehiclePhotoRequest request, CancellationToken cancellationToken = default)
    {
        var photos = await _scraper.ScrapeAsync(request.Make, request.Model, request.Year, cancellationToken);
        return new VehiclePhotoResponse(request.Make, request.Model, request.Year, photos);
    }
}
