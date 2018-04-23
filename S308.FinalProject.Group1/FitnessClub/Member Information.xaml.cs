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
    /// Interaction logic for Member_Information.xaml
    /// </summary>
    public partial class Member_Information : Window
    {
        public Member_Information()
        {
            InitializeComponent();
            //clear all the inputs and results
        }

        //when click on "clear": clear all the inputs and results

        //when click on "search":
            //validation:
            //check if last name, email and phone are filled

            //delcare variables to capture inputs, trim
            //validation:
            //email validation ("@" and "." with correct format)
            //phone validation (10 digits without any other characters)

            //run a query with the membership json file

            //display result
    }
}
