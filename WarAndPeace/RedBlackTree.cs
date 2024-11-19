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

        private RedBlackTree(RedBlackTreeNode root)
        {
            Root = root;
        }

        private RedBlackTree(Color color, RedBlackTree leftSubtree, string value, RedBlackTree rightSubtree)
        {
            Root = new RedBlackTreeNode(value, color, leftSubtree.Root, rightSubtree.Root);
            Debug.Assert(leftSubtree.IsEmpty() || leftSubtree.Root.Value.CompareTo(value) < 0);
            Debug.Assert(rightSubtree.IsEmpty() || rightSubtree.Root.Value.CompareTo(value) > 0);
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
            if (IsEmpty())
            {
                //Create new subtree/makes new node always red
                return new RedBlackTree(Color.RED, new RedBlackTree(), value, new RedBlackTree());
            }

            string rootValue = Root.Value;
            Color rootColor = Root.Color;
            if(rootColor == Color.BLACK) //red under black node could destabilize balance --> rebalance
            {

            }
            else //not immediately balanced --> recoloring
            {

            }
        }

        //Check if tree empty (when root doesnt exist)
        private bool IsEmpty()
        {
            return Root == null;
        }

        // First rule: No red node should have a red child
        private void AssertRedRule()
        {
            if (!IsEmpty()) 
            {
                var left = Left();
                var right = Right();
                if (Root.Color == Color.RED)
                {
                    Debug.Assert(left.IsEmpty() || left.Root.Color == Color.BLACK);
                    Debug.Assert(right.IsEmpty() || right.Root.Color == Color.BLACK);
                }
                left.AssertRedRule(); //Assert for all subtrees that no two adjacent red nodes
                right.AssertRedRule();
            }
        }

        //Second rule: Amount of black nodes from each (empty) leaf to root should be the same
        private int CountBlack()
        {
            if (IsEmpty())
            {
                return 0;
            }
            int leftCount = Left().CountBlack();
            int rightCount = Right().CountBlack();
            Debug.Assert(leftCount == rightCount);
            return (Root.Color == Color.BLACK) ? 1 + leftCount : leftCount;
        }

    }
}
