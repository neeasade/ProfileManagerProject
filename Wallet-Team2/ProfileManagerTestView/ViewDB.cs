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
        UserDB mUserDB;

        public ViewDB()
        {
            InitializeComponent();
        }

        private void ViewDB_Load(object sender, EventArgs e)
        {
            //toggle the below bool if you would like to load an existing DB or make a new DB
            bool newDB = true;

            if(newDB)
            {
                //Make an empty database, and add a new user with an address to it.


            }
            else
            {
                //TODO: change the below from hard coded string to file dialog.
                string DBLocation = "";
                mUserDB = new UserDB(DBLocation);
            }
            //location of the text file to loadDB from.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string DBSaveLocation = "";
            mUserDB.Save(DBSaveLocation);
        }
    }
}
