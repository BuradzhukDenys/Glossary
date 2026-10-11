namespace GlossaryTrainer
{
    internal class Glossary
    {
        private readonly Dictionary<string, string> _words = [];
        public int Count => _words.Count;
        public bool TryGetValue(string englishWord, out string? translation)
            => _words.TryGetValue(englishWord.ToLowerInvariant(), out translation);
        public bool Add(string englishWord, string translation)
        {
            if (_words.ContainsKey(englishWord.ToLowerInvariant())) return false;
            _words.Add(englishWord.ToLowerInvariant(), translation);
            return true;
        }
        public bool Remove(string englishWord) => _words.Remove(englishWord.ToLowerInvariant());
        public IEnumerable<KeyValuePair<string, string>> GetAll()
        {
            return _words.AsEnumerable();
        }
        public KeyValuePair<string, string> GetElementByIndex(int index) => _words.ElementAt(index);
    }
}
