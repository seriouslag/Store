using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Models
{
    public class Address : BaseObject
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int PostalCode { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string SavedName { get; set; }

        public Address(string city, string country, string line1, int postalCode, string state, string name, int phone = 0, string savedName = "", string line2 = "")
        {
            City = city;
            Country = country;
            Line1 = line1;
            PostalCode = postalCode;
            State = state;
            Name = name;
            Phone = phone;
            SavedName = savedName;
            Line2 = line2;
        }

        public Address(string city, string country, string line1, int postalCode, string state, string name, string savedName, int phone = 0, string line2 = "")
        {
            City = city;
            Country = country;
            Line1 = line1;
            PostalCode = postalCode;
            State = state;
            Name = name;
            Phone = phone;
            SavedName = savedName;
            Line2 = line2;
        }

    }
}