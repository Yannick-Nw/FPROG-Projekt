using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework; // Replace with your chosen testing library

namespace WarAndPeace.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void Tokenize_ReturnsCorrectTokens()
        {
            string text = "War, peace, and harmony!";
            var expectedTokens = new List<string> { "war", "peace", "and", "harmony" };
            var actualTokens = Tokenizer.Tokenize(text).ToList();
            Assert.That(actualTokens, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void Tokenize_IgnoresPunctuation()
        {
            string text = "Hello, world! It's a wonderful day.";
            var expectedTokens = new List<string> { "hello", "world", "it's", "a", "wonderful", "day" };
            var actualTokens = Tokenizer.Tokenize(text).ToList();
            Assert.That(actualTokens, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void Tokenize_EmptyInput_ReturnsEmptyList()
        {
            string text = "";
            var actualTokens = Tokenizer.Tokenize(text).ToList();
            Assert.That(actualTokens.Count, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class RedBlackTreeTests
    {
        [Test]
        public void Insert_AddsWordsToTree()
        {
            var tree = new RedBlackTree();
            tree = tree.Insert("war").Insert("peace").Insert("harmony");
            var words = tree.InOrderTraversal().ToList();
            var expected = new List<string> { "harmony", "peace", "war" };
            Assert.That(words, Is.EqualTo(expected));
        }

        [Test]
        public void Insert_IgnoresDuplicates()
        {
            var tree = new RedBlackTree();
            tree = tree.Insert("war").Insert("peace").Insert("war");
            var words = tree.InOrderTraversal().ToList();
            var expected = new List<string> { "peace", "war" };
            Assert.That(expected, Is.EqualTo(words));
        }

        [Test]
        public void CountBlack_ValidatesBlackNodeCount()
        {
            var tree = new RedBlackTree();
            tree = tree.Insert("war").Insert("peace").Insert("harmony");
            int blackCount = tree.CountBlack();
            Assert.That(blackCount, Is.GreaterThan(0)); // Ensure valid black node count
        }
    }

    [TestFixture]
    public class TreeUtilsTests
    {
        [Test]
        public void InsertBatch_AddsBatchToTree()
        {
            var words = new List<string> { "war", "peace", "harmony" };
            var tree = TreeUtils.InsertBatch(words);
            var sortedWords = tree.InOrderTraversal().ToList();
            var expected = new List<string> { "harmony", "peace", "war" };
            Assert.That(sortedWords, Is.EqualTo(expected));
        }

        [Test]
        public void MergeTrees_MergesMultipleTreesCorrectly()
        {
            var tree1 = TreeUtils.InsertBatch(new List<string> { "war", "peace" });
            var tree2 = TreeUtils.InsertBatch(new List<string> { "harmony", "war" });
            var mergedTree = TreeUtils.MergeTrees(new List<RedBlackTree> { tree1, tree2 });
            var sortedWords = mergedTree.InOrderTraversal().ToList();
            var expected = new List<string> { "harmony", "peace", "war" };
            Assert.That(sortedWords, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void ReadWholeFile_ReadsCorrectly()
        {
            string testFilePath = "test.txt";
            File.WriteAllText(testFilePath, "War and Peace");
            string content = Program.ReadWholeFile(testFilePath);
            Assert.That("War and Peace", Is.EqualTo(content));
            File.Delete(testFilePath);
        }

        [Test]
        public void ProcessFile_GeneratesCorrectOutput()
        {
            string fileName = "war_and_peace_test.txt";
            string inputText = "War, peace, harmony, and tranquility!";
            File.WriteAllText(fileName, inputText);

            Program.ProcessFile(fileName);

            var sortedWords = File.ReadAllLines("../../../output.txt").ToList();
            var expected = new List<string> { "and", "harmony", "peace", "tranquility", "war" };
            Assert.That(sortedWords, Is.EqualTo(expected));

            File.Delete("war_and_peace_test.txt");
            File.Delete("../../../output.txt");
        }
    }
}
