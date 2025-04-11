using Documentator.Dto;
using Microsoft.AspNetCore.Components;

namespace Documentator.Web.Components.Pages;

public partial class DocumentatorHome : ComponentBase
{
    IDocumentatorClient _documentatorClient;
    public DocumentatorHome(IDocumentatorClient documentatorClient)
    {
        _documentatorClient = documentatorClient;
    }

    private string inputString = "";
    private string outputString = "Loading";

    private async Task CreateDocumentation()
    {
        DocumentMyCodeDto request = new();
        request.Messages.Add(
            new()
            {
                Role = "user",
                Content = inputString
            });

        _documentatorClient.Authenticate();
        var result = await _documentatorClient.Document(request);
        outputString = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").ToString();
    }
}
