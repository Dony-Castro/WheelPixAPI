using WheelPixAPI.Models;

namespace WheelPixAPI.Services;

public interface IVehiclePhotoService
{
    Task<VehiclePhotoResponse> GetVehiclePhotosAsync(VehiclePhotoRequest request, CancellationToken cancellationToken = default);
}
