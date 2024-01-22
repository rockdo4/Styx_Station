using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    //private SortedDictionary<TPriority, Queue<TElement>> priorityQueue;
    //public int Count
    //{
    //    get { return priorityQueue.Count; }
    //}

    //public PriorityQueue()
    //{
    //    priorityQueue = new SortedDictionary<TPriority, Queue<TElement>>();
    //}

    //public void Enqueue(TElement item, TPriority priority)
    //{
    //    if (!priorityQueue.ContainsKey(priority))
    //    {
    //        priorityQueue[priority] = new Queue<TElement>();
    //    }

    //    priorityQueue[priority].Enqueue(item);
    //}

    //public TElement Dequeue()
    //{
    //    if (priorityQueue.Count == 0)
    //    {
    //        throw new InvalidOperationException("Queue is empty");
    //    }

    //    var highestPriority = priorityQueue.Keys.Min();
    //    var queue = priorityQueue[highestPriority];
    //    var item = queue.Dequeue();

    //    if (queue.Count == 0)
    //    {
    //        priorityQueue.Remove(highestPriority);
    //    }

    //    return item;
    //}

    //public bool IsEmpty
    //{
    //    get { return priorityQueue.Count == 0; }
    //}

    private List<Tuple<TPriority, TElement>> tree;

    public PriorityQueue()
    {
        tree = new List<Tuple<TPriority, TElement>>();
    }

    public int Count
    {
        get { return tree.Count; }
    }

    public void Enqueue(TElement element, TPriority priority)
    {
        tree.Add(new Tuple<TPriority, TElement>(priority, element));
        HeapifyUp();
    }

    public Tuple<TPriority, TElement> Dequeue()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("PriorityQueue is empty");
        }

        Tuple<TPriority, TElement> frontItem = tree[0];
        int lastIndex = Count - 1;

        tree[0] = tree[lastIndex];
        tree.RemoveAt(lastIndex);

        if (Count > 1)
        {
            HeapifyDown();
        }

        return frontItem;
    }

    private void HeapifyUp()
    {
        int childIndex = Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;

            if (tree[childIndex].Item1.CompareTo(tree[parentIndex].Item1) >= 0)
            {
                break;
            }

            SwapElements(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    private void HeapifyDown()
    {
        int currentIndex = 0;

        while (true)
        {
            int leftChildIndex = 2 * currentIndex + 1;
            int rightChildIndex = 2 * currentIndex + 2;

            int smallestChildIndex = currentIndex;

            if (leftChildIndex < Count && tree[leftChildIndex].Item1.CompareTo(tree[smallestChildIndex].Item1) < 0)
            {
                smallestChildIndex = leftChildIndex;
            }

            if (rightChildIndex < Count && tree[rightChildIndex].Item1.CompareTo(tree[smallestChildIndex].Item1) < 0)
            {
                smallestChildIndex = rightChildIndex;
            }

            if (smallestChildIndex == currentIndex)
            {
                break;
            }

            SwapElements(currentIndex, smallestChildIndex);
            currentIndex = smallestChildIndex;
        }
    }

    private void SwapElements(int index1, int index2)
    {
        Tuple<TPriority, TElement> temp = tree[index1];
        tree[index1] = tree[index2];
        tree[index2] = temp;
    }
}
