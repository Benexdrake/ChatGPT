
using ChatGPT3;
using Newtonsoft.Json;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using ChatGPT3.Model;

var gpt = new OpenAIService(new OpenAiOptions()
{
    ApiKey = "Insert_Token_Here"
});

string fileName = "save.json";

var bot = new Bot(gpt);

bool again = true;

while (again)
{
    Console.WriteLine("Was möchtest du fragen? oder zum beenden x eingeben");
    var message = Console.ReadLine();
    if(message.Equals("x"))
    {
        again = false;
        continue;
    }
    Console.WriteLine("Antwort:");
    var response = await bot.SendMessageWithAnswer(message);
    if(response is not null)
    {
        Console.WriteLine($"{response}");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Etwas lief schief");
    }
}
