using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace IntraDotNet.AspNetCore.Wasm.Fetch
{
    /// <summary>
    /// A delegating handler that includes browser request credentials in HTTP requests.
    /// </summary>
    public class BrowserIncludeRequestCredentialsDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserIncludeRequestCredentialsDelegatingHandler"/> class.
        /// </summary>
        public BrowserIncludeRequestCredentialsDelegatingHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        /// <summary>
        /// Sends an HTTP request with browser request credentials included.
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>The HTTP response message.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return base.SendAsync(request, cancellationToken);
        }
    }
}