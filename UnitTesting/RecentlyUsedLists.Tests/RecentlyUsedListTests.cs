namespace RecentlyUsedLists.Tests
{
    public class RecentlyUsedListTests
    {
        [Fact]
        public void InitiallyEmpty()
        {
            IRecentlyUsedList list = new RecentlyUsedList();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void AddsItemsInLifoOrder()
        {
            IRecentlyUsedList list = new RecentlyUsedList();
            list.Add("first");
            list.Add("second");
            Assert.Equal("second", list.Get(0));
            Assert.Equal("first", list.Get(1));
        }

        [Fact]
        public void DoesNotAllowDuplicates()
        {
            IRecentlyUsedList list = new RecentlyUsedList();
            list.Add("first");
            list.Add("first");
            Assert.Equal(1, list.Count);
            Assert.Equal("first", list.Get(0));
        }

        [Fact]
        public void MaintainsCapacity()
        {
            IRecentlyUsedList list = new RecentlyUsedList(2);
            list.Add("first");
            list.Add("second");
            list.Add("third");
            Assert.Equal(2, list.Count);
            Assert.Equal("third", list.Get(0));
            Assert.Equal("second", list.Get(1));
        }

        [Fact]
        public void IndexOutOfRangeThrowsException()
        {
            IRecentlyUsedList list = new RecentlyUsedList();
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Get(0));
        }
    }
}
