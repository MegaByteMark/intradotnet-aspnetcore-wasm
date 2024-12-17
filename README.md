# IntraDotNet.AspNetCore.Wasm

WebAssembly polyfill for enabling Windows features when targeting an intranet environment.

## Features

- Browser console logging
- HTTP request handling with browser credentials

## Installation

To install the package using the `dotnet` CLI, run the following command:

```sh
dotnet add package IntraDotNet.AspNetCore.Wasm
```

## Usage

### Browser Console Logging
You can add browser console logging to your ASP.NET Core application by using the AddBrowserConsole extension method.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IntraDotNet.AspNetCore.Wasm.BrowserConsole;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddBrowserConsole(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.Run();
```
#### Configuration
You can configure the logging level in your appsettings.json file:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "None",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

### HTTP Request Handling with Browser Credentials
You can use the BrowserIncludeRequestCredentialsDelegatingHandler to include browser request credentials in your HTTP requests. Register the handler with dependency injection and use it in your services.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using IntraDotNet.AspNetCore.Wasm.Fetch;

var builder = WebApplication.CreateBuilder(args);

//Add AddBrowserIncludeRequestCredentials to the HttpClient builder
//See example project, Pages/Home.razor for an example of using HttpClient with this handler
builder.Services.AddHttpClient("MyClient")
    .ConfigurePrimaryHttpMessageHandler<BrowserIncludeRequestCredentialsDelegatingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.Run();
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.