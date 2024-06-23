namespace RecentlyUsedLists
{
    public interface IRecentlyUsedList
    {
        void Add(string item);
        string Get(int index);
        int Count { get; }
    }
}
