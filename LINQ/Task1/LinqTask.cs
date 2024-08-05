using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));
            if (suppliers == null) throw new ArgumentNullException(nameof(suppliers));

            return customers.Select(c => (
                customer: c,
                suppliers: suppliers.Where(s => s.City == c.City && s.Country == c.Country)
            ));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));
            if (suppliers == null) throw new ArgumentNullException(nameof(suppliers));

            return customers.GroupJoin(
                suppliers,
                c => new { c.City, c.Country },
                s => new { s.City, s.Country },
                (c, s) => (customer: c, suppliers: s)
            );
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return customers
                .Where(c => c.Orders.Sum(o => o.Total) > limit)
                .OrderBy(c => c.Orders.Any() ? c.Orders.Min(o => o.OrderDate.Year) : 0)
                .ThenBy(c => c.Orders.Any() ? c.Orders.Min(o => o.OrderDate.Month) : 0)
                .ThenByDescending(c => c.Orders.Sum(o => o.Total))
                .ThenBy(c => c.CompanyName);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return customers
                .Where(c => c.Orders.Any())
                .Select(c => (customer: c, dateOfEntry: c.Orders.Min(o => o.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return Linq4(customers)
                .OrderBy(r => r.dateOfEntry.Year)
                .ThenBy(r => r.dateOfEntry.Month)
                .ThenByDescending(r => r.customer.Orders.Sum(o => o.Total))
                .ThenBy(r => r.customer.CustomerID);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return customers.Where(c =>
                !c.PostalCode.All(char.IsDigit) ||
                string.IsNullOrEmpty(c.Region) ||
                !c.Phone.Contains("("));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products == null) throw new ArgumentNullException(nameof(products));

            return products
                .GroupBy(p => p.Category)
                .Select(g => new Linq7CategoryGroup
                {
                    Category = g.Key,
                    UnitsInStockGroup = g.GroupBy(p => p.UnitsInStock)
                                         .Select(ug => new Linq7UnitsInStockGroup
                                         {
                                             UnitsInStock = ug.Key,
                                             Prices = ug.OrderBy(p => p.UnitPrice).Select(p => p.UnitPrice)
                                         }).ToList()
                });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null) throw new ArgumentNullException(nameof(products));

            return products
                .GroupBy(p => p.UnitPrice <= cheap ? cheap :
                              p.UnitPrice <= middle ? middle : expensive)
                .Select(g => (category: g.Key, products: g.AsEnumerable()));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(IEnumerable<Customer> customers)
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));

            return customers
                .GroupBy(c => c.City)
                .Select(g => (
                    city: g.Key,
                    averageIncome: (int)Math.Round(g.SelectMany(c => c.Orders).Sum(o => o.Total) / g.Count()),
                    averageIntensity: (int)Math.Round((double)g.Average(c => c.Orders.Count()))
                ));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null) throw new ArgumentNullException(nameof(suppliers));

            return string.Concat(suppliers
                .Select(s => s.Country)
                .Distinct()
                .OrderBy(c => c.Length)
                .ThenBy(c => c));
        }
    }
}
