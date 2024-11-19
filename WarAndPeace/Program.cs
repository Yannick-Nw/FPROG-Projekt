using System.Reflection.Metadata.Ecma335;

namespace WarAndPeace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = ReadWholeFile("war_and_peace.txt"); //Read text of file
            string[] words = text
            .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => new string(word.Where(char.IsLetterOrDigit).ToArray()))
            .Where(word => !string.IsNullOrEmpty(word))
            .ToArray();
            RedBlackTree redBlackTree = new RedBlackTree();
            foreach (string word in words)
            {
                redBlackTree = redBlackTree.Insert(word);
            }
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);
    }
}
