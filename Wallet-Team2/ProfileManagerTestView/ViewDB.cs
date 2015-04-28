using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProfileManagerLib;

namespace ProfileManagerTestView
{
    public partial class ViewDB : Form
    {
        private ProfileController mProfileController;
        private string mSelectedUserEmail;

        public ViewDB()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check if a user is selected to do operations on.
        /// </summary>
        /// <returns></returns>
        private bool UserSelected()
        {
            if (uxUserBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user first");
                return false;
            }
            return true;
        }

        private bool SelectedUserLoggedIn()
        {
            if(UserSelected())
            {
                if (mProfileController.UserLoggedIn(mSelectedUserEmail))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("That user is not logged in");
                    return false;
                }
            }
            return false; //No user is selected
        }

        private void ViewDB_Load(object sender, EventArgs e)
        {
            //Make a 'dummy' database with 8 users.
            mProfileController = new ProfileController(8);

            //Now that we have a database, load it into the listboxes.
            LoadDisplay();
        }

        /// <summary>
        /// clear the boxes and refresh as if a fresh load, and populate the loggedIn and loggedOut labels.
        /// </summary>
       private void LoadDisplay()
        {
           //clear the list boxes
           uxUserBox.Items.Clear();
           uxCurUserInfoBox.Items.Clear();
           uxCurUserAddrBox.Items.Clear();

           //populate the logged in logged out statuses
           uxLoggedIn.Text = "LoggedIn: ";
           uxLoggedOut.Text = "LoggedOut: ";
           for (int i = 0; i < mProfileController.NumberOfUsers(); i++ )
           {
               string lUserEmail = mProfileController.FindEmail(i);
               uxUserBox.Items.Add(lUserEmail);
               if(mProfileController.UserLoggedIn(lUserEmail))
               {
                   uxLoggedIn.Text += mProfileController.GetUserProperty(lUserEmail,UserProperty.Username) + ",";
               }
               else
               {
                   uxLoggedOut.Text += mProfileController.GetUserProperty(lUserEmail,UserProperty.Username) + ",";
               }
           }

           //select the first item(will trigger an indexChanged event to call UpdateDisplay())
           if(uxUserBox.Items.Count > 0)
           {
               uxUserBox.SelectedIndex = 0;
           }
        }

        private void UpdateDisplay()
        {
            //get current User selected(listbox 'items' are strings):
            mSelectedUserEmail = uxUserBox.SelectedItem.ToString();

            //populate the middle box with that Users info(clear it first):
            uxCurUserInfoBox.Items.Clear();
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.Username));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.Name));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.Email));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.Password));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.RecoveryQuestion));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.RecoveryAnswer));
            uxCurUserInfoBox.Items.Add(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.PhoneNumber));
            
            //populate the right box with that Users Adress information:
            uxCurUserAddrBox.Items.Clear();

            //loop through the 5 possible addresses a user can have(1 based):
            for (int i = 1; i < 6;i++)
            {
                //Attempt to get current Addreses ToString() - if it is null, there is not address at that index.
                string lCurAddressString = mProfileController.GetUserAddressValue(mSelectedUserEmail,i,AddressProperty.ToString);

                //if the first index is null, we know there are no addresses for this user
                if(lCurAddressString == null && i == 1)
                {
                    uxCurUserAddrBox.Items.Add("None found");
                    break;
                }
                if(lCurAddressString != null)
                {
                    uxCurUserAddrBox.Items.Add(lCurAddressString);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( uxSaveDB.ShowDialog() == DialogResult.OK)
            {
                mProfileController.SaveDatabase(uxSaveDB.FileName);
            }
        }


        /// <summary>
        /// A new user has been selected, display that users info.
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Load a database from a textfile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //open up a file dialog and then load that database:
            if(uxOpenDB.ShowDialog() == DialogResult.OK)
            {
                mProfileController = new ProfileController(uxOpenDB.FileName);
                LoadDisplay();
            }
        }

        /// <summary>
        /// Pass the EditAddress form the current user, and open it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(SelectedUserLoggedIn())
            {
                EditAddress lEditAddressForm = new EditAddress();
                lEditAddressForm.mProfileController = mProfileController;
                lEditAddressForm.mUserEmail = mSelectedUserEmail;
                lEditAddressForm.loadDisplay();
                lEditAddressForm.ShowDialog();
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Add a new User to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser lAddUserForm = new AddUser();
            lAddUserForm.mProfileController = mProfileController;
            lAddUserForm.setMode("add");
            lAddUserForm.ShowDialog();
            LoadDisplay();
        }


        /// <summary>
        /// Edit the information of an existing user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if(SelectedUserLoggedIn())
            {
                AddUser lAddUserForm = new AddUser();
                lAddUserForm.mProfileController = mProfileController;
                lAddUserForm.setMode("edit", mSelectedUserEmail); 
                lAddUserForm.ShowDialog();
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Log a user in or out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (UserSelected())
            {
                if(mProfileController.UserLoggedIn(mSelectedUserEmail))
                {
                    mProfileController.LogUserOut(mSelectedUserEmail);
                }
                else
                {
                    string lCurUsername = mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.Username);
                    string lPassAttempt = (Prompt.ShowDialog("Enter password for " + lCurUsername, "Enter password"));
                    if (!mProfileController.LogUserIn(mSelectedUserEmail, lPassAttempt))
                    {
                        MessageBox.Show("Incorrect password!");
                    }
                }
                LoadDisplay();
            }
        }

        /// <summary>
        /// Display a User's ToString() method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if(UserSelected())
            {
                MessageBox.Show(mProfileController.GetUserProperty(mSelectedUserEmail,UserProperty.ToString));
            }
        }

        /// <summary>
        /// Recover a password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if(UserSelected())
            {
                string lRecoveryQuestion = mProfileController.GetUserProperty(mSelectedUserEmail, UserProperty.RecoveryQuestion);
                string lRecoveryAttempt = Prompt.ShowDialog(lRecoveryQuestion, "Password Recovery");
                string response = mProfileController.RecoverPassword(mSelectedUserEmail, lRecoveryAttempt);
                MessageBox.Show(response == null ? "That was the wrong recovery question answer." : "Your password is :"+response);
            }
        }

        /// <summary>
        /// Recover a Username by entering an email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string response = mProfileController.GetUserProperty(mSelectedUserEmail, UserProperty.Username);
            MessageBox.Show("The username for that email is " + response);
        }

        /// <summary>
        /// Delete a User.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            mProfileController.DeleteUser(mSelectedUserEmail);
            LoadDisplay();
        }
    }

    public static class Prompt
    {
        //stackoverflow code :^)
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 200, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}
