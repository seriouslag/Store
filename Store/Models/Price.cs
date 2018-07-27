using System.Collections.Generic;
using System.Linq;
using Store.Models.Exceptions;

namespace Store.Models
{
    public class Price
    {
        public int Id { get; set; }

        public ICollection<DatePrice> DatePrices { get; set; }

        public Price(int price)
        {
            if(price < 0) { throw new PriceLessThanZeroException(); }

            DatePrices = new List<DatePrice>
            {
                new DatePrice(price)
            };
        }

        public int GetCurrentValue()
        {
            var a = DatePrices
                .Where(dp => dp.IsValid())
                .OrderByDescending(d => d.DateTime)
                .First();

            return a?.Price ?? int.MaxValue;
        }
    }
}