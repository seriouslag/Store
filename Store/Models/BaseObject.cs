using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Models
{
    public abstract class BaseObject
    {
        [Required] public int Id { get; set; }
        [Required] public DateTime CreatedDate { get; set; }

        public ICollection<DateTime> ModifiedDates { get; }

        protected BaseObject()
        {
            CreatedDate = DateTime.Now;
            ModifiedDates = new List<DateTime>();
        }

        public DateTime LastModified
        {
            get { return ModifiedDates.Any() ? ModifiedDates.OrderByDescending(md => md.Date).First() : CreatedDate; }
        }

        public void UpdateModifiedDate()
        {
            ModifiedDates.Add(DateTime.Now);
        }

    }
}