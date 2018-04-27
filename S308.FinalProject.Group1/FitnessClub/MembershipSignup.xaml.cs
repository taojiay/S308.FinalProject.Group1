//logo image source: 123RF.com, google
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
    /// Interaction logic for MembershipSignup.xaml
    /// </summary>
    public partial class MembershipSignup : Window
    {
        List<Member> memberList;
        public MembershipSignup()
        {
            InitializeComponent();

            //instantiate a list to hold members
            memberList = new List<Member>();

            //clear all the inputs
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            cboCreditCardType.SelectedIndex = -1;
            txtCreditCardNumber.Text = "";
            cboGender.SelectedIndex = -1;
            txtAge.Text = "";
            txtWeight.Text = "";
            cboPersonalFitnessGoal.SelectedIndex = -1;

            //hide image
            imgCard.Visibility = Visibility.Hidden;
        }

        //create main menu button function: link with main meanu and close current file
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        //when click "clear": clear all the input
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            cboCreditCardType.SelectedIndex = -1;
            txtCreditCardNumber.Text = "";
            cboGender.SelectedIndex = -1;
            txtAge.Text = "";
            txtWeight.Text = "";
            cboPersonalFitnessGoal.SelectedIndex = -1;
            imgCard.Visibility = Visibility.Hidden;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {          

            //validation:
            //check if first name, last name, credit card type, credit card number, phone, email, gender are selected, if not error message display
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name is required.");
                return;
            }

            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name is required.");
                return;
            }


            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone is required.");
                return;
            }

            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Email is required.");
                return;
            }


            if (cboCreditCardType.SelectedIndex == -1)
            {
                MessageBox.Show("Credit card type is required.");
                return;
            }

            if (txtCreditCardNumber.Text.Trim() == "")
            {
                MessageBox.Show("Credit card number is required.");
                return;
            }



            if (cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appropriate gender input.");
                return;
            }

            //validate phone
            ValidatePhone(txtPhone.Text.Trim());

            //validate email
            ValidateEmail(txtEmail.Text.Trim());

            //validate credit card number and type
            ComboBoxItem cbiSelectedCardType = (ComboBoxItem)cboCreditCardType.SelectedItem;
            string strSelectedCardType = cbiSelectedCardType.Content.ToString();

            ValidateCreditCard(strSelectedCardType, txtCreditCardNumber.Text.Trim());

            //validate age
            byte bytAge;
            if (txtAge.Text.Trim() != "")
            {
                if (!byte.TryParse(txtAge.Text.Trim(), out bytAge))
                {
                    MessageBox.Show("Please enter a valid age.");
                    return;
                }
                else
                {
                    bytAge = Convert.ToByte(txtAge.Text.Trim());
                }
            }

            //validate weight, capture inputs
            short shtWeight;
            if (txtWeight.Text.Trim() !="")
            {
                if(!Int16.TryParse(txtWeight.Text.Trim(), out shtWeight))
                {
                    MessageBox.Show("Please enter a valid weight.");
                    return;
                }
                else
                {
                    shtWeight = Convert.ToInt16(txtWeight.Text.Trim());
                }
            }


            //declare variables and capture inputs (besides credit card type, age, weight)
            string strFirstName = txtFirstName.Text.Trim();
            string strLastName = txtLastName.Text.Trim();
            string strCreditCardNumber = txtCreditCardNumber.Text.Trim();
            string strPhone = txtPhone.Text.Trim();
            string strEmail = txtEmail.Text.Trim();

            ComboBoxItem cbiSelectedGender = (ComboBoxItem)cboGender.SelectedItem;
            string strGender = cbiSelectedGender.Content.ToString();

            string strPersonalFitnessGoal = "";
            if(cboPersonalFitnessGoal.SelectedIndex != -1)
            {
                ComboBoxItem cbiSelectedPersonalGoal = (ComboBoxItem)cboPersonalFitnessGoal.SelectedItem;
                strPersonalFitnessGoal = cbiSelectedPersonalGoal.Content.ToString();
            }

            //add new member to the list
            Member newMember;
            newMember = new Member(strFirstName, strLastName, strPhone, strEmail, strSelectedCardType, strCreditCardNumber,strGender, )

            //add to file
            bool bolStatus;
            string strFilePath = @"..\..\..\Data\Member.json";


            bolStatus = 

            if(bolStatus)
                {
                    try
                    {
                        //serialize the new list of member to json format
                        string jsonData = JsonConvert.SerializeObject(memberList);

                        //use System.IO.File to write over the file with the json data
                        System.IO.File.WriteAllText(strFilePath, jsonData);

                        MessageBox.Show("Member is added!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in export process: " + ex.Message);
                    }
                }
            

        }


        
        //create method: validate phone number (only 10 digits)
        public bool ValidatePhone(string phone)
        {
            long lngPhone;

            if (!Int64.TryParse(phone, out lngPhone))
            {
                MessageBox.Show("Phone number contain only numbers.");
                return false;
            }

            if (phone.Length != 10)
            {
                MessageBox.Show("Phone number must contain 10 digits.");
                return false;
            }

            
            return true;
        }


        //create method: validate email address
        //contain an "@" and a "."; 
        //at least 1 character before @; 
        //at least 1 character between @ and .
        //at least 2 characters after the period
        public bool ValidateEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address that contains @ and a period.");
                return false;
            }

            //(test if there is more than one @ or .)
            int intFirstAt, intFirstPeriod, intLastAt, intLastPeriod;
            intFirstAt = email.IndexOf("@");
            intFirstPeriod = email.IndexOf(".");
            intLastAt = email.LastIndexOf("@");
            intLastPeriod = email.LastIndexOf(".");

            if ((intFirstAt != intLastAt) || (intFirstPeriod != intLastPeriod))
            {
                MessageBox.Show("Please enter a valid email address with only one @ and one period.");
                return false;
            }

            string strUsername, strBeforePeriod, strAfterPeriod;
            strUsername = email.Substring(0, intFirstAt);
            strBeforePeriod = email.Substring((intFirstAt + 1), (intFirstPeriod - intFirstAt - 1));
            strAfterPeriod = email.Substring(intFirstPeriod + 1);

            if (strUsername.Length < 1 || strBeforePeriod.Length < 1 || strAfterPeriod.Length < 2)
            {
                MessageBox.Show("Please enter a valid email address.");
                return false;
            }

            return true;
        }


        //create method: validate credit card number and credit card type
        private bool ValidateCreditCard(string creditCardType, string creditCardNumber)
        {
            //1. Declare a variables
            //   - capture credit card number, and credit card type
            //   - counter for loop
            //   - check digit (to hold each digit while working with them)
            //   - check sum (to hold the sum of the digits once modified)
            //   - card type
            //2. Make sure the text entered is numeric
            //       a. message to user that says to enter only numbers
            //       b. show negative result
            //3. Make sure there are 13, 15, 16 digits entered
            //       a. message to the user about the number of digits
            //       b. show negative result
            //4. Determine the card type from the prefix and set the card type variable
            //5. Validate card number
            //       a. reverse all of the characters in the credit card number
            //       b. loop through the characters
            //           - if it is the first, third, fifth, etc digit add it to the check sum
            //           - if it is the second, fourth, sixth, etc digit double before adding to the check sum
            //                   - if after double the digit it is > 9 then add the two numbers before adding to the check sum
            //                   - 12 = 1 + 2 or x - 9
            //       c. if the result is divisible by 10 the card number is a valid number. Set the valid variable
            //6. Show the appropriate result
            //       'a. if valid
            //           - check if card type matches with the input
            //           - yes: return true
            //           - no: return false with error message
            //       b. else
            //           - return false with error message

            //1.
            string strCardTypeInput = creditCardType;
            string strCardNum = creditCardNumber;
            long lngOut;
            bool bolValid = false;
            int i;
            int intCheckDigit;
            int intCheckSum = 0;
            string strCardType;

            //2.
            imgCard.Visibility = Visibility.Hidden;
            if (!Int64.TryParse(strCardNum, out lngOut))
            {
                MessageBox.Show("Credit card numbers contain only numbers.");
                return false;
            }

            //3.
            if (strCardNum.Length != 13 && strCardNum.Length != 15 && strCardNum.Length != 16)
            {
                MessageBox.Show("Credit card numbers must contain 13, 15, or 16 digits.");
                return false;
            }

            //4.
            if (strCardNum.StartsWith("34") || strCardNum.StartsWith("37"))
                strCardType = "American Express";
            else if (strCardNum.StartsWith("6011"))
                strCardType = "Discover";
            else if (strCardNum.StartsWith("51") || strCardNum.StartsWith("52") || strCardNum.StartsWith("53") || strCardNum.StartsWith("54") || strCardNum.StartsWith("55"))
                strCardType = "MasterCard";
            else if (strCardNum.StartsWith("4"))
                strCardType = "VISA";
            else
                strCardType = "Unknown Card Type";

            //5.
            strCardNum = ReverseString(strCardNum);

            for (i = 0; i < strCardNum.Length; i++)
            {
                intCheckDigit = Convert.ToInt32(strCardNum.Substring(i, 1));

                if ((i + 1) % 2 == 0)
                {
                    intCheckDigit *= 2;

                    if (intCheckDigit > 9)
                    {
                        intCheckDigit -= 9;
                    }
                }

                intCheckSum += intCheckDigit;
            }

            if (intCheckSum % 10 == 0)
            {
                bolValid = true;
            }

            //6.
            if (bolValid)
            {
                switch (strCardType)
                {
                    case "American Express":
                        imgCard.Source = new BitmapImage(new Uri(@"/Images/american_express_logo.png", UriKind.Relative));
                        imgCard.Width = 22;
                        imgCard.Height = 22;
                        break;
                    case "Discover":
                        imgCard.Source = new BitmapImage(new Uri(@"/Images/discover_logo.png", UriKind.Relative));
                        imgCard.Width = 32;
                        imgCard.Height = 22;
                        break;
                    case "MasterCard":
                        imgCard.Source = new BitmapImage(new Uri(@"/Images/mastercard_logo.png", UriKind.Relative));
                        imgCard.Width = 37;
                        imgCard.Height = 22;
                        break;
                    case "VISA":
                        imgCard.Source = new BitmapImage(new Uri(@"/Images/visa_logo.png", UriKind.Relative));
                        imgCard.Width = 35;
                        imgCard.Height = 22;
                        break;
                }


                imgCard.Visibility = Visibility.Visible;

                if (strCardType == strCardTypeInput)
                    return true;
                else
                {
                    MessageBox.Show("The selected credit card type doesn't match with the credit card number provided.");
                    return false;
                }
               
            }
            else
            {
                MessageBox.Show("The credit card number is not valid.");
                return false;
            }
        }

        public static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }



        //when click "search" (optional)
        //validation:
        //check if first name, last name, credit card are filled
        //capture inputs
        //declare variables
        //run query for the membership information json file and capture values
        //fill the result in the form

        //when click "submit":


        //declare variables to capture inputs, trim
        //validation:
        //check if the phone number is 10 digits without other characters
        //check if the email has "@" and "." and with the correct format
        //check if the credit card is validate
        //check if the credit card matches the credit card type
        //check if the age is between 0 and 100
        //check if the weight is between 0 and 500

        //store the data into json file

        //link to MembershipInfo Confirmation 




    }
}
