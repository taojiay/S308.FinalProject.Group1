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
    /// Interaction logic for Pricing_Management.xaml
    /// </summary>
    public partial class Pricing_Management : Window
    {
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
            //check if type and price the fields are filled or selected
            if (cbxType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }
            //check if the price can be parsed

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
