using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkyCommerce.Loja.Domain.Extensions
{
    internal class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public HttpLoggingHandler(ILogger logger, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _logger = logger;
        }

        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {            
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} -   Request]";

            _logger.LogInformation($"{msg}========Start==========");
            _logger.LogInformation($"{msg} {request.Method} {request.RequestUri?.PathAndQuery} {request.RequestUri?.Scheme}/{request.Version}");
            _logger.LogInformation($"{msg} Host: {request.RequestUri?.Scheme}://{request.RequestUri?.Host}");

            foreach (var header in request.Headers)
                _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                    _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (request.Content is StringContent || this.IsTextBasedContentType(request.Headers) || this.IsTextBasedContentType(request.Content.Headers))
                {
                    var result = await request.Content.ReadAsStringAsync();

                    _logger.LogInformation($"{msg} Content:");
                    _logger.LogInformation($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");

                }
            }

            var start = DateTime.Now;

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var end = DateTime.Now;

            _logger.LogInformation($"{msg} Duration: {end - start}");
            _logger.LogInformation($"{msg}==========End==========");

            msg = $"[{id} - Response]";
            _logger.LogInformation($"{msg}=========Start=========");

            var resp = response;

            _logger.LogInformation($"{msg} {request.RequestUri?.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

            foreach (var header in resp.Headers)
                _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                    _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (resp.Content is StringContent || this.IsTextBasedContentType(resp.Headers) || this.IsTextBasedContentType(resp.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await resp.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    _logger.LogInformation($"{msg} Content:");
                    _logger.LogInformation($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
                    _logger.LogInformation($"{msg} Duration: {end - start}");
                }
            }

            _logger.LogInformation($"{msg}==========End==========");

            return response;
        }

        readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string>? values;

            if (!headers.TryGetValues("Content-Type", out values)) return false;

            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}
