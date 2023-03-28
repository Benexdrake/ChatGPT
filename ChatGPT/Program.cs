
using ChatGPT3;
using Newtonsoft.Json;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using ChatGPT3.Model;

var gpt = new OpenAIService(new OpenAiOptions()
{
    ApiKey = "INSERT_TOKEN"
});

string fileName = "save.json";

var bot = new Bot(gpt);

var keywords = File.ReadAllLines("list.txt");
List<Answer> answers = new List<Answer>();
if(File.Exists(fileName))
{
    var zwischenSpeicher = File.ReadAllText(fileName);
    answers = JsonConvert.DeserializeObject<List<Answer>>(zwischenSpeicher);
}

int i = 1;
foreach (var keyword in keywords)
{
    var a = answers.FirstOrDefault(x => x.KeyWord.Equals(keyword));
    if(a is null)
    {
        string message = $"Was ist ein: {keyword} und bitte die Erklärung in Deutsch und maximal 2 sätzen.";
        var response = await bot.SendMessageWithAnswer(message,keyword);
        if(response is not null)
        {
            answers.Add(response);
            var jsonstring = JsonConvert.SerializeObject(answers, Formatting.Indented);
            File.WriteAllText(fileName, jsonstring);
        }
        Console.WriteLine($"{i} / {keywords.Length} - {keyword} - {response.Answers[0]}");
    }
        i++;
}

List<string> list = new();
foreach (var a in answers)
{
    var word = $"{a.KeyWord};{a.Answers[0]}";
    list.Add(word);
}

var js = JsonConvert.SerializeObject(list, Formatting.Indented);
File.WriteAllLines(fileName.Replace(".json",".txt"), list);