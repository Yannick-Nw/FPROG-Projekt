using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarAndPeace
{
    public class RedBlackTree
    {
        public RedBlackTreeNode Root { get; }

        public RedBlackTree()
        {
            Root = null;
        }

        public void PrintTree()
        {
            if (Root != null)
            {
                PrintTreeHelper(Root);
            }
            else
            {
                Console.WriteLine("The tree is empty!!");
            }
        }

        // Helper method to print tree structure
        private void PrintTreeHelper(RedBlackTreeNode node)
        {
            if (node == null) return;

            Console.WriteLine($"{node.Value} ({node.Color})");

            if (node.Left != null)
            {
                Console.WriteLine($"{node.Value} -> {node.Left.Value} (left child)");
                PrintTreeHelper(node.Left);
            }
            if (node.Right != null)
            {
                Console.WriteLine($"{node.Value} -> {node.Right.Value} (right child)");
                PrintTreeHelper(node.Right);
            }
        }

        
        public List<string> InOrderTraversal()
        {
            var result = new List<string>();
            InOrderTraversalHelper(Root, result);
            return result;
        }

        private void InOrderTraversalHelper(RedBlackTreeNode node, List<string> result)
        {
            if (node == null) return;

            // Traverse left subtree
            InOrderTraversalHelper(node.Left, result);

            // Visit root
            result.Add(node.Value);

            // Traverse right subtree
            InOrderTraversalHelper(node.Right, result);
        }
        


    private RedBlackTree(RedBlackTreeNode root)
        {
            Root = root;
        }

        private RedBlackTree(Color color, RedBlackTree leftSubtree, string value, RedBlackTree rightSubtree)
        {
            Root = new RedBlackTreeNode(value, color, leftSubtree.Root, rightSubtree.Root);
            Debug.Assert(leftSubtree.RootIsEmpty() || leftSubtree.Root.Value.CompareTo(value) < 0);
            Debug.Assert(rightSubtree.RootIsEmpty() || rightSubtree.Root.Value.CompareTo(value) > 0);
        }
          
        //Public insert method
        public RedBlackTree Insert(string value)
        {
            RedBlackTree newTree = InsertHelper(value);
            return new RedBlackTree(Color.BLACK, newTree.Left(), newTree.Root.Value, newTree.Right());
        }

        //Get left subtree
        public RedBlackTree Left()
        {
            return new RedBlackTree(Root.Left);
        }

        //Get right subtree
        public RedBlackTree Right()
        {
            return new RedBlackTree(Root.Right);
        }

        //Insert nodes and detect violations of red black rule
        private RedBlackTree InsertHelper(string value)
        {
            AssertRedRule();
            if (RootIsEmpty())
            {
                //Create new subtree/makes new node always red
                return new RedBlackTree(Color.RED, new RedBlackTree(), value, new RedBlackTree());
            }

            string rootValue = Root.Value;
            Color rootColor = Root.Color;
            if(rootColor == Color.BLACK) //red under black node could destabilize balance --> rebalance (e.g black grandparent, could have two red nodes following. rebalance those)
            {
                if(FirstStringBigger(rootValue, value)) //Value to insert is smaller than current root -> go left
                {
                    return Balance(Left().InsertHelper(value), rootValue, Right()); //recursion to where to insert node, then go up  and try to balance
                }
                else if(FirstStringSmaller(rootValue, value)) //Value to insert bigger than current root -> go right
                {
                    return Balance(Left(), rootValue, Right().InsertHelper(value)); //recursion to where to insert node, then go up and try to balance
                }
                else
                {
                    return this; //no duplicates allowed
                }
            }
            else //e.g grandparent red, insert for now (child of grandparent cant be red as well, so violation shouldnt be possible here (see assertion)  )
            {
                if (FirstStringBigger(rootValue, value)) //Value to insert is smaller than current root -> go left
                {
                    return new RedBlackTree(rootColor, Left().InsertHelper(value), rootValue, Right()); //Baum wird von unten aus neu aufgebaut mit neuer node so ziemlich
                }
                else if (FirstStringSmaller(rootValue, value)) //Value to insert bigger than current root -> go right
                {
                    return new RedBlackTree(rootColor, Left(), rootValue, Right().InsertHelper(value)); //Baum wird von unten aus neu aufgebaut mit neuer node so ziemlich
                }
                else
                {
                    return this; //no duplicates allowed
                }
            }
        }

        //If parent is black, balance to avoid violation, no rotation needed because of reconstruction of tree with Balance
        public static RedBlackTree Balance(RedBlackTree leftSubtree, string value, RedBlackTree rightSubtree)
        {
            if (leftSubtree.DoubleRedLeft()) //Left red parent, left red child
            {
                //Equivalent to a right rotation, restructure subtree
                return new RedBlackTree(Color.RED, leftSubtree.Left().Paint(Color.BLACK), leftSubtree.Root.Value, 
                    new RedBlackTree(Color.BLACK, leftSubtree.Right(), value, rightSubtree));
                //Left node becomes new root, right subtree has old root as parent, basically like right rotation! and repaint new left and right subtrees black
            }
            else if (leftSubtree.DoubleRedRight()) //Left red parent, right red child
            {
                //Equivalent to left, then right rotation
                return new RedBlackTree(Color.RED, new RedBlackTree(Color.BLACK, leftSubtree.Left(),
                    leftSubtree.Root.Value, leftSubtree.Right().Left()), leftSubtree.Right().Root.Value,
                    new RedBlackTree(Color.BLACK, leftSubtree.Right().Right(), value, rightSubtree));
                //Right node of left subtree becomes new root, left subtree goes to left of this root, old parent to the right of this root 
            }
            else if (rightSubtree.DoubleRedLeft()) //Right red parent, left red child
            {
                //Equivalent to a right, then left rotation
                return new RedBlackTree(Color.RED, new RedBlackTree(Color.BLACK, leftSubtree, value, rightSubtree.Left().Left()),
                    rightSubtree.Left().Root.Value, 
                    new RedBlackTree(Color.BLACK, rightSubtree.Left().Right(), rightSubtree.Root.Value, rightSubtree.Right()));
                //Right subtree.left becomes new root, old parent becomes left subtree, right Subtree stays right of new root
            }
            else if (rightSubtree.DoubleRedRight()) //Right red parent, right red child
            {
                //Equivalent to a left rotation, subtree is restructured
                return new RedBlackTree(Color.RED, new RedBlackTree(Color.BLACK, leftSubtree, value, rightSubtree.Left()), 
                    rightSubtree.Root.Value, rightSubtree.Right().Paint(Color.BLACK));
                //Right subtree new root, old root is left, old right.right is now right, and repaint new left and right subtrees black
            }
            else //no violation, return same tree basically
            {
                return new RedBlackTree(Color.BLACK, leftSubtree, value, rightSubtree);
            }
        }

        //Detect violation: Red node with red child node on left
        private bool DoubleRedLeft()
        {
            return !RootIsEmpty()
                && Root.Color == Color.RED
                && !Left().RootIsEmpty()
                && Left().Root.Color == Color.RED;
        }

        //Detect violation: Red node with red child node on right
        private bool DoubleRedRight()
        {
            return !RootIsEmpty()
                && Root.Color == Color.RED
                && !Right().RootIsEmpty()
                && Right().Root.Color == Color.RED;
        }

        //Return (sub)tree where root's color is changed to new color
        private RedBlackTree Paint(Color newColor)
        {
            Debug.Assert(!RootIsEmpty());
            return new RedBlackTree(newColor, Left(), Root.Value, Right());
        }

        private static bool FirstStringSmaller(string first, string second)
        {
            return first.CompareTo(second) < 0;
        }

        private static bool FirstStringBigger(string first, string second)
        {
            return first.CompareTo(second) > 0;
        }

        //Check if tree empty (when root doesnt exist)
        private bool RootIsEmpty()
        {
            return Root == null;
        }

        // First rule: No red node should have a red child
        private void AssertRedRule()
        {
            if (!RootIsEmpty()) 
            {
                var left = Left();
                var right = Right();
                if (Root.Color == Color.RED)
                {
                    Debug.Assert(left.RootIsEmpty() || left.Root.Color == Color.BLACK);
                    Debug.Assert(right.RootIsEmpty() || right.Root.Color == Color.BLACK);
                }
                left.AssertRedRule(); //Assert for all subtrees that no two adjacent red nodes
                right.AssertRedRule();
            }
        }

        //Second rule: Amount of black nodes from each (empty) leaf to root should be the same
        public int CountBlack()
        {
            if (RootIsEmpty())
            {
                return 0; //sollte das nicht 1 sein?
            }
            int leftCount = Left().CountBlack();
            int rightCount = Right().CountBlack();
            Debug.Assert(leftCount == rightCount);
            return (Root.Color == Color.BLACK) ? 1 + leftCount : leftCount;
        }

    }
}
