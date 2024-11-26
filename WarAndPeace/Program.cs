﻿using System.Collections.Concurrent;
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

            //int totalWords = words.Count();
            //int count = 0;

            Console.WriteLine("Inserting words...");
            var stopwatchInsert = Stopwatch.StartNew();
            // Divide words into smaller chunks
            var wordBatches = words.Chunk(1000); // Adjust batch size as needed

            // Process each batch in parallel
            var trees = wordBatches.AsParallel()
                .Select(batch => InsertBatch(batch))
                .ToList();

            // Merge trees into a single tree
            Console.WriteLine("Merging trees");
            RedBlackTree finalTree = MergeTrees(trees);

            stopwatchInsert.Stop();

            Console.WriteLine(); // Ensure a newline after the progress bar
            Console.WriteLine($"Black node count: {finalTree.CountBlack()}");
            finalTree.PrintTree();

            /*
            // Traverse the tree to get sorted words
            var sortedWords = RedBlackTree.InOrderTraversal();

            // Write sorted words to a file
            File.WriteAllLines("../../../output.txt", sortedWords);

            Console.WriteLine("Sorted words written to output.txt");
            */
            Console.WriteLine($"Elapsed time for Tokenization: {stopwatchTokenizer.Elapsed}");
            Console.WriteLine($"Elapsed time for Tree Insert: {stopwatchInsert.Elapsed}");
        }

        public static string ReadWholeFile(string path) => File.ReadAllText(path);


        // Local function to insert a batch of words into a new tree
        public static RedBlackTree InsertBatch(IEnumerable<string> batch)
        {
            var tree = new RedBlackTree();
            Console.WriteLine("Starting insert batch...");
            foreach (var word in batch)
            {
                tree = tree.Insert(word);
            }

            Console.WriteLine("Tree finished");
            return tree;
        }

        // Local function to merge multiple trees
        private static RedBlackTree MergeTrees(List<RedBlackTree> trees)
        {
            if (trees == null || trees.Count == 0)
                return new RedBlackTree();

            int totalMerges = trees.Count - 1; // Total merges required to reach a single tree
            int completedMerges = 0;

            while (trees.Count > 1)
            {
                var mergedTrees = new ConcurrentBag<RedBlackTree>();

                Parallel.For(0, trees.Count / 2, i =>
                {
                    var mergedTree = MergeTwoTrees(trees[2 * i], trees[2 * i + 1]);
                    mergedTrees.Add(mergedTree);

                    // Update the progress bar atomically
                    Interlocked.Increment(ref completedMerges);
                    DrawProgressBar(completedMerges, totalMerges);
                });

                // Handle any leftover tree if the number of trees is odd
                if (trees.Count % 2 == 1)
                {
                    mergedTrees.Add(trees[^1]);
                }

                trees = mergedTrees.ToList();
            }

            return trees[0];
        }

        // Function to merge two trees
        private static RedBlackTree MergeTwoTrees(RedBlackTree tree1, RedBlackTree tree2)
        {
            var allWords = tree1.InOrderTraversal()
                .Concat(tree2.InOrderTraversal())
                .Distinct()
                .OrderBy(word => word);

            var mergedTree = new RedBlackTree();
            foreach (var word in allWords)
            {
                mergedTree = mergedTree.Insert(word);
            }

            return mergedTree;
        }


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