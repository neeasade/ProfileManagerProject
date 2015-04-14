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
        public User mUser;
        
        public void loadDisplay()
        {
            //terrible code
            Address lAddress;
            List<Address> lUserAddresses = mUser.getAddresses();

            //load in the Address info for each address available from the User.
            if(lUserAddresses.Count >= 1)
            {
                uxPreferred1.Checked = true;
                lAddress = lUserAddresses[0];
                uxStreetNum1.Text = lAddress.mStreetNumber;
                uxStreetName1.Text = lAddress.mStreet;
                uxCityName1.Text = lAddress.mCity;
                uxState1.Text = lAddress.mState;
                uxZip1.Text = lAddress.mZip;
                uxZipExt1.Text = lAddress.mZipExt;
            }

            if(lUserAddresses.Count >= 2)
            {
                lAddress = lUserAddresses[1];
                uxStreetNum2.Text = lAddress.mStreetNumber;
                uxStreetName2.Text = lAddress.mStreet;
                uxCityName2.Text = lAddress.mCity;
                uxStateName2.Text = lAddress.mState;
                uxZip2.Text = lAddress.mZip;
                uxZipExt2.Text = lAddress.mZipExt;
            }

            if(lUserAddresses.Count >= 3)
            {
                lAddress = lUserAddresses[2];
                uxStreetNum3.Text = lAddress.mStreetNumber;
                uxStreetName3.Text = lAddress.mStreet;
                uxCityName3.Text = lAddress.mCity;
                uxStateName3.Text = lAddress.mState;
                uxZip3.Text = lAddress.mZip;
                uxZipExt3.Text = lAddress.mZipExt;
            }

            if(lUserAddresses.Count >= 4)
            {
                lAddress = lUserAddresses[3];
                uxStreetNum4.Text = lAddress.mStreetNumber;
                uxStreetName4.Text = lAddress.mStreet;
                uxCityName4.Text = lAddress.mCity;
                uxStateName4.Text = lAddress.mState;
                uxZip4.Text = lAddress.mZip;
                uxZipExt4.Text = lAddress.mZipExt;
            }

            if(lUserAddresses.Count >= 5)
            {
                lAddress = lUserAddresses[4];
                uxStreetNum5.Text = lAddress.mStreetNumber;
                uxStreetName5.Text = lAddress.mStreet;
                uxCityName5.Text = lAddress.mCity;
                uxStateName5.Text = lAddress.mState;
                uxZip5.Text = lAddress.mZip;
                uxZipExt5.Text = lAddress.mZipExt;
            }
        }

        public EditAddress()
        {
            InitializeComponent();
        }

        private void uxSave_Click(object sender, EventArgs e)
        {
            mUser.ClearAddresses();
            Address lAddress;

            //SHIELD YOUR EYES

            //check if all spaces are empty for each line, and if not, add them.
            if(uxStreetNum1.Text != "" && uxStreetName1.Text != "" && uxCityName1.Text != "" && uxState1.Text != "" && uxZip1.Text != "")
            {
                mUser.AddAddress(uxStreetNum1.Text, uxStreetName1.Text, uxCityName1.Text, uxState1.Text, uxZip1.Text, uxZipExt1.Text);
            }
            if(uxStreetNum2.Text != "" && uxStreetName2.Text != "" && uxCityName2.Text != "" && uxStateName2.Text != "" && uxZip2.Text != "")
            {
                lAddress = (new Address(uxStreetNum2.Text, uxStreetName2.Text, uxCityName2.Text, uxStateName2.Text, uxZip2.Text, uxZipExt2.Text));
                mUser.AddAddress(lAddress);
                if (uxPreferred2.Checked)
                    mUser.SetPreferredShippingAddressByString(lAddress.ToString());
            }
            if(uxStreetNum3.Text != "" && uxStreetName3.Text != "" && uxCityName3.Text != "" && uxStateName3.Text != "" && uxZip3.Text != "")
            {
                lAddress = (new Address(uxStreetNum3.Text, uxStreetName3.Text, uxCityName3.Text, uxStateName3.Text, uxZip3.Text, uxZipExt3.Text));
                mUser.AddAddress(lAddress);
                if (uxPreferred3.Checked)
                    mUser.SetPreferredShippingAddressByString(lAddress.ToString());
            }
            if(uxStreetNum4.Text != "" && uxStreetName4.Text != "" && uxCityName4.Text != "" && uxStateName4.Text != "" && uxZip4.Text != "")
            {
                lAddress = (new Address(uxStreetNum4.Text, uxStreetName4.Text, uxCityName4.Text, uxStateName4.Text, uxZip4.Text, uxZipExt4.Text));
                mUser.AddAddress(lAddress);
                if (uxPreferred4.Checked)
                    mUser.SetPreferredShippingAddressByString(lAddress.ToString());
            }
            if(uxStreetNum5.Text != "" && uxStreetName5.Text != "" && uxCityName5.Text != "" && uxStateName5.Text != "" && uxZip5.Text != "")
            {
                lAddress = (new Address(uxStreetNum5.Text, uxStreetName5.Text, uxCityName5.Text, uxStateName5.Text, uxZip5.Text, uxZipExt5.Text));
                mUser.AddAddress(lAddress);
                if (uxPreferred5.Checked)
                    mUser.SetPreferredShippingAddressByString(lAddress.ToString());
            }
            this.Close();
        }

        private void EditAddress_Load(object sender, EventArgs e)
        {

        }
    }
}
