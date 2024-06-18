using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int Length => count;

        public void Add(T e)
        {
            Node<T> newNode = new Node<T>(e);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            count++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException();

            Node<T> newNode = new Node<T>(e);

            if (index == 0)
            {
                newNode.Next = this.head;
                newNode.Prev = null;

                if (this.head != null)
                {
                    this.head.Prev = newNode;
                }

                this.head = newNode;
            }
            else
            {
                var current = this.head;
                int count = 0;

                while (current != null && count < index - 1)
                {
                    current = current.Next;
                    count++;
                }

                if (current == null)
                {
                    return;
                }

                newNode.Next = current.Next;
                newNode.Prev = current;

                if (current.Next != null)
                {
                    current.Next.Prev = newNode;
                }

                current.Next = newNode;
            }

            count++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node<T> current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        public void Remove(T item)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    if (current == head)
                    {
                        head = current.Next;
                        if (head != null)
                            head.Prev = null;
                    }
                    else if (current == tail)
                    {
                        tail = current.Prev;
                        if (tail != null)
                            tail.Next = null;
                    }
                    else
                    {
                        current.Prev.Next = current.Next;
                        current.Next.Prev = current.Prev;
                    }
                    count--;
                    return;
                }
                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node<T> current = head;
            if (index == 0)
            {
                head = current.Next;
                if (head != null)
                    head.Prev = null;
            }
            else if (index == count - 1)
            {
                current = tail;
                tail = current.Prev;
                if (tail != null)
                    tail.Next = null;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }
            count--;
            return current.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
