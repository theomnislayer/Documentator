namespace Documentator.Dto;

public class DocumentMyCodeDto
{
    public string model { get; set; } = "gpt-3.5-turbo";
    public List<Message> Messages { get; set; } =
    [
        new()
        {
            Role = "system",
            Content = "You create code documentation for the user."
        }
    ];
}
