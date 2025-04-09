using System.Collections.Concurrent;

namespace Documentator.Core;

public class HttpClientFactory
{
    private readonly ConcurrentDictionary<string, System.Net.Http.HttpClient> _clients;

    public HttpClientFactory()
    {
        _clients = new ConcurrentDictionary<string, System.Net.Http.HttpClient>();
    }

    public System.Net.Http.HttpClient Get(string baseAddress, bool useDefaultCredentials = false)
    {
        var key = useDefaultCredentials ? $"{baseAddress}-ntlm" : baseAddress;
        if (_clients.ContainsKey(key)) return _clients[key];

        var handler = new HttpClientHandler
        {
            //UseCookies = false,
            UseCookies = true,
            AllowAutoRedirect = false,
            UseDefaultCredentials = useDefaultCredentials
        };
        var client = new System.Net.Http.HttpClient(handler)
        {
            BaseAddress = new Uri(baseAddress)
        };

        _clients.TryAdd(key, client);
        return client;
    }
}