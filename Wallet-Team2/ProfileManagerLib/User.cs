using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public class User
    {
        public string mName;
        public string mUserName;
        public List<Address> mAddresses;
        public string mEmail;
        public string mPassword;
        public string mRecoveryQuestion;
        public string mRecoveryAnswer;
        public string mPhoneNumber;
        public bool mLoggedIn;

        /// <summary>
        /// Clear all addresses that this user has.
        /// </summary>
        public void ClearAddresses()
        {
            mAddresses = new List<Address>();
        }

        /// <summary>
        /// Attempt to log this user in with a password. mLoggedIn will be set with the result of this.
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
        public void AddAddress(string aStreetNumber,string aStreetName, string aCity, string aState, string aZip, string aZipExt)
        {
            if (mAddresses.Count == 5)
            {
                mAddresses.RemoveAt(4);
                mAddresses.Add(new Address( aStreetNumber, aStreetName,  aCity,  aState,  aZip,  aZipExt));
            }
            else
            {
                mAddresses.Add(new Address( aStreetNumber, aStreetName,  aCity,  aState,  aZip,  aZipExt));
            }
        }

        /// <summary>
        /// Add a new address object to the list of addresses.
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
        /// Return the address object that represents the preferred shipping address.
        /// </summary>
        /// <returns></returns>
        public Address GetPreferredShippingAddress()
        {
            return mAddresses[0];
        }

        /// <summary>
        /// Set a preferred shipping address by calling it's ToString() method to pass as a parameter here.
        /// Returns false if the address was not found in this user.
        /// </summary>
        public bool SetPreferredShippingAddressByString(string aAddressString)
        {
            if(mAddresses[0].ToString() == aAddressString)
            {
                return true;
            }

            for (int i=0; i < mAddresses.Count; i++)
            {
                if(aAddressString == mAddresses[i].ToString())
                {
                    //swap them
                    Address tmp = mAddresses[0];
                    mAddresses[0] = mAddresses[i];
                    mAddresses[i] = tmp;
                    return true;
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
       }

        /// <summary>
        /// Change a users password, based on entered current password and a desired new password
        /// </summary>
        /// <param name="aCurPassword"></param>
        /// <param name="aNewPassword"></param>
        /// <returns>False if aCurPassword is not the current password</returns>
        public bool ChangePassword(String aCurPassword, string aNewPassword)
        {
            mPassword = (aCurPassword == mPassword ? aNewPassword : mPassword);
            return (aNewPassword == mPassword);
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
            return mUserName + "\n" +
                   mEmail + "\n" +
                   mName + "\n" +
                   mPhoneNumber + "\n" +
                   "Preferred shipping address:\n" +
                   (mAddresses.Count > 0 ? mAddresses[0].ToString() : "none found");
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
