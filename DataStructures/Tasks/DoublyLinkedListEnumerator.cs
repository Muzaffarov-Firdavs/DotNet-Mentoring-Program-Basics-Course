using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> head;
        private Node<T> current;

        public DoublyLinkedListEnumerator(Node<T> head)
        {
            this.head = head;
            current = null;
        }

        public T Current
        {
            get
            {
                if (current == null)
                    throw new InvalidOperationException();
                return current.Data;
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (current == null)
            {
                current = head;
            }
            else
            {
                current = current.Next;
            }
            return current != null;
        }

        public void Reset()
        {
            current = null;
        }

        public void Dispose() { }
    }
}
