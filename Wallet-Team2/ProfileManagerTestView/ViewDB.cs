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

        private void ViewDB_Load(object sender, EventArgs e)
        {

            //Make an empty database, and add a new user with an address to it.
            mUserDB = new UserDB();
            List<Address> lAddrList = new List<Address>();
            List<Address> lAddrList2 = new List<Address>();
            List<Address> lAddrList3 = new List<Address>();
            lAddrList.Add(new Address("123","oak st","manhattan","KS","66502",""));
            lAddrList2.Add(new Address("6969","oak st","manhattan","KS","66502",""));
            lAddrList2.Add(new Address("234","oak st","manhattan","KS","66502",""));
            lAddrList3.Add(new Address("345","oak st","manhattan","KS","66502","838"));
            User lUser = new User("username1", "name1", "email1@email.com", "pass1", "recovery question one?", "recovery answer one!", "696-9696", lAddrList);
            User lUser2= new User("username2", "name2", "email2@email.com", "pass2", "recovery question two?", "recovery answer two!", "696-9696", lAddrList2);
            User lUser3= new User("username3", "name3", "email3@email.com", "pass3", "recovery question three?", "recovery answer three!", "696-9696", lAddrList3);
            mUserDB.AddUser(lUser);
            mUserDB.AddUser(lUser2);
            mUserDB.AddUser(lUser3);

            //Now that we have a database, load it into the listboxes.
            LoadDisplay();
        }

       private void LoadDisplay()
        {
           uxUserBox.Items.Clear();
           uxCurUserInfoBox.Items.Clear();
           uxCurUserAddrBox.Items.Clear();
           uxLoggedIn.Text = "LoggedIn: ";
           uxLoggedOut.Text = "LoggedOut: ";
           foreach(User lUser in mUserDB.mUsers)
           {
               uxUserBox.Items.Add(lUser.mUserName);
               if (lUser.mLoggedIn)
               {
                   uxLoggedIn.Text += lUser.mName + ",";
               }
               else
               {
                   uxLoggedOut.Text += lUser.mName + ",";
               }
           }

           
        }

        private void UpdateDisplay()
        {
            //get current User selected(listbox 'items' are strings:
            User lUser = mUserDB.findUser(uxUserBox.SelectedItem.ToString());

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
            foreach (Address lAddress in lUser.mAddresses)
            {
                uxCurUserAddrBox.Items.Add(lAddress.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = uxSaveDB.ShowDialog();
            if(result == DialogResult.OK)
            {
                mUserDB.Save(uxSaveDB.FileName);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            EditAddress lEditAddressForm = new EditAddress();
            lEditAddressForm.mUser = mUserDB.findUser(uxUserBox.SelectedItem.ToString());
            lEditAddressForm.loadDisplay();
            lEditAddressForm.ShowDialog();
            LoadDisplay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser lAddUserForm = new AddUser();
            lAddUserForm.mViewDBForm = this;
            lAddUserForm.setMode("add");
            lAddUserForm.ShowDialog();
            LoadDisplay();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddUser lAddUserForm = new AddUser();
            lAddUserForm.mViewDBForm = this;
            lAddUserForm.setMode("edit", mUserDB.findUser(uxUserBox.SelectedItem.ToString()));
            lAddUserForm.ShowDialog();
            LoadDisplay();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            User lUser = mUserDB.findUser(uxUserBox.SelectedItem.ToString());
            lUser.mLoggedIn = (Prompt.ShowDialog("Enter password for " + lUser.mUserName, "Enter password") == lUser.mPassword);
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
