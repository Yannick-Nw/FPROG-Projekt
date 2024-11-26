namespace WarAndPeace
{
    public static class TreeUtils
    {
        public static RedBlackTree InsertBatch(IEnumerable<string> batch) =>
            batch.Aggregate(new RedBlackTree(), (tree, word) => tree.Insert(word));

        public static RedBlackTree MergeTrees(List<RedBlackTree> trees)
        {
            if (trees == null || trees.Count == 0)
                return new RedBlackTree();

            return MergeAll(trees);
        }

        private static RedBlackTree MergeAll(List<RedBlackTree> trees)
        {
            // Base case: if only one tree, return it
            if (trees.Count == 1)
                return trees[0];

            // Reduce the list by merging pairs in parallel
            var mergedTrees = trees
                .AsParallel()
                .AsOrdered()
                .Select((tree, index) => new { tree, index })
                .GroupBy(x => x.index / 2)
                .Select(group => group.Select(x => x.tree).ToList())
                .Select(pair => pair.Count == 2
                    ? MergeTwoTrees(pair[0], pair[1]) // Merge pair of trees
                    : pair[0]) // Handle odd leftover tree
                .ToList();

            // Recursively merge the reduced list
            return MergeAll(mergedTrees);
        }


        private static RedBlackTree MergeTwoTrees(RedBlackTree tree1, RedBlackTree tree2) =>
            tree1.InOrderTraversal()
                .Concat(tree2.InOrderTraversal())
                .Distinct()
                .OrderBy(word => word)
                .Aggregate(new RedBlackTree(), (mergedTree, word) => mergedTree.Insert(word));
    }
}