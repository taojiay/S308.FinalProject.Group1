using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class FeaturePrice
    {
        //set class properties
        public string FeatureType { get; set; }
        public decimal Price { get; set; }
       


        //set class constructor
        public FeaturePrice()
        {
            FeatureType = "";
            Price = 0;
        }

        //add class method: add membership type price information
        public FeaturePrice(string featureType, decimal price)
        {
            FeatureType = featureType;
            Price = price;
        }
    }
}
