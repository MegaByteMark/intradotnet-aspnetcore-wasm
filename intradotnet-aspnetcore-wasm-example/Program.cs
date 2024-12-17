using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using intradotnet_aspnetcore_wasm_example;
using IntraDotNet.AspNetCore.Wasm.BrowserConsole;
using IntraDotNet.AspNetCore.Wasm.Fetch;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Add AddBrowserConsole to the logging builder
builder.Logging.AddBrowserConsole();

//Add AddBrowserIncludeRequestCredentials to the HttpClient builder
//See example project, Pages/Home.razor for an example of using HttpClient with this handler
builder.Services.AddHttpClient("MyClient")
    .ConfigurePrimaryHttpMessageHandler<BrowserIncludeRequestCredentialsDelegatingHandler>();

await builder.Build().RunAsync();
