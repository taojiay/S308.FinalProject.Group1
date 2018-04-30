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
    /// Interaction logic for Pricing_Management.xaml
    /// </summary>
    public partial class Pricing_Management : Window
    {
        List<MembershipPrice> MembershipPriceIndex;
        List<FeaturesPrice> FeaturePriceIndex;

        public Pricing_Management()
        {
            InitializeComponent();

            //clear all the inputs
            cbxType.SelectedIndex = -1;
            txtPrice.Text = "";
            ckbAvailable.IsChecked = false;
            cbxFeature.SelectedIndex = -1;
            txtFeaturePrice.Text = "";

            //load the membership price list from  the json file
            MembershipPriceIndex = GetMembershipPriceDataFromFile();
            FeaturePriceIndex = GetFeaturesPriceDataFromFile();

        }


        //method: get membership price data from json file
        private List<MembershipPrice> GetMembershipPriceDataFromFile()
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
        private List<FeaturesPrice> GetFeaturesPriceDataFromFile()
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

        //when click on membership "submit" button:
        //validation:
        private void btnMembershipSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"../../../Data/MembershipPricing.json";

            decimal decPrice;
            bool bolAvailability;
            

            //check if type and price fields are filled or selected
            if (cbxType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }

            
            //check if price can be convert to decimal
                if (!Decimal.TryParse(txtPrice.Text.Trim(), out decPrice))
            {
                MessageBox.Show("Please enter a decimal number for Price.");
                return;
            }

            //capture input
            ComboBoxItem cbiSelectedMembershipType = (ComboBoxItem)cbxType.SelectedItem;
            string strSelectedMembershipType = cbiSelectedMembershipType.Content.ToString();

            decPrice = Convert.ToDecimal(txtPrice.Text.Trim());
            if (ckbAvailable.IsChecked == true)
                bolAvailability = true;
            else
                bolAvailability = false;

            //rewrite membership plan price
            foreach (var x in MembershipPriceIndex)
                if (x.MembershipType == strSelectedMembershipType)
                {
                    x.Price = decPrice;
                    x.Availability = bolAvailability;

                }


            try
            {
                //serialize the new list of customer to json format
                string jsonData = JsonConvert.SerializeObject(MembershipPriceIndex);

                //use System.IO.File to write over the file with the json data
                System.IO.File.WriteAllText(strFilePath, jsonData);

                MessageBox.Show("Price of membership plan " + cbiSelectedMembershipType.Content + " has been changed to: $" + txtPrice.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process: " + ex.Message);
            }
        }

        

        //when click on features "submit" button: 
        //validation:
      
         private void btnFeaturesSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\..\Data\AdditionalFeaturePricing.json";
            decimal decFeaturePrice;

  

            //check if type and price the fields are filled or selected

            if (cbxFeature.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an additional feature that you want to change price.");
                return;
            }
            if (!Decimal.TryParse(txtFeaturePrice.Text.Trim(), out decFeaturePrice))
            {
                MessageBox.Show("Please enter a decimal number for price.");
                return;
            }

            //capture inputs
            ComboBoxItem cbiSelectedFeatureType = (ComboBoxItem)cbxFeature.SelectedItem;
            string strSelectedFeatureType = cbiSelectedFeatureType.Content.ToString();

            decFeaturePrice = Convert.ToDecimal(txtFeaturePrice.Text.Trim());


            //rewrite feature price
            foreach (var y in FeaturePriceIndex)
                if (y.FeaturesType == strSelectedFeatureType)
                    y.Price = decFeaturePrice;


                    //import new feature price
                    try
            {
                //serialize the new feature price to json format
                string jsonData = JsonConvert.SerializeObject(FeaturePriceIndex);

                //use System.IO.File to write over the file with the json data
                System.IO.File.WriteAllText(strFilePath, jsonData);

                MessageBox.Show("Price of Feature " + cbiSelectedFeatureType.Content + " has been changed to: $" + txtFeaturePrice.Text);

            }
            
            //if there is error in export process
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process:" + ex.Message);
            }
  



        }

        

        //return to main menu method
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

    
    }
}
