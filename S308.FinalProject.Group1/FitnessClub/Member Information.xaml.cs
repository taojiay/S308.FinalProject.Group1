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
            dtgResult.ItemsSource = null;


            //load the membership list from the json file
            memberList = GetDataSetFromFile();
        }
        public List<Member> GetDataSetFromFile()
        {
            List<Member> lstMember = new List<Member>();

            string strFilePath = @"../../../Data/Member.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                lstMember = JsonConvert.DeserializeObject<List<Member>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Membership from file: " + ex.Message);
            }

            return lstMember;
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
            List<Member> memberSearch;

            //validation:
            //check if last name, email or phone is filled
            if (txtLastName.Text == "" && txtEmail.Text == "" && txtPhoneNumber.Text == "")
            {
                MessageBox.Show("One of Last Name, Email and Phone Number is required.");
                return;
            }


            //delcare variables to capture inputs, trim
            string strLastName = txtLastName.Text.Trim();
            string strEmail = txtEmail.Text.Trim();
            string strPhoneNumber = txtPhoneNumber.Text.Trim();
            //clean the datagrid
            dtgResult.ItemsSource = null;
            //run a query with the membership json file

            memberSearch = memberList.Where(m =>
                (m.LastName.StartsWith(strLastName)|| strLastName == "") && 
                (m.Email.StartsWith(strEmail) || strEmail == "") && 
                (m.Phone.StartsWith(strPhoneNumber) || strPhoneNumber == "")
            ).ToList();

            //format the date and price
            foreach(var m in memberSearch)
            {
                m.StartDate.ToShortDateString();
                m.EndDate.ToShortDateString();
                m.MembershipCostPerMonth = Math.Round(m.MembershipCostPerMonth, 2);

            }

            //set the source of the datagrid and refresh
            if (memberSearch.Count()==0)
            {
                MessageBox.Show("No records are found.");
            }
            else
            {
                dtgResult.ItemsSource = memberSearch;
                dtgResult.Items.Refresh();
            }
            


        }


    }
}
