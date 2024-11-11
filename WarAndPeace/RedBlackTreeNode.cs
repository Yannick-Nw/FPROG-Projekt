using System;

namespace WarAndPeace
{
    public enum Color { RED, BLACK };

    public class RedBlackTreeNode
    {
        public string Value { get; }
        public Color Color { get; }
        public RedBlackTreeNode Left { get; }
        public RedBlackTreeNode Right { get; }
        public RedBlackTreeNode Parent { get; }

        private RedBlackTreeNode(string value, Color color,
                                 RedBlackTreeNode left = null,
                                 RedBlackTreeNode right = null,
                                 RedBlackTreeNode parent = null)
        {
            Value = value;
            Color = color;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public static RedBlackTreeNode CreateRootNode(string value, Color color) =>
            new RedBlackTreeNode(value, color);

        //Add new node with left child
        public RedBlackTreeNode AddLeftChild(string value, Color color) =>
            new RedBlackTreeNode(value, color, parent: this);

        //Add new node with right child
        public RedBlackTreeNode AddRightChild(string value, Color color) =>
            new RedBlackTreeNode(value, color, parent: this);

        //Change existing left node
        public RedBlackTreeNode WithLeft(RedBlackTreeNode newLeft) =>
            new RedBlackTreeNode(Value, Color, newLeft, Right, Parent);

        //Change existing right node
        public RedBlackTreeNode WithRight(RedBlackTreeNode newRight) =>
            new RedBlackTreeNode(Value, Color, Left, newRight, Parent);
    }
}
