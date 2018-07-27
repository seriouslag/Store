using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml;

namespace Store.Models
{
    public class Product : BaseObject
    {
        [Index("UniqueKeyNameString", IsUnique = true, Order = 1)]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ProductOption> ProductOptions { get; set; }

        public int GetHighestPrice()
        {
            return ProductOptions.Count > 0 ? ProductOptions.OrderByDescending(po => po.CurrentPrice).First().CurrentPrice : int.MaxValue;
        }

        public int GetLowestPrice()
        {
            return ProductOptions.Count > 0 ? ProductOptions.OrderByDescending(po => po.CurrentPrice).Last().CurrentPrice : int.MaxValue;
        }

        public Product(string name)
        {
            Name = name;
            ProductOptions = new List<ProductOption>();
        }

        public Product(string name, ProductOption productOption)
        {
            Name = name;
            ProductOptions = new List<ProductOption>{productOption};
        }

        public Product(string name, ICollection<ProductOption> productOptions)
        {
            Name = name;
            ProductOptions = productOptions;
        }
    }
}