using System;
using System.Runtime.CompilerServices;

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
        //No setter: Once created, node is immutable
        public string Value { get; }
        public Color Color { get; }
        public RedBlackTreeNode Left { get; }
        public RedBlackTreeNode Right { get; }

        public RedBlackTreeNode(string value, Color color,
                                 RedBlackTreeNode left = null,
                                 RedBlackTreeNode right = null)
        {
            Value = value;
            Color = color;
            Left = left;
            Right = right;
        }

        public static RedBlackTreeNode CreateNode(string value, Color color) =>
            new RedBlackTreeNode(value, color);

        //Node but add left child
        public RedBlackTreeNode AddLeftChild(RedBlackTreeNode leftNode) =>
            new RedBlackTreeNode(Value, Color, leftNode, Right);

        //Node but add right child
        public RedBlackTreeNode AddRightChild(RedBlackTreeNode rightNode) =>
            new RedBlackTreeNode(Value, Color, Left, rightNode);

        //Node but change color (for rotation)
        public RedBlackTreeNode ChangeColor(Color newColor) =>
            new RedBlackTreeNode(Value, newColor, Left, Right);
    }


    // Node InsertNode(value, oldTreeRootAufGleicherHöhe)
    // if oldTreeRAGH is null
    //          new Node(null, null, value)

    //      if value left of oldTreeRAGH
    //          CheckForAnySpinnzShit when u insert to the left, if ther e is some shit create the new things accordingly
    //          return new Node(InsertNode(value, oldTreeRAGH.LEFTCHILD), oldTreeRAGH.RIGHTCHILD), oldTreeRAGH.value)
    //      if value right of oldTreeRAGH
    //          CheckForAnySpinnzShit when u insert to the right, if there is some shit create the new things accordingly
    //          return new Node(oldTreeRAGH.LEFTChild, InsertNode(value, oldTreeRAGH.RIGHTchild), oldTreeRAGH.value)

}
