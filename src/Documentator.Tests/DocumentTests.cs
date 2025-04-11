using Documentator.Dto;
using Shouldly;

namespace Documentator.Tests;

[TestClass]
public class DocumentTests : BaseTest
{
    private IDocumentatorClient _documentatorClient;
    protected override void Setup()
    {
        _documentatorClient = GetService<IDocumentatorClient>();
    }

    [TestMethod]
    public async Task DocumentTest1()
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
        Console.WriteLine(result.ToString());
        result.ToString().ShouldContain("Add");
    }
}
