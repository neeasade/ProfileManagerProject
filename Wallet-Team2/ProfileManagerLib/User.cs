using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{

    //feeling iffy about out states a little
    enum state{
        LoggedIn,
        LoggedOut,
        Success
    }


    public class User
    {
        String mName;
        String mUserName;
        List<Address> mAddresses;
        String mEmail;
        String mPassword;
        String mRecoveryQuestion;
        String mRecoveryAnswer;
        String mPhoneNumber;

        /// <summary>
        /// Add an existing address object to the list of addresses.
        /// If there are 5 existing addresses for this user, the last on the list will be removed.
        /// </summary>
        /// <param name="aAddressToAdd"></param>
        public void AddAddress(Address aAddressToAdd)
        {
            if (mAddresses.Count == 5)
            {
                mAddresses.RemoveAt(4);
                mAddresses.Add(aAddressToAdd);
            }
            else
            {
                mAddresses.Add(aAddressToAdd);
            }
        }


        /// <summary>
        /// Make a new User
        /// </summary>
       public User(string aUserName, string aName, string aEmail, string aPassword, string aRecoveryQuestion, string aRecoveryAnswer, string aPhoneNumber, List<Address> aAddressList)
        {
             mName = aName;
             mUserName = aUserName;
             List<Address> mAddress = aAddressList;
             mEmail = aEmail;
             mPassword = aPassword;
             mRecoveryQuestion = aRecoveryQuestion;
             mRecoveryAnswer = aRecoveryAnswer;
             mPhoneNumber = aPhoneNumber;
        }

        /// <summary>
        /// Change a users password, based on entered current password and a desired new password
        /// </summary>
        /// <param name="aCurPassword"></param>
        /// <param name="aNewPassword"></param>
        /// <returns>False if aCurPassword is not the currend password</returns>
        public bool ChangePassword(String aCurPassword, string aNewPassword)
        {
            mPassword = (aCurPassword == mPassword ? aNewPassword : mPassword);
            return (aCurPassword == mPassword);
        }

        /// <summary>
        /// Either return the current password if recovery answer is correct, or return and empty string.
        /// </summary>
        /// <returns></returns>
        public string RecoverPassword(string aRecoveryAnswer)
        {
            return (aRecoveryAnswer == mRecoveryAnswer ? mPassword : "");
        }

        /// <summary>
        /// To be used by the UserDB for storage purposes. DO NOT USE
        /// Format will be 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string lDelim = "|";
            string toReturn;
            toReturn = mUserName + lDelim +
                       mName + lDelim +
                       mEmail + lDelim +
                       mPassword + lDelim +
                       mRecoveryQuestion + lDelim +
                       mRecoveryAnswer + lDelim +
                       mPhoneNumber + "\n";

            for(int i=0; i<mAddresses.Count;i++)
            {
                toReturn += mAddresses[i].toDBString();
                toReturn += lDelim;
            }
            return toReturn + "\n";
        }
    }
}
