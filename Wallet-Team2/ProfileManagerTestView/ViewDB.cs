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
        public UserDB mUserDB;

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

        private void ViewDB_Load(object sender, EventArgs e)
        {
            //Make a 'dummy' database with 3 users.
            mUserDB = new UserDB(8);

            //Now that we have a database, load it into the listboxes.
            LoadDisplay();
        }

       private void LoadDisplay()
        {
           //clear the list boxes
           uxUserBox.Items.Clear();
           uxCurUserInfoBox.Items.Clear();
           uxCurUserAddrBox.Items.Clear();

           //populate the logged in logged out statuses
           uxLoggedIn.Text = "LoggedIn: ";
           uxLoggedOut.Text = "LoggedOut: ";
           foreach(User lUser in mUserDB.mUsers)
           {
               uxUserBox.Items.Add(lUser.mEmail);
               if (lUser.mLoggedIn)
               {
                   uxLoggedIn.Text += lUser.mName + ",";
               }
               else
               {
                   uxLoggedOut.Text += lUser.mName + ",";
               }
           }

           //select the first item and then call update display(if there are any items to select)
           if(uxUserBox.Items.Count > 0)
           {
               uxUserBox.SelectedIndex = 0;
               UpdateDisplay();
           }
        }

        private void UpdateDisplay()
        {
            //get current User selected(listbox 'items' are strings:
            User lUser = mUserDB.FindUser(uxUserBox.SelectedItem.ToString());

            //populate the middle box with that Users info(clear it first):
            uxCurUserInfoBox.Items.Clear();
            uxCurUserInfoBox.Items.Add(lUser.mUserName);
            uxCurUserInfoBox.Items.Add(lUser.mName);
            uxCurUserInfoBox.Items.Add(lUser.mEmail);
            uxCurUserInfoBox.Items.Add(lUser.mPassword);
            uxCurUserInfoBox.Items.Add(lUser.mRecoveryQuestion);
            uxCurUserInfoBox.Items.Add(lUser.mRecoveryAnswer);
            uxCurUserInfoBox.Items.Add(lUser.mPhoneNumber);

            //populate the right box with that Users Adress information:
            uxCurUserAddrBox.Items.Clear();
            foreach (Address lAddress in lUser.getAddresses())
            {
                uxCurUserAddrBox.Items.Add(lAddress.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UserSelected())
            {
                DialogResult result = uxSaveDB.ShowDialog();
                if (result == DialogResult.OK)
                {
                    mUserDB.Save(uxSaveDB.FileName);
                }
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
            DialogResult result = uxOpenDB.ShowDialog();
            if(result == DialogResult.OK)
            {
                mUserDB = new UserDB(uxOpenDB.FileName);
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
            EditAddress lEditAddressForm = new EditAddress();
            lEditAddressForm.mUser = mUserDB.FindUser(uxUserBox.SelectedItem.ToString());
            lEditAddressForm.loadDisplay();
            lEditAddressForm.ShowDialog();
            UpdateDisplay();
        }

        /// <summary>
        /// Add a new User to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser lAddUserForm = new AddUser();
            lAddUserForm.mViewDBForm = this;
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
            if (UserSelected())
            {
                AddUser lAddUserForm = new AddUser();
                lAddUserForm.mViewDBForm = this;
                lAddUserForm.setMode("edit", mUserDB.FindUser(uxUserBox.SelectedItem.ToString()));
                lAddUserForm.ShowDialog();
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Log a user in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (UserSelected())
            {
                User lUser = mUserDB.FindUser(uxUserBox.SelectedItem.ToString());
                lUser.mLoggedIn = (Prompt.ShowDialog("Enter password for " + lUser.mUserName, "Enter password") == lUser.mPassword);
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
                MessageBox.Show(mUserDB.FindUser(uxUserBox.SelectedItem.ToString()).ToString());
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
                User lUser = mUserDB.FindUser(uxUserBox.SelectedItem.ToString());
                string response = lUser.RecoverPassword(Prompt.ShowDialog( lUser.mRecoveryQuestion,"Password recovery"));
                if(response == string.Empty)
                {
                    MessageBox.Show("That was the wrong recovery question answer.");
                }
                else
                {
                    MessageBox.Show("Your password is :" + response);
                }
            }
        }

        /// <summary>
        /// Recover a Username by entering an email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string response = mUserDB.FindUserName(Prompt.ShowDialog( "Enter a Users email to find the Username associated with it:","Find Username"));
            MessageBox.Show("The username for that email is " + response);
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
