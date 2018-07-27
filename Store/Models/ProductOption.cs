using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Store.Models.Exceptions;

namespace Store.Models
{
    public class ProductOption : BaseObject
    {
        public string Sku => GetSku();
        public string Name { get; set; }
        private readonly Price _price;
        public int CurrentPrice => GetCurrentPrice();
        public string ProductOptionDescription { get; set; }
        public int QuantityInStock { get; set; }

        public ProductOption(string name, int basePrice, int quantityInStock = 0, string productOptionDescription = "")
        {
            Name = name;
            _price = new Price(basePrice);
            ProductOptionDescription = productOptionDescription;
            QuantityInStock = quantityInStock;
        }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int GetCurrentPrice() => _price.GetCurrentValue();

        // This sets the new base price
        public void UpdateBasePrice(int price) => UpdatePrice(price);

        public void UpdatePrice(int price, long lengthInMilli = 0) => UpdatePrice(price, DateTime.Now, lengthInMilli);


        // Use to set the price for a future date
        public void UpdatePrice(int price, DateTime dateTime, long lengthInMilli = 0)
        {
            // Cannot add prices to the past
            if (DateTime.Compare(dateTime, DateTime.Now) < 0)
            {
                throw new DateNotCurrentOrFutureException(dateTime);
            }
            
            UpdatePrice(new DatePrice(price, dateTime, lengthInMilli));
        }

        // Use to set the price for a future date
        public void UpdatePrice(DatePrice datePrice)
        {
            // Cannot add prices to the past
            if (!datePrice.IsValid()) { throw new DateValueNotValudException(datePrice); }

            _price.DatePrices.Add(datePrice);
            UpdateModifiedDate();
        }

        private string GetSku()
        {

            char[] spaceChars = { ' ', '-' };
            var foundChars = "";

            var fillInChar = System.Configuration.ConfigurationManager.AppSettings["SkuFillInChar"] ?? "X";

            // If Super product as an Name get the first char
            if (!string.IsNullOrWhiteSpace(Product.Name))
            {
                foundChars += Product.Name.Substring(0, 1);
            }

            // All Product Options should have a name, kinda the point
            foundChars += Name.Substring(0, 1);
            var name = Name;
            
            // Overkill; find the next char to add to the sku, should be the next char a 'spaceChar'
            foreach (var spaceChar in spaceChars)
            {
                for (var i = name.IndexOf(spaceChar); i > -1 && foundChars.Length < 3; i = name.IndexOf(spaceChar, i + 1))
                {
                    foundChars += name.Substring(name.IndexOf(spaceChar, i), 0);
                }
            }

            // Round it out
            switch (foundChars.Length)
            {
                case 0:
                    throw new NoNameFoundException();
                case 1:
                    foundChars += fillInChar;
                    goto case 2;
                case 2:
                    // If ProductOption's Name only has 1 foundChar or Super Product Name is empty and
                    // ProductOptions's name is 2 chars then insert X inbetween chars. Example SXB100
                    foundChars.Insert(1, fillInChar);
                    goto case 3;
                case 3:
                    return foundChars;
                default:
                    return foundChars.Substring(0, 3);
            }

        }

    }
}