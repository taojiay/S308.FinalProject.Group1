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
            ckbAvailable.IsChecked = true;
            cbxFeature.SelectedIndex = -1;
            txtFeaturePrice.Text = "";

        }
        
        //when click on membership "submit" button:
        //validation:
        
        private void btnMembershipSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\..\Data\MembershipPrice.json";
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

            //instantiate a new membership plan price from the input and add it to the list
            MembershipPrice membershippriceNew = new MembershipPrice(strSelectedMembershipType, decPrice, bolAvailability);
            MembershipPriceIndex.Add(membershippriceNew);

            //import new membership plan price
            try
            {
                //serialize the new membership plan price to json format
                string jsonData = JsonConvert.SerializeObject(MembershipPriceIndex);

                //use System.IO.File to write over the file with the json data
                System.IO.File.WriteAllText(strFilePath, jsonData);

                MessageBox.Show("New membership plan price has been changed.");

            }
           
            //if there is error in export process
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process:" + ex.Message);
            }
           
            //confirmation message
            MessageBox.Show("Price of membership plan "+ cbiSelectedMembershipType.Content + " has been changed to:" + txtPrice.Text);
            
            //check if the price can be parsed
            //????

        }

        //confirmation message

        //when click on features "submit" button: 
        //validation:
      
         private void btnFeaturesSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\..\Data\FeaturePrice.json";
            decimal decFeaturePrice;

            //check if type and price the fields are filled or selected
            //Is Feature mandatory????
            if (cbxFeature.SelectedIndex == -1)
            {
                MessageBox.Show("Please confirm that you don't want any additional features.");
                return;
            }
            if (!Decimal.TryParse(txtFeaturePrice.Text.Trim(), out decFeaturePrice))
            {
                MessageBox.Show("Please enter a decimal number for Price.");
                return;
            }

            //capture inputs
            ComboBoxItem cbiSelectedFeatureType = (ComboBoxItem)cbxFeature.SelectedItem;
            string strSelectedFeatureType = cbiSelectedFeatureType.Content.ToString();

            decFeaturePrice = Convert.ToDecimal(txtFeaturePrice.Text.Trim());

            //instantiate a new feature price from the input and add it to the list
            FeaturesPrice featurepriceNew = new FeaturesPrice(strSelectedFeatureType, decFeaturePrice);
            FeaturePriceIndex.Add(featurepriceNew);

            //import new feature price
            try
            {
                //serialize the new feature price to json format
                string jsonData = JsonConvert.SerializeObject(FeaturePriceIndex);

                //use System.IO.File to write over the file with the json data
                System.IO.File.WriteAllText(strFilePath, jsonData);

                MessageBox.Show("New feature price has been changed.");

            }
            
            //if there is error in export process
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process:" + ex.Message);
            }
           
            //confirmation message
            MessageBox.Show("Price of Feature "+cbiSelectedFeatureType.Content+" has been changed to:" + txtFeaturePrice.Text);
           
            //check if the price can be parsed
            //???


        }

        

        //create back to main meanu function, close current window

        //return to main menu method
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

    
    }
}
