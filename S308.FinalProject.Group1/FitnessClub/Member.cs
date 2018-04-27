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
            FirstName = "";
            LastName = "";
            Phone = "";
            Email = "";
            CreditCardType = "";
            CreditCardNumber = "";
            Gender = "";
        }

        //method: add member
        public Member(string firstName, string lastName, string phone, string email, string creditcardType, string creditcardNumber, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            CreditCardType = creditcardType;
            CreditCardNumber = creditcardNumber;
            Gender = gender; 
        }
    }
}
