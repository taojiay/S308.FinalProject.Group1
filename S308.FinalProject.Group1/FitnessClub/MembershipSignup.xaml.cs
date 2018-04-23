//logo image source: 123RF.com, google
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MembershipSignup.xaml
    /// </summary>
    public partial class MembershipSignup : Window
    {
        public MembershipSignup()
        {
            InitializeComponent();
            //clear all the inputs
        }

        //create main menu button function: link with main meanu and close current file

        //the credit card logo will change based on the credit card type selected

        //when click "search" (optional)
            //validation:
            //check if first name, last name, credit card are filled
            //capture inputs
            //declare variables
            //run query for the membership information json file and capture values
            //fill the result in the form

        //when click "submit":
            //validation:
            //check if first name, last name, credit card type, credit card number, phone, email, gender are selected, if not error message display

            //declare variables to capture inputs, trim
            //validation:
            //check if the phone number is 10 digits without other characters
            //check if the email has "@" and "." and with the correct format
            //check if the credit card is validate
            //check if the credit card matches the credit card type
            //check if the age is between 0 and 100
            //check if the weight is between 0 and 500

            //store the data into json file

            //link to MembershipInfo Confirmation 


        //when click "clear": clear all the input

    }
}
