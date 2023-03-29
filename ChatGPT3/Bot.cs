using ChatGPT3.Model;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace ChatGPT3
{
    public class Bot
    {
        private readonly OpenAIService _gpt3;
        public Bot(OpenAIService gpt)
        {
            _gpt3 = gpt;
        }

        public async Task<string> SendMessageWithAnswer(string message)
        {
            var completionResult = await _gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = message,
                Model = Models.TextDavinciV3,
                Temperature = 0.5F,
                MaxTokens = 1000,
                N = 1
            });

            if (completionResult.Successful)
            {
                return completionResult.Choices[0].Text.Trim();
            }
            return null;
        }

     
    }
}
