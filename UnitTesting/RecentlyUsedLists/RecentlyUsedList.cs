namespace RecentlyUsedLists
{
    public class RecentlyUsedList : IRecentlyUsedList
    {
        private LinkedList<string> _items;
        private int _capacity;

        public RecentlyUsedList(int capacity = 5)
        {
            _items = new LinkedList<string>();
            _capacity = capacity;
        }

        public void Add(string item)
        {
            if (string.IsNullOrEmpty(item)) return;

            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
            else if (_items.Count == _capacity)
            {
                _items.RemoveLast();
            }

            _items.AddFirst(item);
        }

        public string Get(int index)
        {
            if (index < 0 || index >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _items.ElementAt(index);
        }

        public int Count => _items.Count;
    }
}
