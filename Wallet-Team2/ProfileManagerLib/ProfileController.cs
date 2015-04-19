using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    class ProfileController
    {
        public enum UserProperty
        {
            Name,
            PhoneNumber,
            UserName,
            Email,
            Password,
            RecoveryQuestion,
            RecoveryAnswer
        };

        public enum AddressProperty
        {
            StreetNumber,
            StreetName,
            City,
            State,
            Zip
        };

        UserDB mUserDB;

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
        public ProfileController(string aDBlocation)
        {
            mUserDB = new UserDB(aDBlocation);
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
                case UserProperty.UserName:
                    return lUser.UserName;
                case UserProperty.Email:
                    return lUser.Email;
                case UserProperty.Password:
                    return lUser.Password;
                case UserProperty.RecoveryQuestion:
                    return lUser.RecoveryQuestion;
                case UserProperty.RecoveryAnswer:
                    return lUser.RecoveryAnswer;
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
                case UserProperty.UserName:
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
        /// Get a Username associated with an email.
        /// </summary>
        public string FindEmail(string aUsername)
        {
            return mUserDB.FindEmail(aUsername);
        }

        /// <summary>
        /// Make a new user to add to the database.
        /// To add Addresses after the fact, use AddAddressToUser().
        /// This will return false if a User has an Email or Username conflict.
        /// </summary>
        public bool AddUser(string aUsername, string aName, string aEmail, string aPassword, string aRecoveryQuestion, string aRecoveryAnswer, string aPhoneNumber)
        {
            if(!mUserDB.DoesEmailConflict(aEmail) && !mUserDB.DoesUsernameConflict(aUsername))
            {
                mUserDB.AddUser(new User(aUsername, aName, aEmail,aPassword,aRecoveryQuestion,aRecoveryAnswer, aPhoneNumber, new List<Address>());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add an address to a user. User is limited to 5 addresses, last one will be removed if we exceed that.
        /// User is identified with first parameter, User email.
        /// </summary>
        public void AddAddressToUser(string aEmail, string aStreetNumber, string aStreetName, string aCity, string aState, string aZip)
        {
            mUserDB.FindUser(aEmail).AddAddress(aStreetNumber, aStreetName, aCity, aState, aZip, ""); 
        }

        /// <summary>
        /// Get User's preferred shipping address.
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public string GetUserPreferredAddress(string aEmail)
        {
            return mUserDB.FindUser(aEmail).GetPreferredShippingAddress().ToString();
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
        /// Save the database to a location.
        /// </summary>
        /// <returns></returns>
        public bool SaveDatabase(string aDBlocation)
        {
            return mUserDB.Save(aDBlocation);
        }
    }
}
