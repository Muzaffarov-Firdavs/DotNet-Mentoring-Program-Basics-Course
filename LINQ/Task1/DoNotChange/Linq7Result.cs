using System.Collections.Generic;

namespace Task1.DoNotChange
{
    public class Linq7CategoryGroup
    {
        public string Category { get; set; }
        public IEnumerable<Linq7UnitsInStockGroup> UnitsInStockGroup { get; set; }
    }

    public class Linq7UnitsInStockGroup
    {
        public int UnitsInStock { get; set; }
        public IEnumerable<decimal> Prices { get; set; }
    }
}
