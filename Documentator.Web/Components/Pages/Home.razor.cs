using Documentator.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Documentator.Web.Components.Pages;

public partial class Home : ComponentBase
{
    private readonly IDocumentatorClient _documentatorClient;

    public Home(IDocumentatorClient documentatorClient)
    {
        _documentatorClient = documentatorClient;
    }

    [BindProperty]
    public string UserInput { get; set; } = string.Empty;
    public string OutputText { get; set; } = string.Empty;

    private async Task CreateDocumentation()
    {
        DocumentMyCodeDto request = new();
        request.Messages.Add(
            new()
            {
                Role = "user",
                Content = "public int AddTwoNumbers(int a, int b) { return a+b; }"
            });

        _documentatorClient.Authenticate();
        var result = await _documentatorClient.Document(request);
        OutputText = result?.ToString();
    }
}
