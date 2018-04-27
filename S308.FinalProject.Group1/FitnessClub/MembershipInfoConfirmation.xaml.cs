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


            //default blank member for the default constructor
            InfoFromPrevWindow = new Member();
        }

        public MembershipInfoConfirmation(Member MemberSummary)
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


            //assigning the property from the member class that was passed into this overridden constructor
            InfoFromPrevWindow = MemberSummary;


            //Display the summary
           ShowSummary();
        }

        //create method: show summary of membership confirmation

        public void ShowSummary()
        {
            lblFirstNameInput.Content = InfoFromPrevWindow.FirstName;
            lblLastNameInput.Content = InfoFromPrevWindow.LastName;
            lblPhoneInput.Content = InfoFromPrevWindow.Phone;
            lblEmailInput.Content = InfoFromPrevWindow.Email;
            lblCreditCardTypeInput.Content = InfoFromPrevWindow.CreditCardType;
            lblCreditCardNumberInput.Content = InfoFromPrevWindow.CreditCardNumber;
            lblGenderInput.Content = InfoFromPrevWindow.Gender;
            lblAgeInput.Content = InfoFromPrevWindow.Age.ToString();
            lblWeightInput.Content = InfoFromPrevWindow.Weight.ToString();
            lblPersonalFitnessGoalInput.Content = InfoFromPrevWindow.PersonalFitnessGoal;

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
