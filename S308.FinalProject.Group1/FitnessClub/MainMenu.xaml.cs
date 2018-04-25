//Team 1: Jiayi Tao, Yuheng Cao, Zheng Huang
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }
         //link membership sales button with MembershipSales and close main menu
        private void btnMembershipSales_Click(object sender, RoutedEventArgs e)
        {
            MembershipSales winSales = new MembershipSales();
            winSales.Show();
            this.Close();
        }

        //link pricing management button with Pricing_Management and close main menu
        private void btnPricingManagement_Click(object sender, RoutedEventArgs e)
        {
            Pricing_Management winPricing = new Pricing_Management();
            winPricing.Show();
            this.Close();
        }

        //link member information button with Member_Information and close main menu
        private void btnMemberInformation_Click(object sender, RoutedEventArgs e)
        {
            Member_Information winInfo = new Member_Information();
            winInfo.Show();
            this.Close();
        }

        //create exit button function
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
