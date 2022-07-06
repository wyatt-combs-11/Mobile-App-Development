using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WordMobileApp
{
    public class EnglishDictionary
    {
        public HashSet<string> Words { get; set; }
        public Task Load { get; set; }

        public EnglishDictionary()
        {
            Words = new HashSet<string>();
            Load = loadDictionaryAsync();
        }

        public async Task loadDictionaryAsync()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("WordMobileApp.spellboundWords.txt");
            using (StreamReader fin = new StreamReader(stream))
            {
                while (!fin.EndOfStream)
                {
                    string line = await fin.ReadLineAsync().ConfigureAwait(false);
                    Words.Add(line);
                }
            }
            Console.WriteLine(Words.Count);
        }

        public bool possibleWord(string word)
        {
            if (!Load.IsCompleted && Load != null)
                Load.Wait();
            return Words.Contains(word);
        }
    }
}
