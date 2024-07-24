namespace ASPNETCore8.Blazor.OpenAI.Services.OpenAI;

public interface IGptService
{
    Task<string> ProcessAnswer(string question, string transcript);
}