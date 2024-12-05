# War and Peace Text Processor (FPROG-Project)

This project processes text files, specifically "War and Peace", to tokenize words, organize them in a Red-Black Tree for sorting and efficiency, and output sorted results to a file. It employs data structures and parallel processing for scalability and performance.

## Features

- **Tokenizer**: Tokenizes text into words, ignoring punctuation and case.
- **Red-Black Tree Implementation**: A self-balancing binary search tree used for efficient word sorting.
- **Batch Processing and Tree Merging**: Handles large datasets by processing in batches and merging trees.
- **Parallel Processing**: Uses concurrency to optimize performance for large files.
- **Testing Suite**: Includes unit tests to ensure functionality and reliability.

## Project Structure

- **`Program.cs`**: Main entry point; processes the text file, tokenizes, sorts, and outputs the data.
- **`Tokenizer.cs`**: Contains logic to extract and normalize words from a text.
- **`RedBlackTree.cs`**: Implementation of the Red-Black Tree with insertion, traversal, and balancing rules.
- **`RedBlackTreeNode.cs`**: Defines the node structure for the Red-Black Tree.
- **`TreeUtils.cs`**: Utility methods for batch insertion and tree merging.
- **`ConsoleUtils.cs`**: Helper methods for console logging and progress display.
- **`UnitTests.cs`**: Unit tests covering tokenization, tree operations, and overall functionality.
- **`GlobalUsings.cs`**: Defines global using directives for the project.

## How It Works

1. **Input**: The program reads the text from a file (default: `war_and_peace.txt`).
2. **Tokenization**: Extracts words and converts them to lowercase.
3. **Batch Processing**: Divides words into manageable chunks for processing.
4. **Tree Insertion**: Adds words into Red-Black Trees for sorting and deduplication.
5. **Tree Merging**: Combines multiple trees into one while preserving order and uniqueness.
6. **Output**: Writes sorted words to `output.txt`.

## Technologies Used

- **C# and .NET**
- **NUnit for testing**
- **Parallel LINQ (PLINQ) for concurrency**

