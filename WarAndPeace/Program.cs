using System.Collections.Concurrent;
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
            File.WriteAllLines("../../../output_tokenization.txt", words);

            Console.WriteLine("Sorted words written to output_tokenization.txt");

            //string[] words = {"a", "d", "f", "c"};
            RedBlackTree redBlackTree = new RedBlackTree();

            Console.WriteLine("Inserting words...");
            var stopwatchInsert = Stopwatch.StartNew();
            // Divide words into smaller chunks
            var wordBatches = words.Chunk(1000); // Adjust batch size as needed

            // Process each batch in parallel
            var trees = wordBatches.AsParallel()
                .Select(batch => TreeUtils.InsertBatch(batch))
                .ToList();

            // Merge trees into a single tree
            Console.WriteLine("Merging trees");
            RedBlackTree finalTree = TreeUtils.MergeTrees(trees);

            stopwatchInsert.Stop();

            Console.WriteLine(); // Ensure a newline after the progress bar
            Console.WriteLine($"Black node count: {finalTree.CountBlack()}");
            //finalTree.PrintTree();

            
            // Traverse the tree to get sorted words
            var sortedWords = finalTree.InOrderTraversal();

            // Write sorted words to a file
            File.WriteAllLines("../../../output.txt", sortedWords);

            Console.WriteLine("Sorted words written to output.txt");
            
            Console.WriteLine($"Elapsed time for Tokenization: {stopwatchTokenizer.Elapsed}");
            Console.WriteLine($"Elapsed time for Tree Insert: {stopwatchInsert.Elapsed}");
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);
    }
}