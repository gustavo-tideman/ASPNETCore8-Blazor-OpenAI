using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace ASPNETCore8.Blazor.OpenAI.Services.OpenAI;

public class GptService : IGptService
{
    private readonly OpenAIOptions _openAiOptions;

    public GptService(IOptions<OpenAIOptions> openAiOptions)
    {
        _openAiOptions = openAiOptions.Value;
    }

    public async Task<string> ProcessAnswer(string question, string transcript)
    {
        try
        {
            ChatClient client = new(
                model: GPTModelExtensions.ToApiModel(GptModel.Gpt3_5Turbo),
                credential: _openAiOptions.ApiKey);

            var prompt = $@"
                        The following is a transcript of a video:
                        {transcript}
                        
                        The user asked the following question:
                        {question}
                                            
                        Follow the directives below:
                        
                        1 - Find the answer to the user's question in the transcript above and provide the answer along with the minute of the video where this information can be found.
                        2 - Every answer should be provided in pure markdown and always formatted as well as possible, highlighting relevant points, making lists, creating tables whenever possible, etc. Do not use titles with large fonts (using #, ##, etc.), only bold.
                        3 - Do not provide details in full from the transcript. Formulate the answer on your own without passing on the transcript in full or partially.
                        4 - Never provide an answer that is unrelated to the content provided. You can go beyond and use your knowledge base if the topic is related to the theme of the transcript. Otherwise, respond politely that you are limited to the video's theme (never mention that you are relying on a transcript).";

            ChatCompletion completion = await client.CompleteChatAsync(prompt);

            return completion.Content[0].Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");

            return "Sorry, we were unable to get a response at this time.Try again later.";
        }
    }
}
