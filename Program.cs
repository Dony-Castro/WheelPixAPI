using WheelPixAPI.Infrastructure.Scraping;
using WheelPixAPI.Options;
using WheelPixAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ScrapingOptions>(
    builder.Configuration.GetSection(ScrapingOptions.SectionName));

builder.Services.AddHttpClient<IVehiclePhotoScraper, HtmlAgilityVehiclePhotoScraper>();
builder.Services.AddScoped<IVehiclePhotoService, VehiclePhotoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
