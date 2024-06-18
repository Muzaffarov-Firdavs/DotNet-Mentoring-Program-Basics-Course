using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> storage;

        public HybridFlowProcessor()
        {
            storage = new DoublyLinkedList<T>();
        }


        public T Dequeue()
        {
            if (storage.Length == 0)
                throw new InvalidOperationException("The queue is empty");

            return storage.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            storage.Add(item);
        }

        public T Pop()
        {
            if (storage.Length == 0)
                throw new InvalidOperationException("The stack is empty");

            return storage.RemoveAt(storage.Length - 1);
        }

        public void Push(T item)
        {
            storage.Add(item);
        }
    }
}
