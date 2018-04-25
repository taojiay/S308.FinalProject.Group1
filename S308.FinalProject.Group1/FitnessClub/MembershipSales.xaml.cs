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
using Newtonsoft.Json;
using System.IO;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MembershipSales.xaml
    /// </summary>
    public partial class MembershipSales : Window
    {
        List<MembershipPrice> MembershipPriceIndex;

        public MembershipSales()
        {
            InitializeComponent();

            //load the membership price list from  the json file
            MembershipPriceIndex = GetDataFromFile();

            //only the membership type is available will be displayed in the drop-down list
        }

        //method: get data from json file
        public List<MembershipPrice> GetDataFromFile()
        {
            List<MembershipPrice> MembershipPricing = new List<MembershipPrice>();

            string strFilePath = @"../../../Data/MembershipPricing.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                MembershipPricing = JsonConvert.DeserializeObject<List<MembershipPrice>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Membership Price from file: " + ex.Message);
            }

            return MembershipPricing;
        }




        //return to main menu method
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        //clear all the inputs and quote result
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cboMembershipType.SelectedIndex = -1;
            dtpMembershipStartDate.SelectedDate = null;
            ckbPersonalTrainingPlan.IsChecked = false;
            ckbLockerRental.IsChecked = false;
            lblPricingQuoteResult.Content = "";

        }

        //Submit to get a quote
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            //validation:
            //check if the membership type is select, if not error message display
            if (cboMembershipType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }
               
            //check if the start date is select, if not error message display
            if (dtpMembershipStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a start date.");
                return;
            }

            //declare variables to capture input
            DateTime datStartDate = (DateTime)dtpMembershipStartDate.SelectedDate;
            DateTime datToday = DateTime.Now;
            ComboBoxItem cbiSelectedMembershipType = (ComboBoxItem)cboMembershipType.SelectedItem;
            string strSelectedMembershipType = cbiSelectedMembershipType.Content.ToString();

            //check if the start date is not in the past, if not error message display
            if (datStartDate < datToday)
            {
                MessageBox.Show("Please select a start date that is not in the past.");
                return;
            }
            
            
        }

        

        


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



    }
}
