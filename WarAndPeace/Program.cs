using System.Reflection.Metadata.Ecma335;

namespace WarAndPeace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = ReadWholeFile("war_and_peace.txt"); //Read text of file
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);
    }
}
