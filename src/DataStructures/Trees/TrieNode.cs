using System.Collections.Generic;

namespace Appalachia.Utility.DataStructures.Trees
{
    /// <summary>
    ///     The Trie Node.
    /// </summary>
    public class TrieNode
    {
        /// <summary>
        ///     CONSTRUCTORS
        /// </summary>
        public TrieNode(char key) : this(key, false)
        {
        }

        public TrieNode(char key, bool isTerminal)
        {
            Key = key;
            IsTerminal = isTerminal;
            Children = new Dictionary<char, TrieNode>();
        }

        #region Fields and Autoproperties

        public virtual bool IsTerminal { get; set; }

        /// <summary>
        ///     Instance variables.
        /// </summary>
        public virtual char Key { get; set; }

        public virtual Dictionary<char, TrieNode> Children { get; set; }
        public virtual TrieNode Parent { get; set; }

        #endregion

        /// <summary>
        ///     Return the word at this node if the node is terminal; otherwise, return null
        /// </summary>
        public virtual string Word
        {
            get
            {
                if (!IsTerminal)
                {
                    return null;
                }

                var curr = this;
                var stack = new Stack<char>();

                while (curr.Parent != null)
                {
                    stack.Push(curr.Key);
                    curr = curr.Parent;
                }

                return new string(stack.ToArray());
            }
        }

        /// <summary>
        ///     Returns an enumerable list of key-value pairs of all the words that start
        ///     with the prefix that maps from the root node until this node.
        /// </summary>
        public virtual IEnumerable<string> GetByPrefix()
        {
            if (IsTerminal)
            {
                yield return Word;
            }

            foreach (var childKeyVal in Children)
            foreach (var terminalNode in childKeyVal.Value.GetByPrefix())
            {
                yield return terminalNode;
            }
        }

        /// <summary>
        ///     Returns an enumerable collection of terminal child nodes.
        /// </summary>
        public virtual IEnumerable<TrieNode> GetTerminalChildren()
        {
            foreach (var child in Children.Values)
            {
                if (child.IsTerminal)
                {
                    yield return child;
                }

                foreach (var grandChild in child.GetTerminalChildren())
                {
                    if (grandChild.IsTerminal)
                    {
                        yield return grandChild;
                    }
                }
            }
        }

        /// <summary>
        ///     Remove this element upto its parent.
        /// </summary>
        public virtual void Remove()
        {
            IsTerminal = false;

            if ((Children.Count == 0) && (Parent != null))
            {
                Parent.Children.Remove(Key);

                if (!Parent.IsTerminal)
                {
                    Parent.Remove();
                }
            }
        }

        /// <summary>
        ///     Clears this node instance
        /// </summary>
        public void Clear()
        {
            Children.Clear();
            Children = null;
        }

        /// <summary>
        ///     IComparer interface implementation
        /// </summary>
        public int CompareTo(TrieNode other)
        {
            if (other == null)
            {
                return -1;
            }

            return Key.CompareTo(other.Key);
        }
    }
}
