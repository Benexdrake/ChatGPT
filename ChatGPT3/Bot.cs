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

        public async Task<Answer> SendMessageWithAnswer(string message, string keyword)
        {
            var completionResult = await _gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = message,
                Model = Models.TextDavinciV2,
                Temperature = 0.5F,
                MaxTokens = 500,
                N = 1
            });

            if (completionResult.Successful)
            {
                List<string> list = new();
                var answer = new Answer();
                answer.KeyWord = keyword;

                foreach (var choice in completionResult.Choices)
                {
                    list.Add(choice.Text.Trim().Replace(@"\n\n", @"\n"));
                }
                answer.Answers = list.ToArray();
                return answer;
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
            return null;

        }
    }
}
