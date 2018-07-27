using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Store.Models.Exceptions
{

    [Serializable]
    public class DateNotCurrentOrFutureException : Exception
    {
        public DateNotCurrentOrFutureException()
        {

        }

        public DateNotCurrentOrFutureException(DateTime date) : base($"Date cannot be in the past: {date.ToString(CultureInfo.CurrentCulture)}")
        {

        }
    }

    [Serializable]
    public class DateValueNotValudException : Exception
    {
        public DateValueNotValudException()
        {

        }

        public DateValueNotValudException(DateValue dateValue) : base($"DateValue is not valid: {dateValue}")
        {

        }
    }

    [Serializable]
    public class NoNameFoundException : Exception
    {
        public NoNameFoundException()
        {

        }
    }

    [Serializable]
    public class OrderItemQuanityLessThanZeroException : Exception
    {
        public OrderItemQuanityLessThanZeroException()
        {

        }
    }

    [Serializable]
    public class PriceLessThanZeroException : Exception
    {
        public PriceLessThanZeroException()
        {

        }
    }
}