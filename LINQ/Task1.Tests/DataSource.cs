using System;
using System.Collections.Generic;
using Task1.DoNotChange;

namespace Task1.Tests
{
    internal static class DataSource
    {
        public static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductID = 1,
                ProductName = "Chai",
                Category = "Beverages",
                UnitPrice = 19.0000M,
                UnitsInStock = 39
            },
            new Product
            {
                ProductID = 2,
                ProductName = "Chang",
                Category = "Beverages",
                UnitPrice = 18.0000M,
                UnitsInStock = 17
            },
            new Product
            {
                ProductID = 3,
                ProductName = "Aniseed Syrup",
                Category = "Condiments",
                UnitPrice = 30.0000M,
                UnitsInStock = 13
            },
            new Product
            {
                ProductID = 7,
                ProductName = "Uncle Bob's Organic Dried Pears",
                Category = "Condiments",
                UnitPrice = 10.0000M,
                UnitsInStock = 15
            },
            new Product
            {
                ProductID = 8,
                ProductName = "Northwoods Cranberry Sauce",
                Category = "Condiments",
                UnitPrice = 40.0000M,
                UnitsInStock = 15
            }
        };

        public static readonly List<Supplier> Suppliers = new List<Supplier>
        {
            new Supplier
            {
                SupplierName = "Exotic Liquids",
                Address = "49 Gilbert St.",
                City = "London",
                Country = "UK"
            },
            new Supplier
            {
                SupplierName = "New Orleans Cajun Delights",
                Address = "P.O. Box 78934",
                City = "New Orleans",
                Country = "USA"
            },
            new Supplier
            {
                SupplierName = "Grandma Kelly's Homestead",
                Address = "707 Oxford Rd.",
                City = "Ann Arbor",
                Country = "USA"
            },
            new Supplier
            {
                SupplierName = "Tokyo Traders",
                Address = "9-8 Sekimai Musashino-shi",
                City = "Tokyo",
                Country = "Japan"
            },
            new Supplier
            {
                SupplierName = "Cooperativa de Quesos 'Las Cabras'",
                Address = "Calle del Rosal 4",
                City = "Oviedo",
                Country = "Spain"
            },
            new Supplier
            {
                SupplierName = "Mayumi's",
                Address = "92 Setsuko Chuo-ku",
                City = "Osaka",
                Country = "Japan"
            },
            new Supplier
            {
                SupplierName = "Pavlova, Ltd.",
                Address = "74 Rose St. Moonie Ponds",
                City = "Melbourne",
                Country = "Australia"
            },
            new Supplier
            {
                SupplierName = "Specialty Biscuits, Ltd.",
                Address = "29 King's Way",
                City = "Manchester",
                Country = "UK"
            },
            new Supplier
            {
                SupplierName = "PB Knäckebröd AB",
                Address = "Kaloadagatan 13",
                City = "Göteborg",
                Country = "Sweden"
            },
            new Supplier
            {
                SupplierName = "Refrescos Americanas LTDA",
                Address = "Av. das Americanas 12.890",
                City = "Sao Paulo",
                Country = "Brazil"
            },
            new Supplier
            {
                SupplierName = "Heli Süßwaren GmbH & Co. KG",
                Address = "Tiergartenstraße 5",
                City = "Berlin",
                Country = "Germany"
            }
        };

        public static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                CustomerID = "ALFKI",
                CompanyName = "Alfreds Futterkiste",
                Address = "Obere Str. 57",
                City = "Berlin",
                PostalCode = "12209",
                Country = "Germany",
                Phone = "(012)030-0074321",
                Fax = "(012)030-0076545",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 10643,
                        OrderDate = DateTime.Parse("1997-08-25T00:00:00"),
                        Total = 814.70m
                    },
                    new Order
                    {
                        OrderID = 10692,
                        OrderDate = DateTime.Parse("1997-10-03T00:00:00"),
                        Total = 878.00m
                    },
                    new Order
                    {
                        OrderID = 10702,
                        OrderDate = DateTime.Parse("1997-10-13T00:00:00"),
                        Total = 330m
                    }
                },
                Region = "Germany"
            },
            new Customer
            {
                CustomerID = "ANATR",
                CompanyName = "Ana Trujillo Emparedados y helados",
                Address = "Avda. de la Constitución 2222",
                City = "Mexico D.F.",
                PostalCode = "05021",
                Country = "Mexico",
                Phone = "(5) 555-4729",
                Fax = "(5) 555-3745",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 10308,
                        OrderDate = DateTime.Parse("1996-09-18T00:00:00"),
                        Total = 88.80m
                    },
                    new Order
                    {
                        OrderID = 10625,
                        OrderDate = DateTime.Parse("1997-08-08T00:00:00"),
                        Total = 479.75m
                    },
                    new Order
                    {
                        OrderID = 10759,
                        OrderDate = DateTime.Parse("1997-11-28T00:00:00"),
                        Total = 320m
                    }
                },
                Region = "Mexico"
            },
            new Customer
            {
                CustomerID = "ANTON2",
                CompanyName = "Antonio Moreno Taquería",
                Address = "Mataderos  2312",
                City = "Mexico D.F.",
                PostalCode = "05023",
                Country = "Mexico",
                Phone = "(5) 555-3932",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 10365,
                        OrderDate = DateTime.Parse("1996-11-27T00:00:00"),
                        Total = 403.20m
                    },
                    new Order
                    {
                        OrderID = 10507,
                        OrderDate = DateTime.Parse("1997-04-15T00:00:00"),
                        Total = 749.06m
                    }
                }
            },
            new Customer
            {
                CustomerID = "AROUT",
                CompanyName = "Around the Horn",
                Address = "120 Hanover Sq.",
                City = "London",
                PostalCode = "WA1 1DP",
                Country = "UK",
                Phone = "555-7788",
                Fax = "(171) 555-6750",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 10355,
                        OrderDate = DateTime.Parse("1996-11-15T00:00:00"),
                        Total = 480.00m
                    },
                    new Order
                    {
                        OrderID = 10383,
                        OrderDate = DateTime.Parse("1996-12-16T00:00:00"),
                        Total = 899.5m
                    }
                }
            },
            new Customer
            {
                CustomerID = "ANTON",
                CompanyName = "Antonio Moreno Taquería",
                Address = "Mataderos  2312",
                City = "Mexico D.F.",
                PostalCode = "05f023",
                Country = "Mexico",
                Phone = "(5) 555-3932",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 10365,
                        OrderDate = DateTime.Parse("1996-11-27T00:00:00"),
                        Total = 0.1m
                    },
                    new Order
                    {
                        OrderID = 10507,
                        OrderDate = DateTime.Parse("1997-04-15T00:00:00"),
                        Total = 0.3m
                    }
                },
                Region = "London"
            },
            new Customer
            {
                CustomerID = "AROUT123",
                CompanyName = "Around the Horn",
                Address = "1220 Hanover Sq.",
                City = "London",
                PostalCode = "12374",
                Country = "UK",
                Phone = "(171) 555-7789",
                Fax = "(171) 555-6720",
                Orders = new Order[0],
                Region = "London"
            },
            new Customer
            {
                CustomerID = "WOLZA",
                CompanyName = "Wolski  Zajazd",
                Address = "ul. Filtrowa 68",
                City = "Warszawa",
                PostalCode = "01-012qw",
                Country = "Poland",
                Phone = "642-7012",
                Fax = "642-7012",
                Orders = new []
                {
                    new Order
                    {
                        OrderID = 105037,
                        OrderDate = DateTime.Parse("1996-04-15T00:00:00"),
                        Total = 2m
                    }
                },
                Region = "Poland"
            },
            new Customer
            {
                CustomerID = "WOLZA enc",
                CompanyName = "Wolski  Zajazd",
                Address = "ul. Filtrowa 69",
                City = "Warszawa",
                PostalCode = "abcrebInc",
                Country = "Poland",
                Phone = "(026)642-7013",
                Fax = "(026)642-7013",
                Orders = new Order[0],
                Region = "Poland"
            },
            new Customer
            {
                CustomerID = "QUEEN",
                CompanyName = "Queen Cozinha",
                Address = "Alameda dos Canàrios, 891",
                City = "Sao Paulo",
                PostalCode = "05487020",
                Country = "Brazil",
                Phone = "(11) 555-1189",
                Fax = "(11) 555-1189",
                Orders = new Order[0]
            },
            new Customer
            {
                CustomerID = "LONEP",
                CompanyName = "Lonesome Pine Restaurant",
                Address = "89 Chiaroscuro Rd.",
                City = "USA",
                PostalCode = "97219",
                Country = "Brazil",
                Phone = "555-9573",
                Fax = "555-9573",
                Orders = new Order[0],
                Region = "OR"
            },
        };
    }
}