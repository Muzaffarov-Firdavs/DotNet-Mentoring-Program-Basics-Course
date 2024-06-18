namespace Tasks
{
    public class Node<T>
    {
        public T Data;
        public Node<T> Next;
        public Node<T> Prev;

        public Node(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
