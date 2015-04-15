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
        public ViewDB mViewDBForm;
        public string mMode;

        public void setMode(string aMode, User aUser = null)
        {
            mMode = aMode;
            if (mMode == "edit" && aUser != null)
            {
                uxName.Text = aUser.Name;
                uxEmail.Text = aUser.Email;
                uxUsername.Text = aUser.UserName;
                uxRecoveryQuestion.Text = aUser.RecoveryQuestion;
                uxRecoveryAnswer.Text = aUser.RecoveryAnswer;
                uxPhoneNumber.Text = aUser.PhoneNumber;
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
                User lUser = mViewDBForm.mUserDB.FindUser(uxEmail.Text);
                lUser.Name = uxName.Text;
                lUser.UserName = uxUsername.Text;
                lUser.PhoneNumber = uxPhoneNumber.Text;
                lUser.RecoveryAnswer = uxRecoveryAnswer.Text;
                lUser.RecoveryQuestion = uxRecoveryQuestion.Text;
                this.Close();
            }
            else if (mMode == "add")
            {
                //adding in a new user, check if confirm password
                if(uxPassword.Text == uxConfirmPassword.Text)
                {
                    User lNewUser = new User(uxUsername.Text, uxName.Text, uxEmail.Text,uxPassword.Text,uxRecoveryQuestion.Text,uxRecoveryAnswer.Text,uxPhoneNumber.Text,new List<Address>() );
                    mViewDBForm.mUserDB.AddUser(lNewUser);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password not confirmed!");
                }
            }
        }

        private void uxPasswordChange_Click(object sender, EventArgs e)
        {
            PasswordChange lPassChangeForm = new PasswordChange();
            lPassChangeForm.mUser = mViewDBForm.mUserDB.FindUser(uxEmail.Text);
            lPassChangeForm.ShowDialog();
        }
    }
}
