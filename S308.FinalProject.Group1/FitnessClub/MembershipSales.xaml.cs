//logo image source: 123RF.com
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
    /// Interaction logic for MembershipSales.xaml
    /// </summary>
    public partial class MembershipSales : Window
    {
        public MembershipSales()
        {
            InitializeComponent();
            //clear all the inputs and result

            //only the membership type is available will be displayed in the drop-down list
        }

        //when hit "submit" button:

            //validation:
            //check if the membership type is select, if not error message display
            //check if the start date is select, if not error message display
            //check if the start date is not in the past, if not error message display
            //check if the end date is select, if not error message display
            //check if the end date is greater than the start date, if not error message display

            //declare variables
            //capture of all the inputs

            //calculate the month selected 

            //validation:
            //check if 12 month plan is selected, make sure the selected month is 12, 24, 36, etc. Otherwise, error message display

            //retrieve the pricing information (membership type + additional feature) from the json file

            //calculate the subtotal: price * month(or how many 12 month)

            //calculate additional feature: price * month

            //calculate total 

            //display result when click on submit

        //create cancel button function: back to main menu and close current window

        //Only after the quote preview can click on sign up 
        //link sign up button with MembershipSignup and close current window


        //create main menu button function and close current window
    }
}
