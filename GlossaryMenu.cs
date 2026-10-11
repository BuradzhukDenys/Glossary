namespace GlossaryTrainer
{
    internal class GlossaryMenu
    {
        private readonly Glossary glossary = new();
        private void Translation()
        {
            while (true)
            {
                Console.WriteLine("Enter english word to translate or 'back' for return to menu");

                if (Back(out string keyInput)) break;
                if (string.IsNullOrWhiteSpace(keyInput)) continue;

                if (glossary.TryGetValue(keyInput, out string? translation))
                {
                    Console.WriteLine(translation);
                }
                else
                {
                    Console.WriteLine($"Glossary don't have '{keyInput}' word");
                }
            }
        }
        private void AddNewWord()
        {
            while (true)
            {
                Console.WriteLine("Enter english word for add to glossary or 'back' for return to menu");

                if (Back(out string keyInput)) break;
                if (string.IsNullOrWhiteSpace(keyInput)) continue;

                Console.WriteLine("Enter ukrainian word for add translate to glossary or 'back' for return to menu");

                if (Back(out string valueInput)) break;
                if (string.IsNullOrWhiteSpace(valueInput)) continue;

                if (!glossary.Add(keyInput, valueInput))
                {
                    Console.WriteLine($"Glossary already have '{keyInput}' word");
                    continue;
                }
                Console.WriteLine($"A new word '{valueInput}' was added to glossary");
            }
        }
        private void DeleteWord()
        {
            while (true)
            {
                Console.WriteLine("Enter english word to delete from glossary or 'back' for return to menu");

                if (Back(out string keyInput)) break;
                if (string.IsNullOrWhiteSpace(keyInput)) continue;

                if (glossary.Remove(keyInput))
                {
                    Console.WriteLine($"Word '{keyInput}' was deleted");
                }
                else
                {
                    Console.WriteLine($"Glossary doesn't have '{keyInput}' word");
                }
            }
        }
        private void CheckWords()
        {
            foreach (var kvp in glossary.GetAll())
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }
        private void Test()
        {
            if (glossary.Count <= 0)
            {
                Console.WriteLine("Glossary is empty");
                return;
            }

            Random rnd = Random.Shared;
            int totalWords = 0;
            int correctWords = 0;

            while (true)
            {
                int elementIndex = rnd.Next(0, glossary.Count);
                string key = glossary.GetElementByIndex(elementIndex).Key;
                Console.WriteLine("Translate this word or print 'back' for return to menu:");
                Console.WriteLine(key);

                if (Back(out string valueInput)) break;
                if (string.IsNullOrWhiteSpace(valueInput)) continue;

                if (glossary.TryGetValue(key, out string? translation) && translation == valueInput)
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

            Console.WriteLine($"You answered correctly {correctWords}/{totalWords}");
        }

        private bool Back(out string input)
        {
            string answerInput = Console.ReadLine() ?? "back";
            answerInput = answerInput.ToLower();

            input = answerInput;
            if (answerInput == "back")
            {
                return true;
            }

            return false;
        }
        private void WriteToFile(FileManager filemanager)
        {
            filemanager.WriteToFile(glossary);
            Console.WriteLine("All words were successfully written to the file");
        }
        public void Run(FileManager filemanager)
        {
            foreach (var kvp in filemanager.ReadFromFile())
            {
                glossary.Add(kvp.Key, kvp.Value);
            }
            Console.WriteLine("All words were successfully read from the file");

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

                switch (menuChoose)
                {
                    case "1":
                        Translation();
                        break;
                    case "2":
                        AddNewWord();
                        WriteToFile(filemanager);
                        break;
                    case "3":
                        DeleteWord();
                        WriteToFile(filemanager);
                        break;
                    case "4":
                        CheckWords();
                        break;
                    case "5":
                        Test();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
