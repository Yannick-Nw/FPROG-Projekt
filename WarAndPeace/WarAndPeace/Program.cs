using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace WarAndPeace
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProcessFile();
        }
        public static void ProcessFile(string input = "war_and_peace.txt")
        {
            string text = ReadWholeFile(input); //Read text of file

            var stopwatchTokenizer = Stopwatch.StartNew();
            List<string> words = Tokenizer.Tokenize(text).ToList();
            stopwatchTokenizer.Stop();
            
            File.WriteAllLines("../../../output_tokenization.txt", words);
            ConsoleUtils.Log("Sorted words written to output_tokenization.txt");

            ConsoleUtils.Log("Inserting words...");
            var stopwatchInsert = Stopwatch.StartNew();
            // Divide words into smaller chunks
            IEnumerable<IEnumerable<string>> wordBatches = words.Chunk(1000); // Adjust batch size as needed

            // Process each batch in parallel
            List<RedBlackTree> trees = wordBatches.AsParallel().Select(TreeUtils.InsertBatch).ToList();
            
            // Merge trees into a single tree
            ConsoleUtils.Log("Merging trees...");
            RedBlackTree finalTree = TreeUtils.MergeTrees(trees);
            stopwatchInsert.Stop();

            ConsoleUtils.Log("\n"); // Ensure a newline after the progress bar
            ConsoleUtils.Log($"Black node count: {finalTree.CountBlack()}");
            //finalTree.PrintTree();
            
            // Traverse the tree to get sorted words
            IEnumerable<string> sortedWords = finalTree.InOrderTraversal();

            // Write sorted words to a file
            File.WriteAllLines("../../../output.txt", sortedWords);
            ConsoleUtils.Log("Sorted words written to output.txt");
            
            ConsoleUtils.Log($"Elapsed time for Tokenization: {stopwatchTokenizer.Elapsed}");
            ConsoleUtils.Log($"Elapsed time for Tree Insert: {stopwatchInsert.Elapsed}");
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);
    }
}