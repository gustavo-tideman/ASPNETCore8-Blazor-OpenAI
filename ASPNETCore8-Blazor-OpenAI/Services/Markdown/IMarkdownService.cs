namespace ASPNETCore8.Blazor.OpenAI.Services.Markdown
{
    public interface IMarkdownService
    {
        string ConvertToHtml(string markdown);
    }
}
