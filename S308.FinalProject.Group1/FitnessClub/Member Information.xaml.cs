//logo image source: 123RF.com
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
        //Create a lst of member
        List<Member> memberList;
        public Member_Information()
        {
            InitializeComponent();
            //clear all the inputs and results
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            lblMemberInformationResult.Content = "";

            //Initialize list of members
            memberList = new List<Member>();
        }

        //return to main menu method
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        //when click on "clear": clear all the inputs and results
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
        }
        //when click on "search":
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\..\Data\Customers.json";
        }



        
        //validation:
        //check if last name, email or phone is filled

        //delcare variables to capture inputs, trim


        //run a query with the membership json file

        //display result
    }
}
