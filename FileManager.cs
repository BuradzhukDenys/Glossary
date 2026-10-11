namespace GlossaryTrainer
{
    internal class FileManager
    {
        private readonly string _glossaryFile;
        public FileManager(string path, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _glossaryFile = Path.Combine(path, fileName);
        }
        public IEnumerable<KeyValuePair<string, string>> ReadFromFile()
        {
            if (!File.Exists(_glossaryFile))
            {
                return [];
            }

            return File.ReadLines(_glossaryFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(" - "))
                .Where(parts => parts.Length == 2)
                .Select(parts => new KeyValuePair<string, string>(parts[0], parts[1]));

        }
        public void WriteToFile(Glossary glossary)
        {
            var glossaryText = glossary.GetAll().Select(kvp => $"{kvp.Key} - {kvp.Value}");

            File.WriteAllLines(_glossaryFile, glossaryText);
        }
    }
}
