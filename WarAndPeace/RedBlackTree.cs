using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarAndPeace
{
    //flags for deciding how to rotate, outside of tree class --> ensures immutability
    public record RotationFlags(bool LlFlag, bool RrFlag, bool LrFlag, bool RlFlag);
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

        //Public insert method
        public RedBlackTree Insert(string value)
        {
            if(Root == null)
            {
                var firstRoot = RedBlackTreeNode.CreateNode(value, Color.BLACK);
                return new RedBlackTree(firstRoot);
            }

            var (newRoot, _) = InsertHelper(Root, value, new RotationFlags(false, false, false, false)); //record of flags, to make tree immutable
            return new RedBlackTree(newRoot);
        }

        //Insert nodes and detect violations of red black rule
        private (RedBlackTreeNode, RotationFlags) InsertHelper(RedBlackTreeNode root, string value, RotationFlags flags)
        {
            bool redConflict = false;

            if (root == null)
            {
                return (RedBlackTreeNode.CreateNode(value, Color.RED), flags);
            }
            else if (value.CompareTo(root.Value) < 0)
            {
                var (leftNode, newFlags) = InsertHelper(root.Left, value, flags);
                leftNode = leftNode.AddParent(root);
                root = root.AddLeftChild(leftNode);

                if (root != this.Root && root.Color == Color.RED && root.Left.Color == Color.RED)
                {
                    redConflict = true;
                }
                flags = newFlags;
            }
            else
            {
                var (rightNode, newFlags) = InsertHelper(root.Right, value, flags);
                rightNode = rightNode.AddParent(root);
                root = root.AddRightChild(rightNode);

                if (root != this.Root && root.Color == Color.RED && root.Right.Color == Color.RED)
                {
                    redConflict = true;
                }
                flags = newFlags;
            }
            //SIDE NOTE: red Conflict = true fired for parent node, ll, rr etc. is handled on grandfather node (one above parent)
            //child node in this case would be the one that follows red node and is also red --> the one that causes the violation basically

            //Check if rotations needed (flags set) - based on how the nodes are setup (ll = left and left node of that both red)
            if (flags.LlFlag)
            {   // red left and red left child node
                root = RotateRight(root); //Right rotation of grandfather node (root is grandfather in this case)
                root = root.ChangeColor(Color.BLACK); //Swap colors of grandfather and parent (makes root of subtree black, basically)
                var rightRedNode = root.Right.ChangeColor(Color.RED); //grandfather is now right child of parent node, make it red
                root = root.AddRightChild(rightRedNode);
                _llFlag = false;
            }
            else if (flags.RrFlag)
            {   // red right and red right child node
                root = RotateLeft(root); //Left rotation of grandfather node (root is grandfather in this case)
                root = root.ChangeColor(Color.BLACK); //Swap colors of grandfather and parent
                var leftRedNode = root.Left.ChangeColor(Color.RED); //Grandfather now left child of parent node, make it red
                root = root.AddLeftChild(leftRedNode);
                _rrFlag = false;
            }
            else if (flags.RlFlag)
            {  //Red right and red left child node: rotate twice, first right then left
                var rightNode = RotateRight(root.Right); //root.Right is parent node in this case (right child of grandfather)
                rightNode = rightNode.AddParent(root); 
                root = root.AddRightChild(rightNode); //Connect new right Node to grandparent
                root = RotateLeft(root); //Now rotate grandfather to the left
                root = root.ChangeColor(Color.BLACK); //Make new root of this subtree black
                var leftRedNode = root.Left.ChangeColor(Color.RED); //Make old grandfather/root of subtree red
                root = root.AddLeftChild(leftRedNode); //change black node to red node, immutable so new node is created
                _rlFlag = false;
            }
            else if (flags.LrFlag)
            {  //Red left and red right child node: rotate twice, first left then right
                var leftNode = RotateLeft(root.Left); //root.Left is parent node in this case, left child of grandfather
                leftNode = leftNode.AddParent(root);
                root = root.AddLeftChild(leftNode); //connect new left node to grandfather/root of subtree
                root = RotateRight(root); //Rotate grandfather to the right
                root = root.ChangeColor(Color.BLACK); //Make new root of subtree black
                var rightRedNode = root.Right.ChangeColor(Color.RED); //Make old grandfather/now right child red
                root = root.AddRightChild(rightRedNode); //change black node to red node, immutable so new node is created
                _lrFlag = false;
            }

            if (redConflict)
            {
                // rr und ll vertauscht! gehören zum jeweils anderen if statement

                redConflict = false;
            }

            return (root, flags);
        }

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
