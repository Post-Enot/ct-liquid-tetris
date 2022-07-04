using System.Collections.Generic;

namespace LiquidTetris.SpecificCollections
{
    public class BorderedQueue<T>
    {
        public BorderedQueue(int maxElementCount)
        {
            MaxElementCount = maxElementCount;
            _queue = new(maxElementCount);
        }

        public int Count => _queue.Count;
        public readonly int MaxElementCount;

        private readonly Queue<T> _queue;

        public bool Enqueue(T element)
        {
            if (Count + 1 <= MaxElementCount)
            {
                _queue.Enqueue(element);
                return true;
            }
            return false;
        }

        public T Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}
