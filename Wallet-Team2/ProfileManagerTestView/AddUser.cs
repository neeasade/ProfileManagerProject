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
    public partial class AddUser : Form
    {
        public ProfileController mProfileController;
        public string mSelectedUser;
        public string mMode;

        public void setMode(string aMode, string aUser = "")
        {
            mMode = aMode;
            if (mMode == "edit" && aUser != "")
            {
                mSelectedUser = aUser;
                uxName.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.Name);
                uxEmail.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.Email);
                uxUsername.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.Username);
                uxRecoveryQuestion.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.RecoveryQuestion);
                uxRecoveryAnswer.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.RecoveryAnswer);
                uxPhoneNumber.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.PhoneNumber);
                uxName.Text = mProfileController.GetUserProperty(mSelectedUser, UserProperty.Name);
                uxEmail.Enabled = false;
                uxPassword.Enabled = false;
                uxConfirmPassword.Enabled = false;
            }
            else
            {
                if(mMode == "add" )
                {
                    uxEmail.Enabled = true;
                    uxPassword.Enabled = true;
                    uxConfirmPassword.Enabled = true;
                    uxPasswordChange.Hide();
                }
            }
        }

        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }

        private void uxSave_Click(object sender, EventArgs e)
        {
            if (mMode == "edit")
            {
                //editing an existing user
                mProfileController.SetUserProperty(mSelectedUser,UserProperty.Name,uxName.Text);
                mProfileController.SetUserProperty(mSelectedUser,UserProperty.Username,uxUsername.Text);
                mProfileController.SetUserProperty(mSelectedUser,UserProperty.PhoneNumber,uxPhoneNumber.Text);
                mProfileController.SetUserProperty(mSelectedUser,UserProperty.RecoveryQuestion,uxRecoveryQuestion.Text);
                mProfileController.SetUserProperty(mSelectedUser,UserProperty.RecoveryAnswer,uxRecoveryAnswer.Text);

                this.Close();
            }
            else if (mMode == "add")
            {
                //adding in a new user, check if confirm password
                if(uxPassword.Text == uxConfirmPassword.Text)
                {
                    mProfileController.AddUser(uxUsername.Text, uxName.Text, uxEmail.Text, uxPassword.Text, uxRecoveryQuestion.Text, uxRecoveryAnswer.Text, uxPhoneNumber.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password not confirmed!");
                    return;
                }
            }
        }

        private void uxPasswordChange_Click(object sender, EventArgs e)
        {
            PasswordChange lPassChangeForm = new PasswordChange();
            lPassChangeForm.mProfileController = mProfileController;
            lPassChangeForm.mSelectedUser = mSelectedUser;
            lPassChangeForm.ShowDialog();
        }
    }
}
