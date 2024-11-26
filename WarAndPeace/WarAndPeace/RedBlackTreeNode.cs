using System;
using System.Runtime.CompilerServices;

namespace WarAndPeace
{
    public enum Color
    {
        RED,
        BLACK
    };

    public record RedBlackTreeNode(
        string Value,
        Color Color,
        RedBlackTreeNode? Left = null,
        RedBlackTreeNode? Right = null)
    {
        public RedBlackTreeNode AddLeftChild(RedBlackTreeNode leftNode) =>
            this with { Left = leftNode };

        public RedBlackTreeNode AddRightChild(RedBlackTreeNode rightNode) =>
            this with { Right = rightNode };

        public RedBlackTreeNode ChangeColor(Color newColor) =>
            this with { Color = newColor };
    }
}