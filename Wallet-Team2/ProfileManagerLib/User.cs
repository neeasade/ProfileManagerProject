using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    internal class User
    {
        private string mName;
        private string mUserName;
        private List<Address> mAddresses;
        private string mEmail;
        private string mPassword;
        private string mRecoveryQuestion;
        private string mRecoveryAnswer;
        private string mPhoneNumber;
        private bool mLoggedIn;

        /// <summary>
        /// User Logged in status - set with Login() and Logout() functions.
        /// </summary>
        public bool LoggedIn
        {
            get { return mLoggedIn;  }
        }

        /// <summary>
        /// User Full Name - User must be logged in to get or set value.
        /// </summary>
        public string Name
        {
            get { return (mLoggedIn ? mName : "Please login to get name."); }
            set { if (mLoggedIn) {mName = value;} }           
        }

        /// <summary>
        /// User Phone Number - User must be logged in to get or set value.
        /// </summary>
        public string PhoneNumber
        {
            get { return (mLoggedIn ? mPhoneNumber : "Please login to get phone number."); }
            set { if (mLoggedIn) {mPhoneNumber = value;} }
        }

        /// <summary>
        /// User Username - User must be logged in to set.
        /// </summary>
        public string UserName
        {
            get { return mUserName; }
            set { if (mLoggedIn) { mUserName = value; } }
        }

        /// <summary>
        /// User Email - User must be logged in to set.
        /// </summary>
        public string Email
        {
            get { return mEmail; }
            set { if (mLoggedIn) { mEmail = value; } }
        }

        /// <summary>
        /// User password - must be logged in to get value.
        /// Set this value with the ChangePassword() function()
        /// </summary>
        public string Password
        {
            get {
                return (mLoggedIn ? mPassword : "Please login to get password.");
            }
        }

        /// <summary>
        /// User Recovery question used to recover password - User must be logged in to set.
        /// </summary>
        public string RecoveryQuestion
        {
            get { return mRecoveryQuestion; }
            set { if (mLoggedIn) { mRecoveryQuestion = value; } }
        }

        /// <summary>
        /// User answer to their recovery question - must be logged in to set.
        /// </summary>
        public string RecoveryAnswer
        {
            get { return mRecoveryAnswer; }
            set { if (mLoggedIn) { mRecoveryAnswer = value;} }
        }
 
        /// <summary>
        /// A Method to return the current list of current Addresses owned by the user, with the preferred Address in the first index.
        /// Returns null if the user is not logged in.
        /// </summary>
        /// <returns></returns>
        public List<Address> getAddresses()
        {
            return (!mLoggedIn || mAddresses.Count == 0 ? null : mAddresses);
        }

        /// <summary>
        /// Log this user out.
        /// </summary>
        public void Logout()
        {
            mLoggedIn = false;
        }

        /// <summary>
        /// Clear all addresses that this user has.
        /// </summary>
        public void ClearAddresses()
        {
            if (mLoggedIn)
            {
                mAddresses = new List<Address>();
            }
        }

        /// <summary>
        /// Attempt to log this user in with a password. Returns true if successful.
        /// </summary>
        /// <param name="aPassword"></param>
        /// <returns>True if the logon is successful, false if otherwise</returns>
        public bool LogIn(string aPassword)
        {
            mLoggedIn = (mPassword == aPassword);
            return mLoggedIn;
        }

        /// <summary>
        /// Add an existing address object to the list of addresses.
        /// If there are 5 existing addresses for this user, the last on the list will be removed.
        /// </summary>
        /// <param name="aAddressToAdd"></param>
        public void AddAddress(string aStreetNumber,string aStreetName, string aCity, string aState, string aZip)
        {
            if (mLoggedIn)
            {
                if (mAddresses.Count == 5)
                {
                    mAddresses.RemoveAt(4);
                }
                mAddresses.Add(new Address(aStreetNumber, aStreetName, aCity, aState, aZip));
            }
        }

        /// <summary>
        /// Remove and existing user address based on a matching Address objects ToString() method.
        /// Returns false if the Address is not found, or the user is not logged in.
        /// </summary>
        /// <param name="aAddressToString"></param>
        public bool RemoveAddress(string aAddressToString)
        {
            if (mLoggedIn)
            {
                for (int i = 0; i < mAddresses.Count; i++)
                {
                    if (aAddressToString == mAddresses[i].ToString())
                    {
                        mAddresses.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Return the address object that represents the preferred shipping address.
        /// If the User is not logged in, returns null.
        /// </summary>
        /// <returns></returns>
        public Address GetPreferredShippingAddress()
        {
            return (mLoggedIn ? mAddresses[0] : null);
        }

        /// <summary>
        /// Set a preferred shipping address by calling it's ToString() method to pass as a parameter here.
        /// Returns false if the address was not found in this user, or if the user is not logged in.
        /// </summary>
        public bool SetPreferredShippingAddressByString(string aAddressString)
        {
            if (mLoggedIn)
            {
                if (mAddresses[0].ToString() == aAddressString)
                {
                    return true;
                }

                if (mAddresses.Count > 1)
                {
                    for (int i = 1; i < mAddresses.Count; i++)
                    {
                        if (aAddressString == mAddresses[i].ToString())
                        {
                            //swap them
                            Address tmp = mAddresses[0];
                            mAddresses[0] = mAddresses[i];
                            mAddresses[i] = tmp;
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Make a new User. If there is no address information for a user, simply pass in "new List<Address>()" to the aAddressList parameter.
        /// </summary>
       public User(string aUserName, string aName, string aEmail, string aPassword, string aRecoveryQuestion, string aRecoveryAnswer, string aPhoneNumber, List<Address> aAddressList)
       {
             mName = aName;
             mUserName = aUserName;
             mEmail = aEmail;
             mPassword = aPassword;
             mRecoveryQuestion = aRecoveryQuestion;
             mRecoveryAnswer = aRecoveryAnswer;
             mPhoneNumber = aPhoneNumber;
             mAddresses = aAddressList;
             mLoggedIn = false;
       }

        /// <summary>
        /// Change a users password, based on entered current password and a desired new password.
        /// </summary>
        /// <param name="aCurPassword"></param>
        /// <param name="aNewPassword"></param>
        /// <returns>False if aCurPassword is not the current password, or user is not logged in.</returns>
        public bool ChangePassword(String aCurPassword, string aNewPassword)
        {
            if (mLoggedIn)
            {
                mPassword = (aCurPassword == mPassword ? aNewPassword : mPassword);
                return (aNewPassword == mPassword);
            }
            return false;
        }

        /// <summary>
        /// Either return the current password if recovery answer is correct, or return null.
        /// </summary>
        public string RecoverPassword(string aRecoveryAnswer)
        {
            return (aRecoveryAnswer == mRecoveryAnswer ? mPassword : null);
        }

        /// <summary>
        /// Returns some information about the user that would be nice to have on a summary page of some sort.
        /// example:
        /// CoolUserName42
        /// JohnDoe@hotmail.com
        /// John Doe
        /// 555-555-5657
        /// Preferred shipping address:
        /// 123 Oak ln Manhattan KS, 66502 834
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName + "\n" +
                   Email + "\n" +
                   Name + "\n" +
                   PhoneNumber + "\n" +
                   (LoggedIn ? "Preferred shipping address:\n" + (mAddresses.Count > 0 ? mAddresses[0].ToString() : "none found") : "");
        }

        /// <summary>
        /// Returns a string with User information delimited for db storage purposes.
        /// </summary>
        /// <returns></returns>
        public string ToDBString()
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
            //trim off the last lDelim:
            toReturn = toReturn.Substring(0, toReturn.Length - 2);
            return toReturn + "\n";
        }
    }
}
