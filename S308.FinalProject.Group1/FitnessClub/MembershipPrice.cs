using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class MembershipPrice
    {
        //set class properties
        public string MembershipType { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }


        //set class constructor
        public MembershipPrice()
        {
            MembershipType = "";
            Price = 0;
            Availability = true;
        }

        //add class method: add membership type price information
        public MembershipPrice(string membershipType, decimal price, bool availability)
        {
            MembershipType = membershipType;
            Price = price;
            Availability = availability;
        }
    }
}
