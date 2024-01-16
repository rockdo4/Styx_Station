using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    private SortedDictionary<TPriority, Queue<TElement>> priorityQueue;
    public int Count
    {
        get { return priorityQueue.Count; }
    }

    public PriorityQueue()
    {
        priorityQueue = new SortedDictionary<TPriority, Queue<TElement>>();
    }

    public void Enqueue(TElement item, TPriority priority)
    {
        if (!priorityQueue.ContainsKey(priority))
        {
            priorityQueue[priority] = new Queue<TElement>();
        }

        priorityQueue[priority].Enqueue(item);
    }

    public TElement Dequeue()
    {
        if (priorityQueue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        var highestPriority = priorityQueue.Keys.Min();
        var queue = priorityQueue[highestPriority];
        var item = queue.Dequeue();

        if (queue.Count == 0)
        {
            priorityQueue.Remove(highestPriority);
        }

        return item;
    }

    public bool IsEmpty
    {
        get { return priorityQueue.Count == 0; }
    }
}
