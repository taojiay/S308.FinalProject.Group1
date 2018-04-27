using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class FeaturesPrice
    {
        //set class properties
        public string FeaturesType { get; set; }
        public decimal Price { get; set; }


        //set class constructor
        public FeaturesPrice()
        {
            FeaturesType = "";
            Price = 0;
        }

        //add class method: add feature price information
        public FeaturesPrice(string featuresType, decimal price)
        {
            FeaturesType = featuresType;
            Price = price;
        }
    }
}
