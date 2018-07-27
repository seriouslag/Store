using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class DatePrice: DateValue
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public long LengthInMilli { get; set; }

        public DatePrice(int price, long lengthInMilli = 0) : base(price)
        {
            Price = price;
            LengthInMilli = lengthInMilli;
            DateTime = GetDateTimeNow();
        }

        public DatePrice(int price, DateTime dateTime, long lengthInMilli = 0) : base (price, dateTime)
        {
            Price = price;
            LengthInMilli = lengthInMilli;
            DateTime = dateTime;
        }

        public DatePrice(DateValue dateValue, long lengthInMilli = 0) : base(dateValue.Value, dateValue.DateTime)
        {
            Price = dateValue.Value;
            LengthInMilli = lengthInMilli;
            DateTime = dateValue.DateTime;
        }

        public bool IsValid()
        {
            // Future prices are not valid yet
            if (DateTime.Compare(this.DateTime, DateTime.Now) < 0) { return false; }

            // Length of 0 mean new base price
            if (LengthInMilli == 0) { return true; }

            return DateTime.Compare(DateTime.Now, this.DateTime.AddMilliseconds(LengthInMilli)) <= 0;
        }

        public long GetTimeTillInvalid()
        {
            if (!IsValid()) { return -1; }

            return LengthInMilli == 0
                ? 0
                : DateTime.Compare(DateTime.Now, this.DateTime.AddMilliseconds(LengthInMilli));
        }

        public override string ToString() => $"DateValue: Price:  {Price}; LengthInMilli: {LengthInMilli}; Price: {Price}; GetTimeTillInvalid: {GetTimeTillInvalid()}";
    }
}