using System;

namespace Appalachia.Utility.DataStructures.Trees
{
    public class TernarySearchTree
    {
        #region Fields and Autoproperties

        public TernaryTreeNode Root { get; private set; }

        #endregion

        public void Insert(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new Exception("Inputted value is empty");
            }

            if (Root == null)
            {
                Root = new TernaryTreeNode(null, word[0], word.Length == 1);
            }

            WordInsertion(word);
        }

        public void Insert(string[] words)
        {
            foreach (var word in words)
            {
                Insert(word);
            }
        }

        private TernaryTreeNode ChooseNode(TernaryTreeNode currentNode, string word, ref int index)
        {
            //Center Branch
            if (word[index] == currentNode.Value)
            {
                index++;

                if (currentNode.GetMiddleChild == null)
                {
                    InsertInTree(
                        currentNode.AddMiddleChild(word[index], word.Length == (index + 1)),
                        word,
                        ref index
                    );
                }

                return currentNode.GetMiddleChild;
            }

            //Right Branch

            if (word[index] > currentNode.Value)
            {
                if (currentNode.GetRightChild == null)
                {
                    InsertInTree(
                        currentNode.AddRightChild(word[index], word.Length == (index + 1)),
                        word,
                        ref index
                    );
                }

                return currentNode.GetRightChild;
            }

            //Left Branch

            if (currentNode.GetLeftChild == null)
            {
                InsertInTree(
                    currentNode.AddLeftChild(word[index], word.Length == (index + 1)),
                    word,
                    ref index
                );
            }

            return currentNode.GetLeftChild;
        }

        private void InsertInTree(TernaryTreeNode currentNode, string word, ref int currentIndex)
        {
            var length = word.Length;

            currentIndex++;
            var currNode = currentNode;
            for (var i = currentIndex; i < length; i++)
            {
                currNode = currNode.AddMiddleChild(word[i], word.Length == (currentIndex + 1));
            }

            currentIndex = length;
        }

        private void WordInsertion(string word)
        {
            var index = 0;
            var currentNode = Root;

            while (index < word.Length)
            {
                currentNode = ChooseNode(currentNode, word, ref index);
            }
        }
    }
}
