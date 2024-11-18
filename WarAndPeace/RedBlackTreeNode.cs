using System;

namespace WarAndPeace
{
    public enum Color { RED, BLACK };
    /*
     PLAN:
     * kleinere strings links (also die weiter oben im alphabet, A..)
     * große strings rechts
     * bei insert immer neuen tree returnen (funktional)
     * nach insert mit fix insert so rotaten usw. dass es wieder red black entspricht
     * Rote node: Keine roten kindsknoten erlaubt, vorübergehend ungleichgewicht
     * Schwarze node: Jeder pfad von root zu leaf hat gleiche anzahl schwarzer knoten (höhe)
     * --> sind also die stabilen knoten, wurzel ist IMMER schwarz
     */
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

        public static RedBlackTreeNode CreateNode(string value, Color color) =>
            new RedBlackTreeNode(value, color);

        //Node but add left child
        public RedBlackTreeNode AddLeftChild(RedBlackTreeNode leftNode) =>
            new RedBlackTreeNode(Value, Color, leftNode, Right, Parent);

        //Node but add right child
        public RedBlackTreeNode AddRightChild(RedBlackTreeNode rightNode) =>
            new RedBlackTreeNode(Value, Color, Left, rightNode, Parent);

        //Node but change color (for rotation)
        public RedBlackTreeNode ChangeColor(Color newColor) =>
            new RedBlackTreeNode(Value, newColor, Left, Right, Parent);
    }
}
