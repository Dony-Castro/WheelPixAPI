namespace WheelPixAPI.Models;

public sealed record VehiclePhotoResponse(string Make, string Model, int? Year, IReadOnlyList<string> Photos);
