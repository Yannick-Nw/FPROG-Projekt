using System.Reflection.Metadata.Ecma335;

namespace WarAndPeace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = ReadWholeFile("war_and_peace.txt"); //Read text of file
            string[] words = {"a", "d", "f", "c", "e", "g", "h", "b", "i" };
            RedBlackTree redBlackTree = new RedBlackTree();
            foreach (string word in words)
            {
                redBlackTree = redBlackTree.Insert(word);
            }
            Console.WriteLine(redBlackTree.CountBlack());
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);
    }
}
