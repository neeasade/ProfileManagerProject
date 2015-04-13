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

            //load in the Address info for each address available from the User.
            if(mUser.mAddresses.Count >= 1)
            {
                uxPreferred1.Checked = true;
                lAddress = mUser.mAddresses[0];
                uxStreetNum1.Text = lAddress.mStreetNumber;
                uxStreetName1.Text = lAddress.mStreet;
                uxCityName1.Text = lAddress.mCity;
                uxState1.Text = lAddress.mState;
                uxZip1.Text = lAddress.mZip;
                uxZipExt1.Text = lAddress.mZipExt;
            }

            if(mUser.mAddresses.Count >= 2)
            {
                lAddress = mUser.mAddresses[1];
                uxStreetNum2.Text = lAddress.mStreetNumber;
                uxStreetName2.Text = lAddress.mStreet;
                uxCityName2.Text = lAddress.mCity;
                uxStateName2.Text = lAddress.mState;
                uxZip2.Text = lAddress.mZip;
                uxZipExt2.Text = lAddress.mZipExt;
            }

            if(mUser.mAddresses.Count >= 3)
            {
                lAddress = mUser.mAddresses[2];
                uxStreetNum3.Text = lAddress.mStreetNumber;
                uxStreetName3.Text = lAddress.mStreet;
                uxCityName3.Text = lAddress.mCity;
                uxStateName3.Text = lAddress.mState;
                uxZip3.Text = lAddress.mZip;
                uxZipExt3.Text = lAddress.mZipExt;
            }

            if(mUser.mAddresses.Count >= 4)
            {
                lAddress = mUser.mAddresses[3];
                uxStreetNum4.Text = lAddress.mStreetNumber;
                uxStreetName4.Text = lAddress.mStreet;
                uxCityName4.Text = lAddress.mCity;
                uxStateName4.Text = lAddress.mState;
                uxZip4.Text = lAddress.mZip;
                uxZipExt4.Text = lAddress.mZipExt;
            }

            if(mUser.mAddresses.Count >= 5)
            {
                lAddress = mUser.mAddresses[4];
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
            //SHIELD YOUR EYES

            Address[] lNewAddresses = new Address[5]; //List to store the new address information
            bool[] isPopulated = new bool[5]; // array to check if an address line has been completely filled, to make sure a valid preferred is selected

            //check if all spaces are empty for each line, and if not, add them.
            if(uxStreetNum1.Text != "" && uxStreetName1.Text != "" && uxCityName1.Text != "" && uxState1.Text != "" && uxZip1.Text != "")
            {
                isPopulated[0] = true;
                lNewAddresses[0] = (new Address(uxStreetNum1.Text, uxStreetName1.Text, uxCityName1.Text, uxState1.Text, uxZip1.Text, uxZipExt1.Text));
            }
            if(uxStreetNum2.Text != "" && uxStreetName2.Text != "" && uxCityName2.Text != "" && uxStateName2.Text != "" && uxZip2.Text != "")
            {
                isPopulated[1] = true;
                lNewAddresses[1] = (new Address(uxStreetNum2.Text, uxStreetName2.Text, uxCityName2.Text, uxStateName2.Text, uxZip2.Text, uxZipExt2.Text));

            }
            if(uxStreetNum3.Text != "" && uxStreetName3.Text != "" && uxCityName3.Text != "" && uxStateName3.Text != "" && uxZip3.Text != "")
            {
                isPopulated[2] = true;
                lNewAddresses[2] = (new Address(uxStreetNum3.Text, uxStreetName3.Text, uxCityName3.Text, uxStateName3.Text, uxZip3.Text, uxZipExt3.Text));
            }
            if(uxStreetNum4.Text != "" && uxStreetName4.Text != "" && uxCityName4.Text != "" && uxStateName4.Text != "" && uxZip4.Text != "")
            {
                isPopulated[3] = true;
                lNewAddresses[3] = (new Address(uxStreetNum4.Text, uxStreetName4.Text, uxCityName4.Text, uxStateName4.Text, uxZip4.Text, uxZipExt4.Text));
            }
            if(uxStreetNum5.Text != "" && uxStreetName5.Text != "" && uxCityName5.Text != "" && uxStateName5.Text != "" && uxZip5.Text != "")
            {
                isPopulated[4] = true;
                lNewAddresses[4] = (new Address(uxStreetNum5.Text, uxStreetName5.Text, uxCityName5.Text, uxStateName5.Text, uxZip5.Text, uxZipExt5.Text));
            }

            //See which index was selected as preferred(0-4 indexing radio buttons)
            bool[] preferredIndex=new bool[5];
            preferredIndex[0] = uxPreferred1.Checked;
            preferredIndex[1] = uxPreferred2.Checked;
            preferredIndex[2] = uxPreferred3.Checked;
            preferredIndex[3] = uxPreferred4.Checked;
            preferredIndex[4] = uxPreferred5.Checked;

            //Find the selected preferred, and then make it first index of addresses
            //starting at 1, because if the first button is selected, it has already been made the preferred address.
            for(int i=1;i<5;i++)
            {
                if(isPopulated[i] && preferredIndex[i])
                {
                    Address tmp = lNewAddresses[i];
                    lNewAddresses[i] = lNewAddresses[0];
                    lNewAddresses[0] = tmp;
                }
            }

            //convert the array to a list of only populated addresses:
            List<Address> lEndUserAddresses = new List<Address>();
            for (int i=0; i < lNewAddresses.Length; i++)
            {
                if(lNewAddresses[i] != null)
                {
                    lEndUserAddresses.Add(lNewAddresses[i]);
                }
            }

            mUser.mAddresses = lEndUserAddresses;
            this.Close();
        }

        private void EditAddress_Load(object sender, EventArgs e)
        {

        }
    }
}
