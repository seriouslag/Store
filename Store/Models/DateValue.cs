using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class DateValue
    {
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime DateTime { get; set; }

        public DateValue(int value)
        {
            Value = value;
            DateTime = GetDateTimeNow();
        }
    
        public DateValue(int value, DateTime dateTime)
        {
            Value = value;
            DateTime = dateTime;
        }

        // Probably should move this to a move gloabal function incase ever to
        // decide to change the way to get dates
        protected static DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

        public override string ToString() => $"DateValue: Date:  {DateTime}; Value: {Value};";
    }
}