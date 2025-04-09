using System.Net;
using System.Net.Http.Headers;

namespace Documentator.Core;

public interface IHttpClient
{
    Uri BaseAddress { get; }
    CookieContainer CookieContainer { get; set; }
    HttpRequestHeaders DefaultRequestHeaders { get; }
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}

public class HttpClient : IHttpClient
{
    protected System.Net.Http.HttpClient Client;
    public Uri BaseAddress => Client.BaseAddress;
    public CookieContainer CookieContainer { get; set; }
    public HttpRequestHeaders DefaultRequestHeaders => Client.DefaultRequestHeaders;

    public HttpClient() => CookieContainer = new CookieContainer();

    public HttpClient(System.Net.Http.HttpClient client) : this()
    {
        Client = client;
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => SendAsync(request, default);

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await Client.SendAsync(request, cancellationToken);
        if (response.Headers.TryGetValues("Set-Cookie", out var cookieHeaders))
        {
            foreach (var cookieHeader in cookieHeaders)
            {
                CookieContainer.SetCookies(request.RequestUri, cookieHeader);
            }
        }
        return response;
    }
}
