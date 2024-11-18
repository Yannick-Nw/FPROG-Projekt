using System;
using System.Collections.Generic;
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

        public RedBlackTree Insert(string value)
        {
            if(Root == null)
            {
                var firstRoot = RedBlackTreeNode.CreateNode(value, Color.BLACK);
                return new RedBlackTree(firstRoot);
            }

            var newRoot = InsertHelper(Root, value);
            return new RedBlackTree(newRoot);
        }

        public RedBlackTreeNode InsertHelper(RedBlackTreeNode root, string value)
        {
            bool redConflict = false; //for red following red node conflict

            if(root == null) //recursion breaking condition
            {
                return RedBlackTreeNode.CreateNode(value, Color.RED); //TODO: in sample created without color (difference?)
            } 
            else if (value.CompareTo(root.Value) < 0) //string smaller than root, go left
            {
                var leftNode = InsertHelper(root.Left, value);
                leftNode = leftNode.AddParent(root);
                root = root.AddLeftChild(leftNode); //immutable, create new node with left child
                if(root != this.Root)
                {
                    if(root.Color == Color.RED && root.Left.Color == Color.RED)
                    {
                        redConflict = true; //Two adjacent red nodes: violation of rule!
                    }
                }
            } 
            else //string bigger than root, go right
            {
                var rightNode = InsertHelper(root.Right, value);
                rightNode = rightNode.AddParent(root);
                root = root.AddRightChild(rightNode);
                if (root != this.Root)
                {
                    if(root.Color == Color.RED && root.Right.Color == Color.RED)
                    {
                        redConflict = true;
                    }
                }
            } 
            //Check if rotations needed (flags set)

        }
        /*
         // Perform rotations
        if (ll) {
            root = rotateLeft(root);
            root->colour = 'B';
            root->left->colour = 'R';
            ll = false;
        } else if (rr) {
            root = rotateRight(root);
            root->colour = 'B';
            root->right->colour = 'R';
            rr = false;
        } else if (rl) {
            root->right = rotateRight(root->right);
            root->right->parent = root;
            root = rotateLeft(root);
            root->colour = 'B';
            root->left->colour = 'R';
            rl = false;
        } else if (lr) {
            root->left = rotateLeft(root->left);
            root->left->parent = root;
            root = rotateRight(root);
            root->colour = 'B';
            root->right->colour = 'R';
            lr = false;
        }
         */

        //Flags for left, right rotation
        private bool _llFlag;
        private bool _rrFlag;
        private bool _lrFlag;
        private bool _rlFlag;

        private RedBlackTreeNode RotateLeft(RedBlackTreeNode root)
        {

        }

        private RedBlackTreeNode RotateRight(RedBlackTreeNode root)
        {

        }

        private RedBlackTree CreateNewTreeWithFlags(RedBlackTreeNode root)
        {
            var newTree = new RedBlackTree(root);
            newTree._llFlag = _llFlag; //is set only once, not mutable ! (?)
            newTree._rrFlag = _rrFlag;
            newTree._lrFlag = _lrFlag;
            newTree._rlFlag = _rlFlag;

            return newTree;
        }
    }
}
