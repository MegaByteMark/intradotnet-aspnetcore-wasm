using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using intradotnet_aspnetcore_wasm_example;
using IntraDotNet.AspNetCore.Wasm.BrowserConsole;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Add AddBrowserConsole to the logging builder
builder.Logging.AddBrowserConsole();

await builder.Build().RunAsync();
