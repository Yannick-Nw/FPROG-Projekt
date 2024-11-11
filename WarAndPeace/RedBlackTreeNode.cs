using System;

namespace WarAndPeace
{
    public enum Color { RED, BLACK };

    public class RedBlackTreeNode<TValue>
    {
        public TValue Value { get; }
        public Color Color { get; }
        public RedBlackTreeNode<TValue> Left { get; }
        public RedBlackTreeNode<TValue> Right { get; }
        public RedBlackTreeNode<TValue> Parent { get; }

        private RedBlackTreeNode(TValue value, Color color,
                                 RedBlackTreeNode<TValue> left = null,
                                 RedBlackTreeNode<TValue> right = null,
                                 RedBlackTreeNode<TValue> parent = null)
        {
            Value = value;
            Color = color;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public static RedBlackTreeNode<TValue> CreateRootNode(TValue value, Color color) =>
            new RedBlackTreeNode<TValue>(value, color);

        //Add new node with left child
        public RedBlackTreeNode<TValue> AddLeftChild(TValue value, Color color) =>
            new RedBlackTreeNode<TValue>(value, color, parent: this);

        //Add new node with right child
        public RedBlackTreeNode<TValue> AddRightChild(TValue value, Color color) =>
            new RedBlackTreeNode<TValue>(value, color, parent: this);

        //Change existing left node
        public RedBlackTreeNode<TValue> WithLeft(RedBlackTreeNode<TValue> newLeft) =>
            new RedBlackTreeNode<TValue>(Value, Color, newLeft, Right, Parent);

        //Change existing right node
        public RedBlackTreeNode<TValue> WithRight(RedBlackTreeNode<TValue> newRight) =>
            new RedBlackTreeNode<TValue>(Value, Color, Left, newRight, Parent);
    }
}
