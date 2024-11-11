using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarAndPeace
{
    public enum Color { RED, BLACK };

    public class RedBlackTreeNode<TValue>
    {
        public TValue Value { get; set; }
        Color Color { get; set; }
        RedBlackTreeNode<TValue> Left;
        RedBlackTreeNode<TValue> Right;
        RedBlackTreeNode<TValue> Parent;

        public static RedBlackTreeNode<TValue> CreateNode(TValue value, Color color)
        {
            return new RedBlackTreeNode<TValue>(value, color);
        }

        internal RedBlackTreeNode(TValue value, Color color)
        {
            Value = value;
            Color = color;
        }

    }
}
