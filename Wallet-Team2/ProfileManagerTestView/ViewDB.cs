﻿using System;
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
           foreach(User lUser in mUserDB.mUsers)
           {
               uxUserBox.Items.Add(lUser.mUserName);
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
    }
}
