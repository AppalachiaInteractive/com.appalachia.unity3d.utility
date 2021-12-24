using System.Collections.Generic;

namespace Appalachia.Utility.DataStructures.Trees
{
    public interface IBinarySearchTree<T>
        where T : System.IComparable<T>
    {
        // Returns true if tree allows inserting duplicates; otherwise, false
        bool AllowsDuplicates { get; }

        // Checks if the tree is empty.
        bool IsEmpty { get; }

        // Returns a copy of the tree root
        BSTNode<T> Root { get; }

        // Returns the number of elements in the Tree
        int Count { get; }

        // Returns the height of the tree.
        int Height { get; }

        // Clear this tree.
        void Clear();

        // Check for the existence of an item
        bool Contains(T item);

        // Find the element in the tree, returns null if not found.
        T Find(T item);

        // Finds all the elements in the tree that match the predicate.
        IEnumerable<T> FindAll(System.Predicate<T> searchPredicate);

        // Finds the maximum element.
        T FindMax();

        // Finds the minimum element.
        T FindMin();

        // Returns an enumerator that visits node in the order: left child, parent, right child
        IEnumerator<T> GetInOrderEnumerator();

        // Returns an enumerator that visits node in the order: left child, right child, parent
        IEnumerator<T> GetPostOrderEnumerator();

        // Returns an enumerator that visits node in the order: parent, left child, right child
        IEnumerator<T> GetPreOrderEnumerator();

        // Inserts an element to the tree
        void Insert(T item);

        // Inserts an array of elements to the tree.
        void Insert(T[] collection);

        // Inserts a list of items to the tree.
        void Insert(List<T> collection);

        // Remove an element from tree
        void Remove(T item);

        // Removes the max value from tree
        void RemoveMax();

        // Removes the min value from tree
        void RemoveMin();

        // Return an array of the tree elements
        T[] ToArray();

        // Return an array of the tree elements
        List<T> ToList();
    }

    /// <summary>
    ///     The itemed version of the Binary Search Tree.
    /// </summary>
    /// <typeparam name="TKey">Type of items.</typeparam>
    /// <typeparam name="TValue">Type of records per node.</typeparam>
    public interface IBinarySearchTree<TKey, TValue>
        where TKey : System.IComparable<TKey>
    {
        bool AllowsDuplicates { get; }
        bool IsEmpty { get; }
        BSTMapNode<TKey, TValue> Root { get; }
        int Count { get; }
        int Height { get; }
        void Clear();
        bool Contains(TKey item);
        KeyValuePair<TKey, TValue> Find(TKey item);
        IEnumerable<KeyValuePair<TKey, TValue>> FindAll(System.Predicate<TKey> searchPredicate);
        KeyValuePair<TKey, TValue> FindMax();
        KeyValuePair<TKey, TValue> FindMin();
        void Insert(TKey key, TValue value);
        void Insert(KeyValuePair<TKey, TValue> keyValuePair);
        void Insert(KeyValuePair<TKey, TValue>[] collection);
        void Insert(List<KeyValuePair<TKey, TValue>> collection);
        void Remove(TKey item);
        void RemoveMax();
        void RemoveMin();
        KeyValuePair<TKey, TValue>[] ToArray();
        List<KeyValuePair<TKey, TValue>> ToList();
    }
}
