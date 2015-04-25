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
    public partial class EditAddress : Form
    {
        //public User mUser;
        public ProfileController mProfileController;
        public string mUserEmail;
        
        public void loadDisplay()
        {
            //decades pass 

            string lStreetNum = mProfileController.GetUserAddressValue(mUserEmail,1,AddressProperty.StreetNumber);
            //load in the Address info for each address available from the User.
            if( lStreetNum != null)
            {
                uxPreferred1.Checked = true;
                uxStreetNum1.Text = lStreetNum;
                uxStreetName1.Text = mProfileController.GetUserAddressValue(mUserEmail, 1, AddressProperty.StreetName);
                uxCityName1.Text = mProfileController.GetUserAddressValue(mUserEmail, 1, AddressProperty.City);
                uxState1.Text = mProfileController.GetUserAddressValue(mUserEmail, 1, AddressProperty.State);
                uxZip1.Text = mProfileController.GetUserAddressValue(mUserEmail, 1, AddressProperty.Zip);
            }

            lStreetNum = mProfileController.GetUserAddressValue(mUserEmail,2,AddressProperty.StreetNumber);
            if(lStreetNum != null)
            {
                uxStreetNum2.Text = lStreetNum;
                uxStreetName2.Text = mProfileController.GetUserAddressValue(mUserEmail, 2, AddressProperty.StreetName);
                uxCityName2.Text = mProfileController.GetUserAddressValue(mUserEmail, 2, AddressProperty.City);
                uxStateName2.Text = mProfileController.GetUserAddressValue(mUserEmail, 2, AddressProperty.State);
                uxZip2.Text = mProfileController.GetUserAddressValue(mUserEmail, 2, AddressProperty.Zip);
            }

            lStreetNum = mProfileController.GetUserAddressValue(mUserEmail,3,AddressProperty.StreetNumber);
            if(lStreetNum != null)
            {
                uxStreetNum3.Text = lStreetNum;
                uxStreetName3.Text = mProfileController.GetUserAddressValue(mUserEmail, 3, AddressProperty.StreetName);
                uxCityName3.Text = mProfileController.GetUserAddressValue(mUserEmail, 3, AddressProperty.City);
                uxStateName3.Text = mProfileController.GetUserAddressValue(mUserEmail, 3, AddressProperty.State);
                uxZip3.Text = mProfileController.GetUserAddressValue(mUserEmail, 3, AddressProperty.Zip);
            }

            lStreetNum = mProfileController.GetUserAddressValue(mUserEmail,4,AddressProperty.StreetNumber);
            if(lStreetNum != null)
            {
                uxStreetNum4.Text = lStreetNum;
                uxStreetName4.Text = mProfileController.GetUserAddressValue(mUserEmail, 4, AddressProperty.StreetName);
                uxCityName4.Text = mProfileController.GetUserAddressValue(mUserEmail, 4, AddressProperty.City);
                uxStateName4.Text = mProfileController.GetUserAddressValue(mUserEmail, 4, AddressProperty.State);
                uxZip4.Text = mProfileController.GetUserAddressValue(mUserEmail, 4, AddressProperty.Zip);
            }

            lStreetNum = mProfileController.GetUserAddressValue(mUserEmail,5,AddressProperty.StreetNumber);
            if(lStreetNum != null)
            {
                uxStreetNum5.Text = lStreetNum;
                uxStreetName5.Text = mProfileController.GetUserAddressValue(mUserEmail, 5, AddressProperty.StreetName);
                uxCityName5.Text = mProfileController.GetUserAddressValue(mUserEmail, 5, AddressProperty.City);
                uxStateName5.Text = mProfileController.GetUserAddressValue(mUserEmail, 5, AddressProperty.State);
                uxZip5.Text = mProfileController.GetUserAddressValue(mUserEmail, 5, AddressProperty.Zip);
            }
        }

        public EditAddress()
        {
            InitializeComponent();
        }

        private void uxSave_Click(object sender, EventArgs e)
        {
            //SHIELD YOUR EYES

            //Clear all users addresses
            mProfileController.ClearUserAddresses(mUserEmail);

            //check if all spaces are empty for each line, and if not, Add address to user:
            if(uxStreetNum1.Text != "" && uxStreetName1.Text != "" && uxCityName1.Text != "" && uxState1.Text != "" && uxZip1.Text != "")
            {
                mProfileController.AddAddressToUser(mUserEmail, uxStreetNum1.Text, uxStreetName1.Text, uxCityName1.Text, uxState1.Text, uxZip1.Text);
            }

            if(uxStreetNum2.Text != "" && uxStreetName2.Text != "" && uxCityName2.Text != "" && uxStateName2.Text != "" && uxZip2.Text != "")
            {
                mProfileController.AddAddressToUser(mUserEmail, uxStreetNum2.Text, uxStreetName2.Text, uxCityName2.Text, uxStateName2.Text, uxZip2.Text);
                if (uxPreferred2.Checked)
                    mProfileController.SetUserPreferredAddressByString(mUserEmail, mProfileController.GetUserAddressValue(mUserEmail, mProfileController.GetUserAddressCount(mUserEmail), AddressProperty.ToString));
            }

            if(uxStreetNum3.Text != "" && uxStreetName3.Text != "" && uxCityName3.Text != "" && uxStateName3.Text != "" && uxZip3.Text != "")
            {
                mProfileController.AddAddressToUser(mUserEmail, uxStreetNum3.Text, uxStreetName3.Text, uxCityName3.Text, uxStateName3.Text, uxZip3.Text);
                if (uxPreferred3.Checked)
                    mProfileController.SetUserPreferredAddressByString(mUserEmail, mProfileController.GetUserAddressValue(mUserEmail, mProfileController.GetUserAddressCount(mUserEmail), AddressProperty.ToString));
            }
            if(uxStreetNum4.Text != "" && uxStreetName4.Text != "" && uxCityName4.Text != "" && uxStateName4.Text != "" && uxZip4.Text != "")
            {
                mProfileController.AddAddressToUser(mUserEmail, uxStreetNum4.Text, uxStreetName4.Text, uxCityName4.Text, uxStateName4.Text, uxZip4.Text);
                if (uxPreferred4.Checked)
                    mProfileController.SetUserPreferredAddressByString(mUserEmail, mProfileController.GetUserAddressValue(mUserEmail, mProfileController.GetUserAddressCount(mUserEmail), AddressProperty.ToString));
            }
            if(uxStreetNum5.Text != "" && uxStreetName5.Text != "" && uxCityName5.Text != "" && uxStateName5.Text != "" && uxZip5.Text != "")
            {
                mProfileController.AddAddressToUser(mUserEmail, uxStreetNum5.Text, uxStreetName2.Text, uxCityName2.Text, uxStateName2.Text, uxZip2.Text);
                if (uxPreferred5.Checked)
                    mProfileController.SetUserPreferredAddressByString(mUserEmail, mProfileController.GetUserAddressValue(mUserEmail, mProfileController.GetUserAddressCount(mUserEmail), AddressProperty.ToString));
            }
            this.Close();
        }

        private void EditAddress_Load(object sender, EventArgs e)
        {

        }
    }
}
