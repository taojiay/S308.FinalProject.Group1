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
    /// Interaction logic for MembershipInfoConfirmation.xaml
    /// </summary>
    public partial class MembershipInfoConfirmation : Window
    {
        public Member InfoFromPrevWindow { get; set; }

        public MembershipInfoConfirmation()
        {
            InitializeComponent();


            //clear all the output
            lblFirstNameInput.Content = "";
            lblLastNameInput.Content = "";
            lblPhoneInput.Content = "";
            lblEmailInput.Content = "";
            lblCreditCardTypeInput.Content = "";
            lblCreditCardNumberInput.Content = "";
            lblGenderInput.Content = "";
            lblAgeInput.Content = "";
            lblWeightInput.Content = "";
            lblPersonalFitnessGoalInput.Content = "";


            //default blank member for the default constructor
            InfoFromPrevWindow = new Member();
        }
        public MembershipInfoConfirmation(Member newMember)
        {
            InitializeComponent();

            //assigning the property from the member class that was passed into this overridden constructor
            InfoFromPrevWindow = newMember;

            //Display the summary
            ShowSummary(InfoFromPrevWindow);
        }

        //create method: show summary of membership confirmation
        public void ShowSummary(Member summary)
        {
            lblFirstNameInput.Content = summary.FirstName;
            lblLastNameInput.Content = summary.LastName;
            lblPhoneInput.Content = summary.Phone;
            lblEmailInput.Content = summary.Email;
            lblCreditCardTypeInput.Content = summary.CreditCardType;
            lblCreditCardNumberInput.Content = summary.CreditCardNumber;
            lblGenderInput.Content = summary.Gender;
            lblAgeInput.Content = summary.Age.ToString();
            lblWeightInput.Content = summary.Weight.ToString();
            lblPersonalFitnessGoalInput.Content = summary.PersonalFitnessGoal;

        }



        //close this window and back to the membership sale window
        private void btnAddAnotherMember_Click(object sender, RoutedEventArgs e)
        {
            MembershipSales winSales = new MembershipSales();
            winSales.Show();
            this.Close();
        }

        //back to main menu
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }
    }
}
