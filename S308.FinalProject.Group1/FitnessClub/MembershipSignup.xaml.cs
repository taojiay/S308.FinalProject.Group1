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
using System.IO;
using Newtonsoft.Json;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MembershipSignup.xaml
    /// </summary>
    public partial class MembershipSignup : Window
    {
        public Member InfoFromPrevWindow { get; set; }

        List<Member> memberList;
        Member MemberSummary;


        public MembershipSignup()
        {
            InitializeComponent();


            //default blank member info for the default constructor
            InfoFromPrevWindow = new Member();
        }

        public MembershipSignup(Member QuoteInfo)
        {
            InitializeComponent();

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

            //instantiate a list to hold members
            memberList = new List<Member>();

            //call the method to import current members' information and display in the datagrid
            ImportMemberData();

            //assigning the property from the member class that was passed into this overridden constructor
            InfoFromPrevWindow = QuoteInfo;


        }

        private void ImportMemberData()
        {
            string strFilePath = @"..\..\..\Data\Member.json";

            try
            {
                //use System.IO.File to read the entire data file
                string jsonData = File.ReadAllText(strFilePath);

                //serialize the json data to a list of customers
                memberList = JsonConvert.DeserializeObject<List<Member>>(jsonData);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }

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

        private bool addMember(string firstname, string lastname, string phone, string email, object selectedcardtype, string cardnumber, object selectedgender, string age, string weight, object selectedgoal)
        {
            //Define variable
            Member newMember;

            //trim inputs
            firstname = firstname.Trim();
            lastname = lastname.Trim();
            phone = phone.Trim();
            email = email.Trim();
            cardnumber = cardnumber.Trim();
            age = age.Trim();
            weight = weight.Trim();

            //validation:
            //check if first name, last name, credit card type, credit card number, phone, email, gender are selected, if not error message display
            if (firstname == "")
            {
                MessageBox.Show("First name is required.");
                return false;
            }

            if (lastname == "")
            {
                MessageBox.Show("Last name is required.");
                return false;
            }


            if (phone == "")
            {
                MessageBox.Show("Phone is required.");
                return false;
            }

            if (email == "")
            {
                MessageBox.Show("Email is required.");
                return false;
            }


            if (selectedcardtype == null)
            {
                MessageBox.Show("Credit card type is required.");
                return false;
            }

            if (cardnumber == "")
            {
                MessageBox.Show("Credit card number is required.");
                return false;
            }



            if (selectedgender == null)
            {
                MessageBox.Show("Please select an appropriate gender input.");
                return false;
            }

            //validate phone
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

            //validate email
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

            //validate credit card number and type

            string strSelectedCardType = ((ComboBoxItem)selectedcardtype).Content.ToString();

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
            string strCardTypeInput = strSelectedCardType;
            string strCardNum = cardnumber;
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

                if (strCardType != strCardTypeInput)
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

            //validate age
            byte bytAge;
            if (age != "")
            {
                if (!byte.TryParse(age, out bytAge))
                {
                    MessageBox.Show("Please enter a valid age.");
                    return false;
                }
                else
                {
                    bytAge = Convert.ToByte(age);
                }
            }
            else
                bytAge = 0;

            //validate weight, capture inputs
            short shtWeight;
            if (weight != "")
            {
                if (!Int16.TryParse(weight, out shtWeight))
                {
                    MessageBox.Show("Please enter a valid weight.");
                    return false;
                }
                else
                {
                    shtWeight = Convert.ToInt16(weight);
                }
            }
            else
                shtWeight = 0;

            //capture personal fitness goal
            string strGoal;
            if (selectedgoal != null)
            {
                strGoal = ((ComboBoxItem)selectedgoal).Content.ToString();
            }
            else
                strGoal = "";

            //Validate that a member is not purchaseing a membership that overlaps the timeframe of a previously purchased membership
            //1. previous start < now end
            //2. previous start < now start and previous end > now end
            //3. previous end > now start
            DateTime datPreviousStart, datPreviousEnd;
            foreach (var m in memberList)
                if (m.FirstName == firstname && m.LastName == lastname && m.Phone == phone)
                {
                    datPreviousStart = m.StartDate;
                    datPreviousEnd = m.EndDate;
                    if(datPreviousEnd < InfoFromPrevWindow.EndDate)
                    {
                        MessageBox.Show("The chosen time is overlapping with the previously purchased membership.");
                        return false;
                    }
                    else if(datPreviousStart < InfoFromPrevWindow.StartDate && datPreviousEnd > InfoFromPrevWindow.EndDate)
                    {
                        MessageBox.Show("The chosen time is overlapping with the previously purchased membership.");
                        return false;
                    }
                    else if(datPreviousEnd > InfoFromPrevWindow.StartDate)
                    {
                        MessageBox.Show("The chosen time is overlapping with the previously purchased membership.");
                        return false;
                    }
                }


                    newMember = new Member(InfoFromPrevWindow, firstname, lastname, phone, email, ((ComboBoxItem)selectedcardtype).Content.ToString(), cardnumber, ((ComboBoxItem)selectedgender).Content.ToString(), bytAge, shtWeight, strGoal);

            //Add the new member object to the list
            memberList.Add(newMember);

            //pass on information
            MemberSummary = new Member(firstname, lastname, phone, email, ((ComboBoxItem)selectedcardtype).Content.ToString(), cardnumber, ((ComboBoxItem)selectedgender).Content.ToString(), bytAge, shtWeight, strGoal);


            //Return true (as status) to the calling code
            return true;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {


            //declare variables
            bool bolStatus;
            string strFilePath = @"..\..\..\Data\Member.json";

            //call AddMember method, and passing all needed inputs
            //the method will return a bool type as the status of the Add operation
            bolStatus = addMember(txtFirstName.Text, txtLastName.Text, txtPhone.Text, txtEmail.Text, cboCreditCardType.SelectedItem, txtCreditCardNumber.Text, cboGender.SelectedItem, txtAge.Text, txtWeight.Text, cboPersonalFitnessGoal.SelectedItem);

            if (bolStatus)
            {
                try
                {
                    //serialize the new list of member to json format
                    string jsonData = JsonConvert.SerializeObject(memberList);

                    //use System.IO.File to write over the file with the json data
                    System.IO.File.WriteAllText(strFilePath, jsonData);

                    MessageBox.Show("Member is added!");


                    //close window
                    MembershipInfoConfirmation winConfirmation = new MembershipInfoConfirmation(MemberSummary);
                    winConfirmation.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in export process: " + ex.Message);
                }
            }

        }







        public static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }


        //when click "search" (optional)
        private void btnSearchforPrefill_Click(object sender, RoutedEventArgs e)
        {
            //validation:
            //check if first name, last name, credit card are filled
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name is required for search function.");
                return;
            }

            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name is required for search function.");
                return;
            }


            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone is required for search function.");
                return;
            }

            //validate phone
            long lngPhone;

            if (!Int64.TryParse(txtPhone.Text.Trim(), out lngPhone))
            {
                MessageBox.Show("Phone number contain only numbers.");
                return;
            }

            if (txtPhone.Text.Trim().Length != 10)
            {
                MessageBox.Show("Phone number must contain 10 digits.");
                return;
            }


            //declare variables to capture inputs
            string strFirstName, strLastName, strPhone;
            strFirstName = txtFirstName.Text.Trim();
            strLastName = txtLastName.Text.Trim();
            strPhone = txtPhone.Text.Trim();


            //run query for the membership information json file and capture values
            foreach (var m in memberList)
                if (m.FirstName == strFirstName && m.LastName == strLastName && m.Phone == strPhone)
                {
                    //fill the result in the form
                    txtEmail.Text = m.Email;

                    foreach (ComboBoxItem i in cboCreditCardType.Items)
                    {
                        if(i.Content.ToString() == m.CreditCardType)
                            cboCreditCardType.SelectedItem = i;
                    }



                    txtCreditCardNumber.Text = m.CreditCardNumber;


                    foreach (ComboBoxItem i in cboGender.Items)
                    {
                        if (i.Content.ToString() == m.Gender)
                            cboGender.SelectedItem = i;
                    }
                   


                    if (m.Age != 0)
                        txtAge.Text = m.Age.ToString();
                    if (m.Weight != 0)
                        txtWeight.Text = m.Weight.ToString();

                    if (m.PersonalFitnessGoal != "")
                    {
                        foreach (ComboBoxItem i in cboPersonalFitnessGoal.Items)
                        {
                            if (i.Content.ToString() == m.PersonalFitnessGoal)
                                cboPersonalFitnessGoal.SelectedItem = i;
                        }
                    }

                    if (txtEmail.Text.Trim() == "")
                    {
                        MessageBox.Show("No record found.");
                    }

                }

        }


    }
}
