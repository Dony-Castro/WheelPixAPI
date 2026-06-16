namespace WheelPixAPI.Infrastructure.Scraping;

public interface IVehiclePhotoScraper
{
    Task<IReadOnlyList<string>> ScrapeAsync(string make, string model, int? year, CancellationToken cancellationToken = default);
}
