using Microsoft.VisualBasic.FileIO;
using System.Text;

void Translation(Dictionary<string, string> dict)
{
    while(true)
    {
        Console.WriteLine("Enter english word to translate or 'back' for return to menu");

        string keyInput = Console.ReadLine()!;
        keyInput = keyInput.ToLower();

        if (keyInput == "back") break;

        if (dict.ContainsKey(keyInput))
        {
            Console.WriteLine(dict[keyInput]);
        }
        else
        {
            Console.WriteLine($"Gloasry don't have '{keyInput}' word");
        }
    }
}

void AddNewWord(Dictionary<string, string> dict)
{
    while (true)
    {
        Console.WriteLine("Enter english word for add to glosary or 'back' for return to menu");
        string keyInput = Console.ReadLine()!;
        keyInput = keyInput.ToLower();
        if (keyInput == "back") break;

        if (dict.ContainsKey(keyInput))
        {
            Console.WriteLine($"Glosary already have '{keyInput}' word");
            continue;
        }

        Console.WriteLine("Enter ukrainian word for add translate to glosary or 'back' for return to menu");
        string valueInput = Console.ReadLine()!;
        valueInput = valueInput.ToLower();
        if (valueInput == "back") break;

        dict.Add(keyInput, valueInput);
        Console.WriteLine($"A new word '{valueInput}' was added to gloasary");
    }
}

void DeleteWord(Dictionary<string, string> dict)
{
    while (true)
    {
        Console.WriteLine("Enter english word to delete from glosary or 'back' for return to menu");
        string keyInput = Console.ReadLine()!;
        keyInput = keyInput.ToLower();
        if (keyInput == "back") break;

        if (dict.ContainsKey(keyInput))
        {
            dict.Remove(keyInput);
            Console.WriteLine($"Word '{keyInput}' was deleted");
        }
        else
        {
            Console.WriteLine($"Gloasey dont have '{keyInput}' word");
        }
    }
}

void CheckWords(Dictionary<string, string> dict)
{
    foreach(string key in dict.Keys)
    {
        Console.WriteLine($"{key} - {dict[key]}");
    }
}

void Test(Dictionary<string, string> dict)
{
    Random rnd = new Random();
    int totalWords = 0;
    int correctWords = 0;

    while (true)
    {
        int elementIndex = rnd.Next(0, dict.Count);
        string key = dict.ElementAt(elementIndex).Key;
        Console.WriteLine("Translate this word or print 'back' for return to menu:");
        Console.WriteLine(key);

        string answerInput = Console.ReadLine()!;
        answerInput = answerInput.ToLower();

        if (answerInput == "back") break;

        if (dict[key] == answerInput)
        {
            Console.WriteLine("You are right");
            correctWords++;
        }
        else
        {
            Console.WriteLine("Mistake");
        }
        totalWords++;
    }

    Console.WriteLine($"You answer correct {correctWords}/{totalWords}");
}

Dictionary<string, string> ReadFromFile(string filePath)
{
    var result = new Dictionary<string, string>();

    if (!File.Exists(filePath))
    {
        return result;
    }

    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        string[] keyValue = line.Split(" - ");
        result[keyValue[0]] = keyValue[1];
    }

    return result;
}

void WriteToFile(string filePath, Dictionary<string, string> dict)
{
    var glossaryText = new List<string>(dict.Count);

    foreach (string key in dict.Keys)
    {
        glossaryText.Add($"{key} - {dict[key]}");
    }

    File.WriteAllLines(filePath, glossaryText);
}

string projectDirectory = Directory.GetCurrentDirectory();
string glossaryPath = Path.Combine(projectDirectory, "Glossary");
string glossaryFile = Path.Combine(glossaryPath, "glossary.txt");

if (!Directory.Exists(glossaryPath))
{
    Directory.CreateDirectory(glossaryPath);
}

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

Dictionary<string, string> glossary = ReadFromFile(glossaryFile);

while (true)
{
    Console.Write(
        "Menu\n" +
        "1. Translate word english - ukrainian\n" +
        "2. Add new word\n" +
        "3. Delete word\n" +
        "4. Check words\n" +
        "5. Test\n" +
        "6. Exit\n");

    string menuChoose = Console.ReadLine()!;

    switch(menuChoose)
    {
        case "1":
            Translation(glossary);
            break;
        case "2":
            AddNewWord(glossary);
            WriteToFile(glossaryFile, glossary);
            break;
        case "3":
            DeleteWord(glossary);
            WriteToFile(glossaryFile, glossary);
            break;
        case "4":
            CheckWords(glossary);
            break;
        case "5":
            Test(glossary);
            break;
        case "6":
            return;
        default:
            Console.WriteLine("Unknown command");
            break;
    }
}