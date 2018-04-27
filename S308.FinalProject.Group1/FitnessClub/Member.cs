using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Member
    {
        //set class properties 
        public string MembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MembershipCostPerMonth { get; set; }
        public decimal Subtotal { get; set; }
        public string AdditionalFeatures { get; set; }
        public decimal Total { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string Gender { get; set; }
        public byte Age { get; set; }
        public short Weight { get; set; }
        public string PersonalFitnessGoal { get; set; }

        //set class constructor
        public Member()
        {
            MembershipType = "";
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            MembershipCostPerMonth = 0;
            Subtotal = 0;
            AdditionalFeatures = "";
            Total = 0;
            FirstName = "";
            LastName = "";
            Phone = "";
            Email = "";
            CreditCardType = "";
            CreditCardNumber = "";
            Gender = "";
        }

        //method: add member info
        public Member (string membershipType, DateTime startDate, DateTime endDate, decimal membershipCostPerMonth, decimal subtotal, string additionalFeatures, decimal total)
        {
            MembershipType = membershipType;
            StartDate = startDate;
            EndDate = endDate;
            MembershipCostPerMonth = membershipCostPerMonth;
            Subtotal = subtotal;
            AdditionalFeatures = additionalFeatures;
            Total = total; 
        }

        public Member(Member quoteinfo, string firstName, string lastName, string phone, string email, string creditcardType, string creditcardNumber, string gender, byte age, short weight, string personalfitnessGoal)
        {
            MembershipType = quoteinfo.MembershipType;
            StartDate = quoteinfo.StartDate;
            EndDate = quoteinfo.EndDate;
            MembershipCostPerMonth = quoteinfo.MembershipCostPerMonth;
            Subtotal = quoteinfo.Subtotal;
            AdditionalFeatures = quoteinfo.AdditionalFeatures;
            Total = quoteinfo.Total; 
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            CreditCardType = creditcardType;
            CreditCardNumber = creditcardNumber;
            Gender = gender;
            Age = age;
            Weight = weight;
            PersonalFitnessGoal = personalfitnessGoal;
        }

    }
}
