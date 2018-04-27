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
   
        public Pricing_Management()
        {
            InitializeComponent();
            //clear all the inputs

            cbxType.SelectedIndex = -1;
            txtPrice.Text = "";
            ckbAvailable.IsChecked = false;
            cbxFeature.SelectedIndex = -1;
            txtFeaturePrice.Text = "";

        }

        //when click on membership "submit" button:
        //validation:
        
        private void btnMembershipSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\..\Data\MembershipPrice.json";
            decimal price;
            //check if type and price the fields are filled or selected
            if (cbxType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }

            //instantiate a new price from the input and add it to the list
            MembershipPrice membershippriceNew = new MembershipPrice(txtPrice.Text.Trim(), price);
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
            MessageBox.Show("Membership Type has been changed to:" + txtPrice.Text);
            //check if the price can be parsed
            //????

        }

        //confirmation message

        //when click on features "submit" button: 
        //validation:
      
         private void btnFeaturesSubmit_Click(object sender, RoutedEventArgs e)
        {  
            //check if type and price the fields are filled or selected
            if (cbxFeature.SelectedIndex == -1)
            {
                MessageBox.Show("Please confirm that you don't want any additional features.");
                return;
            }
            //check if the price can be parsed
   

        }

        //confirmation message

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
