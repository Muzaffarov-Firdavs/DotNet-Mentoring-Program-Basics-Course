using System;

namespace Task1
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is Product other)
            {
                return this.Name == other.Name && this.Price == other.Price;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price);
        }
    }
}
