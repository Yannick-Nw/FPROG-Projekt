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
                var newRoot = RedBlackTreeNode.CreateNode(value, Color.BLACK);
                return new RedBlackTree(newRoot);
            }

            var newRoot = InsertHelper(Root, value);
            return new RedBlackTree(newRoot);
        }

        public RedBlackTreeNode InsertHelper(RedBlackTreeNode root, string value)
        {
            bool redConflict = false; //for red following red node conflict

            if(root == null)
            {

            } 
            else if ()
            {

            } 
            else
            {

            }
        }

        //Flags for left, right rotation
        private bool _llFlag;
        private bool _rrFlag;
        private bool _lrFlag;
        private bool _rlFlag;

        private RedBlackTreeNode RotateLeft(RedBlackTreeNode root)
        {

        }

        private RedBlackTree RotateRight(RedBlackTreeNode root)
        {

        }
    }
}
