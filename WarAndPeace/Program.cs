using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace WarAndPeace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = ReadWholeFile("war_and_peace.txt"); //Read text of file

            var stopwatchTokenizer = Stopwatch.StartNew();
            var words = Tokenizer.Tokenize(text);
            stopwatchTokenizer.Stop();
            
            //string[] words = {"a", "d", "f", "c"};
            RedBlackTree redBlackTree = new RedBlackTree();

            int totalWords = words.Count();
            int count = 0;

            Console.WriteLine("Inserting words...");
            var stopwatchInsert = Stopwatch.StartNew();
            foreach (string word in words)
            {
                redBlackTree = redBlackTree.Insert(word);
                count++;
                DrawProgressBar(count, totalWords);
            }
            stopwatchInsert.Stop();

            Console.WriteLine(); // Ensure a newline after the progress bar
            Console.WriteLine($"Black node count: {redBlackTree.CountBlack()}");
            redBlackTree.PrintTree();

            /*
            // Traverse the tree to get sorted words
            var sortedWords = RedBlackTree.InOrderTraversal();

            // Write sorted words to a file
            File.WriteAllLines("output.txt", sortedWords);

            Console.WriteLine("Sorted words written to output.txt");
            */
            Console.WriteLine($"Elapsed time for Tokenization: {stopwatchTokenizer.Elapsed}");
            Console.WriteLine($"Elapsed time for Tree Insert: {stopwatchInsert.Elapsed}");
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);

        // Method to draw a progress bar
        private static void DrawProgressBar(int progress, int total, int barLength = 50)
        {
            Console.CursorLeft = 0; // Reset the cursor position
            double percentage = (double)progress / total;
            int filledBars = (int)(percentage * barLength);
            string bar = new string('=', filledBars) + new string('-', barLength - filledBars);

            Console.Write($"[{bar}] {progress}/{total} ({percentage:P0})");
        }
    }
}
