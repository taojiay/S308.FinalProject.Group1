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
        List<FeaturesPrice> FeaturesPriceIndex;

        //declare variables to pass on the information to next screen
        string strmembershiptype;
        DateTime datstartdate;
        DateTime datenddate;
        decimal decmembershipcostpermonth;
        decimal decsubtotal;
        string stradditionalfeatures;
        decimal dectotal;

        public MembershipSales()
        {
            InitializeComponent();

            //clear all the inputs and results
            cboMembershipType.SelectedIndex = -1;
            dtpMembershipStartDate.SelectedDate = null;
            ckbPersonalTrainingPlan.IsChecked = false;
            ckbLockerRental.IsChecked = false;
            lblPricingQuoteResult.Content = "";



            //load the membership price list from  the json file
            MembershipPriceIndex = GetMembershipPriceDataFromFile();
            FeaturesPriceIndex = GetFeaturesPriceDataFromFile();

            //only the membership type is available will be displayed in the drop-down list
            foreach (var a in MembershipPriceIndex)
                if (a.Availability == true)
                {
                    ComboBoxItem cbiItem = new ComboBoxItem();
                    cbiItem.Content = a.MembershipType;
                    cboMembershipType.Items.Add(cbiItem);
                }
                    

        }

        //method: get membership price data from json file
        public List<MembershipPrice> GetMembershipPriceDataFromFile()
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

        //method: get additional features price data from json file
        public List<FeaturesPrice> GetFeaturesPriceDataFromFile()
        {
            List<FeaturesPrice> FeaturesPricing = new List<FeaturesPrice>();

            string strFilePath = @"../../../Data/AdditionalFeaturePricing.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                FeaturesPricing = JsonConvert.DeserializeObject<List<FeaturesPrice>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Membership Price from file: " + ex.Message);
            }

            return FeaturesPricing;
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
            DateTime datToday = DateTime.Today;
            ComboBoxItem cbiSelectedMembershipType = (ComboBoxItem)cboMembershipType.SelectedItem;
            string strSelectedMembershipType = cbiSelectedMembershipType.Content.ToString();
            

            //check if the start date is not in the past, if not error message display
            if (datStartDate < datToday)
            {
                MessageBox.Show("Please select a start date that is not in the past.");
                return;
            }


            //determine whether the selected type is one month or 12 month to calculate the end date
            int intMonth;
            DateTime datEndDate;
            if (strSelectedMembershipType.Contains("12 Month"))
                intMonth = 12;
            else
                intMonth = 1;

            datEndDate = datStartDate.AddMonths(intMonth);

            //find the membership price
            decimal decMembershipPrice = 0;

            foreach (var i in MembershipPriceIndex)
                if (i.MembershipType == strSelectedMembershipType)
                    decMembershipPrice = i.Price;

            //calculate membership cost per month (membershipprice/month)
            decimal decMonthlyMembershipPrice;
            decMonthlyMembershipPrice = decMembershipPrice / Convert.ToDecimal(intMonth);
            

            //retrieve monthly price for each additional feature
            string strPersonalTrainingPlan = "Personal Training Plan";
            string strLockerRental = "Locker Rental";
            decimal decMonthlyPersonalTrainingPlan = 0;
            decimal decMonthlyLockerRental = 0;

            foreach (var x in FeaturesPriceIndex)
                if (x.FeaturesType == strPersonalTrainingPlan)
                    decMonthlyPersonalTrainingPlan = x.Price;

            foreach (var y in FeaturesPriceIndex)
                if (y.FeaturesType == strLockerRental)
                    decMonthlyLockerRental = y.Price;

            //declare variables to indicate if the additional features are checked and calculate the additional feature price per month;
            decimal decMonthlyFeaturesPrice;
            decimal decTotalFeaturesPrice;
            string strFeatures;


            if (ckbPersonalTrainingPlan.IsChecked == true && ckbLockerRental.IsChecked == true)
            { 
                decMonthlyFeaturesPrice = decMonthlyLockerRental + decMonthlyPersonalTrainingPlan;
                strFeatures = strPersonalTrainingPlan + Environment.NewLine + strLockerRental.PadLeft(48);
            }
            else if (ckbPersonalTrainingPlan.IsChecked == true && ckbLockerRental.IsChecked == false)
            {
                decMonthlyFeaturesPrice = decMonthlyPersonalTrainingPlan;
                strFeatures = strPersonalTrainingPlan;
            } 
            else if (ckbPersonalTrainingPlan.IsChecked == false && ckbLockerRental.IsChecked == true)
            {
                decMonthlyFeaturesPrice = decMonthlyLockerRental;
                strFeatures = strLockerRental;
            }
            else
            {
                decMonthlyFeaturesPrice = 0;
                strFeatures = "none";
            }
                
            decTotalFeaturesPrice = decMonthlyFeaturesPrice * Convert.ToDecimal(intMonth);

            //calculate total price
            decimal decTotalPrice;
            decTotalPrice = decMembershipPrice + decTotalFeaturesPrice;

            //Display the Result 
            string strQuote;
            strQuote = "Membership Type: " + strSelectedMembershipType + Environment.NewLine
                + "Start Date: " + datStartDate.ToShortDateString() + Environment.NewLine
                + "End Date: " + datEndDate.ToShortDateString() + Environment.NewLine
                + "Membership Cost Per Month: " + decMonthlyMembershipPrice.ToString("C", new System.Globalization.CultureInfo("en-US")) + Environment.NewLine
                + "Subtotal: " + decMembershipPrice.ToString("C", new System.Globalization.CultureInfo("en-US")) + Environment.NewLine
                + "Additional Features: " + strFeatures + Environment.NewLine
                + "Total: " + decTotalPrice.ToString("C", new System.Globalization.CultureInfo("en-US"));

            lblPricingQuoteResult.Content = strQuote;


            //pass the info to variables
            strmembershiptype = strSelectedMembershipType;
            datstartdate = datStartDate;
            datenddate = datEndDate;
            decmembershipcostpermonth = decMonthlyMembershipPrice;
            decsubtotal = decMembershipPrice;
            stradditionalfeatures = strFeatures;
            dectotal = decTotalPrice;
            

        }
        //when click on "Sign up" change to screen to "MembershipSignup"
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if(lblPricingQuoteResult.Content.ToString() == "")
            {
                MessageBox.Show("Please get a quote before signing up.");
                return;
            }
            else
            {
                //create some info to send to the next window
                Member QuoteInfo = new Member(strmembershiptype, datstartdate, datenddate, decmembershipcostpermonth, decsubtotal, stradditionalfeatures, dectotal);

                //open next window and close this window
                MembershipSignup winMembershipSignup = new MembershipSignup();
                winMembershipSignup.Show();
                this.Close();
            }
        }

        

    }
}
