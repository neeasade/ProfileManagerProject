using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public enum UserProperty
    {
        Name,
        PhoneNumber,
        Username,
        Email,
        Password,
        RecoveryQuestion,
        RecoveryAnswer,
        ToString
    };

    public enum AddressProperty
    {
        StreetNumber,
        StreetName,
        City,
        State,
        Zip,
        ToString
    };


    public interface IProfileController
    {
         string GetUserAddressValue(string aEmail, int aAddressIndex, AddressProperty aAddressProperty);
         bool SetUserAddressValue(string aEmail, int aAddressIndex, AddressProperty aAddressProperty, string aAddressPropertyValue);
         string GetUserProperty(string aEmail, UserProperty aUserProperty);
         bool SetUserProperty(string aEmail, UserProperty aUserProperty, string aUserValue);
         int NumberOfUsers();
         int GetUserAddressCount(string aEmail);
         void SetUserPreferredAddressByString(string aEmail, string aAddressToString);
         bool ChangeUserPassword(string aEmail, string aCurPassword, string aNewPassword);
         bool UserLoggedIn(string aEmail);
         bool LogUserIn(string aEmail, string aPassword) ;
         void LogUserOut(string aEmail);
         string FindEmail(string aUsername);
         string FindEmail(int aUserIndex);
         bool AddUser(string aUsername, string aName, string aEmail, string aPassword, string aRecoveryQuestion, string aRecoveryAnswer, string aPhoneNumber);
         void AddAddressToUser(string aEmail, string aStreetNumber, string aStreetName, string aCity, string aState, string aZip);
         string GetUserPreferredAddress(string aEmail);
         void ClearUserAddresses(string aEmail);
         void DeleteUser(string aEmail);
         string RecoverPassword(string aEmail, string aRecoveryAnswer);
         bool SaveDatabase(string aDBlocation);
    }

    public class ProfileController : IProfileController
    {
        private UserDB mUserDB;


        //---------------------------------------- Constructors ---------------------------------------

        /// <summary>
        /// Make a new  ProfileController with an empty User database
        /// </summary>
        public ProfileController()
        {
            mUserDB = new UserDB();
        }

        /// <summary>
        /// Make a new ProfileControllers who's database gets populated with an amount of 'dummy' users, determined by the parameter.
        /// </summary>
        public ProfileController(int aFakeUserCount)
        {
            mUserDB = new UserDB(aFakeUserCount);
        }

        /// <summary>
        /// Make a new ProfileController who's data is determined by loading in a text file, determined by parameter.
        /// </summary>
        public ProfileController(string aDBLocation)
        {
            mUserDB = new UserDB(aDBLocation);
        }

        //---------------------------------------- Get and set User and Address Values -----------------------------------
        /// <summary>
        /// Get a user address value from a users address by index(1-5)
        /// Returns null if Address Index doesn't exist.
        /// </summary>
        /// <param name="aEmail">Email to identify user.</param>
        /// <param name="aAddressIndex">Index of Address belonging to user to get AddressProperty from.</param>
        /// <param name="aAddressProperty">The part of the address to get.</param>
        /// <returns></returns>
        public string GetUserAddressValue(string aEmail, int aAddressIndex, AddressProperty aAddressProperty)
        {
            User lUser = mUserDB.FindUser(aEmail);
            if(lUser.getAddresses() == null || lUser.getAddresses().Count < aAddressIndex)
            {
                return null;
            }

            Address lAddress = lUser.getAddresses()[--aAddressIndex];

            switch(aAddressProperty)
            {
                case AddressProperty.StreetNumber: return lAddress.StreetNumber;
                case AddressProperty.StreetName: return lAddress.Street;
                case AddressProperty.City: return lAddress.City;
                case AddressProperty.State: return lAddress.State;
                case AddressProperty.Zip: return lAddress.Zip;
                case AddressProperty.ToString: return lAddress.ToString();
            }

            //shouldn't get here:
            return null;
        }

        /// <summary>
        /// Set an Address value on an address that a User has, identified by Address index(1-5)
        /// returns false if a nonexistent index was specified or the user was logged out.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <param name="aAddressIndex"></param>
        /// <param name="aAddressProperty"></param>
        /// <returns></returns>
        public bool SetUserAddressValue(string aEmail, int aAddressIndex, AddressProperty aAddressProperty, string aAddressPropertyValue)
        {
            User lUser = mUserDB.FindUser(aEmail);
            if(lUser.getAddresses().Count < aAddressIndex || !lUser.LoggedIn)
            {
                return false;
            }

            Address lAddress = lUser.getAddresses()[--aAddressIndex];

            switch(aAddressProperty)
            {
                case AddressProperty.StreetNumber:
                    lAddress.StreetNumber = aAddressPropertyValue; break;
                case AddressProperty.StreetName: 
                    lAddress.Street = aAddressPropertyValue; break;
                case AddressProperty.City: 
                    lAddress.City = aAddressPropertyValue; break;
                case AddressProperty.State: 
                    lAddress.State = aAddressPropertyValue; break;
                case AddressProperty.Zip: 
                    lAddress.Zip = aAddressPropertyValue; break;
            }
            return true;
        }

        /// <summary>
        /// Get a value from a User who's email matches the aEmail parameter. The second parameter to this function is an enum that determines what thing to get.
        /// Please be aware if the user is not logged in many of these are considered off limits.
        /// </summary>
        /// <param name="aEmail">Email to identify the user to get values from.</param>
        /// <param name="aUserValue">Enum value representing what value to get.</param>
        public string GetUserProperty(string aEmail, UserProperty aUserProperty)
        {
            User lUser = mUserDB.FindUser(aEmail);
            switch(aUserProperty)
            {
                case UserProperty.Name:
                    return lUser.Name;
                case UserProperty.PhoneNumber:
                    return lUser.PhoneNumber;
                case UserProperty.Username:
                    return lUser.UserName;
                case UserProperty.Email:
                    return lUser.Email;
                case UserProperty.Password:
                    return lUser.Password;
                case UserProperty.RecoveryQuestion:
                    return lUser.RecoveryQuestion;
                case UserProperty.RecoveryAnswer:
                    return lUser.RecoveryAnswer;
                case UserProperty.ToString:
                    return lUser.ToString();
            }
            return "No matching case for value found.";
        }

        /// <summary>
        /// Set a user value. User must be logged in to change values.
        /// The Password cannot be changed from here, see ChangeUserPassword().
        /// </summary>
        /// <returns></returns>
        public bool SetUserProperty(string aEmail, UserProperty aUserProperty, string aUserValue)
        {
            //We shouldn't be able to change anything if the user isn't logged in.
            if (!UserLoggedIn(aEmail))
            {
                return false;
            }

            User lUser = mUserDB.FindUser(aEmail);

            switch (aUserProperty)
            {
                case UserProperty.Name:
                    lUser.Name = aUserValue; break;
                case UserProperty.PhoneNumber:
                    lUser.PhoneNumber = aUserValue; break;
                case UserProperty.Username:
                    lUser.UserName = aUserValue; break;
                case UserProperty.Email:
                    lUser.Email = aUserValue; break;
                case UserProperty.RecoveryQuestion:
                    lUser.RecoveryQuestion = aUserValue; break;
                case UserProperty.RecoveryAnswer:
                    lUser.RecoveryAnswer = aUserValue; break;
            }
            //If we get here, User was logged in and we set the value.
            return true;
        }
        //---------------------------------------- UserDB Wrappers -----------------------------------

        /// <summary>
        /// This is not a wrapp
        /// </summary>
        public int NumberOfUsers()
        {
            return mUserDB.NumberOfUsers;
        }

        /// <summary>
        /// Get the number of addresses that a user has.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public int GetUserAddressCount(string aEmail)
        {
            return mUserDB.FindUser(aEmail).getAddresses().Count;
        }

        /// <summary>
        /// Set a users preferred address by that addresses ToString() value.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <param name="aAddressToString"></param>
        public void SetUserPreferredAddressByString(string aEmail, string aAddressToString)
        {
            mUserDB.FindUser(aEmail).SetPreferredShippingAddressByString(aAddressToString);
        }

        /// <summary>
        /// Attempt to change a password, with a current password and new desired password.
        /// </summary>
        public bool ChangeUserPassword(string aEmail, string aCurPassword, string aNewPassword)
        {
            return mUserDB.FindUser(aEmail).ChangePassword(aCurPassword, aNewPassword);
        }

        /// <summary>
        /// Determine if a User is logged in.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public bool UserLoggedIn(string aEmail)
        {
            return mUserDB.FindUser(aEmail).LoggedIn;
        }
   
        /// <summary>
        /// Log a user in with an email and password. Returns result of logon.
        /// To log a user in with their username, user the FindEmail() function to pass as the first parameter.
        /// </summary>
        public bool LogUserIn(string aEmail, string aPassword) 
        {
            return mUserDB.FindUser(aEmail).LogIn(aPassword);
        }

        /// <summary>
        /// Log a user with a matching email out.
        /// </summary>
        /// <param name="aEmail"></param>
        public void LogUserOut(string aEmail)
        {
            mUserDB.FindUser(aEmail).Logout();
        }

        /// <summary>
        /// Get an email associated with an username.
        /// </summary>
        public string FindEmail(string aUsername)
        {
            return mUserDB.FindEmail(aUsername);
        }

        /// <summary>
        /// Get the email of a user at an index.
        /// </summary>
        /// <param name="aUserIndex"></param>
        /// <returns></returns>
        public string FindEmail(int aUserIndex)
        {
            return mUserDB.FindEmailAtIndex(aUserIndex);
        }

        /// <summary>
        /// Make a new user to add to the database.
        /// To add Addresses after the fact, use AddAddressToUser().
        /// This will return false if a User has an Email or Username conflict.
        /// </summary>
        public bool AddUser(string aUsername, string aName, string aEmail, string aPassword, string aRecoveryQuestion, string aRecoveryAnswer, string aPhoneNumber)
        {
                return mUserDB.AddUser(new User(aUsername, aName, aEmail,aPassword,aRecoveryQuestion,aRecoveryAnswer, aPhoneNumber, new List<Address>()));
        }

        /// <summary>
        /// Add an address to a user. User is limited to 5 addresses, last one will be removed if we exceed that.
        /// User is identified with first parameter, User email.
        /// </summary>
        public void AddAddressToUser(string aEmail, string aStreetNumber, string aStreetName, string aCity, string aState, string aZip)
        {
            mUserDB.FindUser(aEmail).AddAddress(aStreetNumber, aStreetName, aCity, aState, aZip);
        }

        /// <summary>
        /// Get User's preferred shipping address string.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public string GetUserPreferredAddress(string aEmail)
        {
            return mUserDB.FindUser(aEmail).GetPreferredShippingAddress().ToString();
        }

        /// <summary>
        /// Clear all of a users addresses.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public void ClearUserAddresses(string aEmail)
        {
            mUserDB.FindUser(aEmail).ClearAddresses();
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public void DeleteUser(string aEmail)
        {
            mUserDB.DeleteUser(aEmail);
        }

        /// <summary>
        /// Attempt to recover a users password - returns null on failure.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <param name="aRecoveryAnswer"></param>
        /// <returns></returns>
        public string RecoverPassword(string aEmail, string aRecoveryAnswer)
        {
            return mUserDB.FindUser(aEmail).RecoverPassword(aRecoveryAnswer);
        }

        /// <summary>
        /// Save the database to a location.
        /// </summary>
        /// <returns></returns>
        public bool SaveDatabase(string aDBLocation)
        {
            return mUserDB.Save(aDBLocation);
        }
    }
}
