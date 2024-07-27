using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tests
{
    [TestFixture]
    public class Tests
    {
        [TestCase(6250, ExpectedResult = 0)]
        [TestCase(0, ExpectedResult = 6)]
        [TestCase(-1, ExpectedResult = 10)]
        [TestCase(1, ExpectedResult = 5)]
        public int Linq1_Limit_ReturnsCustomersCount(decimal limit)
        {
            return LinqTask.Linq1(DataSource.Customers, limit).Count();
        }

        [Test]
        public void Linq1_NullSource_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq1(null, 42).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq2_CustomersAndSuppliers_2CustomersHaveSuppliers()
        {
            var result = LinqTask.Linq2(DataSource.Customers, DataSource.Suppliers).ToList();

            Assert.That(() => result.Count, Is.EqualTo(DataSource.Customers.Count));
            foreach (var (customer, suppliers) in result)
            {
                foreach (var supplier in suppliers)
                {
                    StringAssert.AreEqualIgnoringCase(customer.City, supplier.City);
                    StringAssert.AreEqualIgnoringCase(customer.Country, supplier.Country);
                }
            }
        }

        [Test]
        public void Linq2_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq2(null, null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq2UsingGroup_CustomersAndSuppliers_2CustomersHaveSuppliers()
        {
            var result = LinqTask.Linq2UsingGroup(DataSource.Customers, DataSource.Suppliers).ToList();

            Assert.That(() => result.Count, Is.EqualTo(DataSource.Customers.Count));
            foreach (var (customer, suppliers) in result)
            {
                foreach (var supplier in suppliers)
                {
                    StringAssert.AreEqualIgnoringCase(customer.City, supplier.City);
                    StringAssert.AreEqualIgnoringCase(customer.Country, supplier.Country);
                }
            }
        }

        [Test]
        public void Linq2UsingGroup_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq2UsingGroup(null, null).ToList(), Throws.ArgumentNullException);
        }

        [TestCase(800, ExpectedResult = 2)]
        [TestCase(0, ExpectedResult = 6)]
        [TestCase(-1, ExpectedResult = 6)]
        [TestCase(1, ExpectedResult = 5)]
        public int Linq3_Limit_ReturnsCustomersCount(decimal limit)
        {
            return LinqTask.Linq3(DataSource.Customers, limit).Count();
        }

        [Test]
        public void Linq3_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq3(null, 42).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq4_Customers_CustomersAndDateOfEntry()
        {
            var result = LinqTask.Linq4(DataSource.Customers).ToList();

            Assert.That(() => result.Count, Is.EqualTo(DataSource.Customers.Count - 4));
            foreach (var (customer, dateOfEntry) in result)
            {
                Assert.That(FindCustomerOrdersMinDate(customer), Is.EqualTo(dateOfEntry));
            }
        }

        [Test]
        public void Linq4_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq4(null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq5_Customers_CustomersAndDateOfEntry()
        {
            var result = LinqTask.Linq5(DataSource.Customers).ToList();

            Assert.That(() => result.Count, Is.EqualTo(DataSource.Customers.Count - 4));
            foreach (var (customer, dateOfEntry) in result)
            {
                Assert.That(FindCustomerOrdersMinDate(customer), Is.EqualTo(dateOfEntry));
            }

            Assert.That(result[0].customer, Is.EqualTo(DataSource.Customers[6]));
            Assert.That(result[1].customer, Is.EqualTo(DataSource.Customers[1]));
            Assert.That(result[2].customer, Is.EqualTo(DataSource.Customers[3]));
            Assert.That(result[3].customer, Is.EqualTo(DataSource.Customers[2]));
            Assert.That(result[4].customer, Is.EqualTo(DataSource.Customers[4]));
            Assert.That(result[5].customer, Is.EqualTo(DataSource.Customers[0]));
        }

        [Test]
        public void Linq5_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq5(null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq6_Customers_ReturnsFilteredCustomers()
        {
            var expectedResult = DataSource.Customers.ToList();
            expectedResult.RemoveAt(0);
            expectedResult.RemoveAt(0);
            expectedResult.RemoveAt(3);

            var result = LinqTask.Linq6(DataSource.Customers).ToList();

            CollectionAssert.AreEquivalent(expectedResult, result);
        }

        [Test]
        public void Linq6_NullCustomer_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq6(null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq7_Customers_Returns5()
        {
            var expectedResult = new[]
            {
                new Linq7CategoryGroup
                {
                    Category = "Beverages",
                    UnitsInStockGroup = new[]
                    {
                        new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = 39,
                            Prices = new [] { 19.0000M }
                        },
                        new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = 17,
                            Prices = new [] { 18.0000M }
                        }
                    }
                },
                new Linq7CategoryGroup
                {
                    Category = "Condiments",
                    UnitsInStockGroup = new[]
                    {
                        new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = 15,
                            Prices = new [] { 10.0000M, 40.0000M }
                        },
                        new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = 13,
                            Prices = new [] { 30.0000M }
                        }
                    }
                }
            };

            var result = LinqTask.Linq7(DataSource.Products);

            foreach (var categoryGroup in result)
            {
                var expectedCategoryGroup = expectedResult.Single(_ => _.Category == categoryGroup.Category);
                foreach (var unitInStockGroup in categoryGroup.UnitsInStockGroup)
                {
                    var expectedUnitInStockGroup = expectedCategoryGroup
                        .UnitsInStockGroup.Single(_ => _.UnitsInStock == unitInStockGroup.UnitsInStock);
                    CollectionAssert.AreEqual(expectedUnitInStockGroup.Prices, unitInStockGroup.Prices);
                }
            }
        }

        [Test]
        public void Linq7_NullProducts_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq7(null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq8_Products_ReturnsGroupedProducts()
        {
            decimal cheap = 10, middle = 30, expensive = 40;
            var result = LinqTask.Linq8(DataSource.Products, cheap, middle, expensive).ToList();

            var cheapProducts = result.Single(_ => _.category == cheap).products;
            Assert.That(cheapProducts.Count(), Is.EqualTo(1));
            var middleProducts = result.Single(_ => _.category == middle).products;
            Assert.That(middleProducts.Count(), Is.EqualTo(3));
            var expensiveProducts = result.Single(_ => _.category == expensive).products;
            Assert.That(expensiveProducts.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Linq8_NullProducts_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq8(null, 42, 42, 42).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq9_Customers_ReturnsGroupedProducts()
        {
            var expected = new List<(string city, int averageIncome, int averageIntensity)>
            {
                ("Berlin", 2023, 3),
                ("Mexico D.F.", 680, 2),
                ("London", 690, 1),
                ("Warszawa", 1, 0),
                ("Sao Paulo", 0, 0),
                ("USA", 0, 0)
            };

            var result = LinqTask.Linq9(DataSource.Customers).ToList();

            foreach (var valueTuple in result)
            {
                var expectedValue = expected.Single(_ => _.city == valueTuple.city);
                Assert.That(expectedValue.averageIncome, Is.EqualTo(valueTuple.averageIncome));
                Assert.That(expectedValue.averageIntensity, Is.EqualTo(valueTuple.averageIntensity));
            }
        }

        [Test]
        public void Linq9_NullCustomers_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq9(null).ToList(), Throws.ArgumentNullException);
        }

        [Test]
        public void Linq10_Suppliers_ReturnsAggregateString()
        {
            string result = LinqTask.Linq10(DataSource.Suppliers);
            StringAssert.AreEqualIgnoringCase("UKUSAJapanSpainBrazilSwedenGermanyAustralia", result);
        }

        [Test]
        public void Linq10_NullSuppliers_ThrowsArgumentNullException()
        {
            Assert.That(() => LinqTask.Linq10(null).ToList(), Throws.ArgumentNullException);
        }

        private static DateTime FindCustomerOrdersMinDate(Customer customer)
        {
            var min = DateTime.MaxValue;
            foreach (var order in customer.Orders)
            {
                if (order.OrderDate < min)
                {
                    min = order.OrderDate;
                }
            }

            return min;
        }
    }
}
