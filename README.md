# WheelPixAPI (.NET 10)

WheelPixAPI is now an ASP.NET Core (.NET 10) modular monolith API that provides vehicle photos using web scraping.

## Architecture
- **Framework**: ASP.NET Core Web API (.NET 10)
- **Modules**:
  - `Controllers` - HTTP endpoints
  - `Services` - application/business logic
  - `Infrastructure/Scraping` - HtmlAgilityPack scraping implementation
  - `Models` - request/response contracts
  - `Options` - strongly typed configuration
- **Documentation**: Swagger/OpenAPI (`/swagger`)

## Configuration
Configuration is managed through `appsettings.json`:

```json
"Scraping": {
  "SourceUrlTemplate": "https://www.bing.com/images/search?q={make}+{model}+{year}",
  "ImageXPath": "//img[@src]",
  "MaxResults": 10
}
```

## Running
```bash
dotnet restore
dotnet run
```

## API
### `GET /api/vehicle-photos?make={make}&model={model}&year={year}`
Returns scraped image URLs for the requested vehicle.
