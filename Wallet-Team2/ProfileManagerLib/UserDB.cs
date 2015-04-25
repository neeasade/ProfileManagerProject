using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    internal class UserDB
    {
        //List of Users loaded in the DB
        private List<User> mUsers;

        public int NumberOfUsers
        {
            get { return mUsers.Count; }
        }

        /// <summary>
        /// Get the email of a user by their index in the database. 
        /// </summary>
        /// <param name="aIndex"></param>
        public string FindEmailAtIndex(int aIndex)
        {
            return (mUsers.Count > aIndex ? mUsers[aIndex].Email : null);
        }

        /// <summary>
        /// Determine if an Email Conflicts with an existing User.
        /// </summary>
        /// <param name="aEmail"></param>
        public bool EmailConflicts(string aEmail)
        {
            return (mUsers.Where<User>(tmpUser => tmpUser.Email == aEmail).Count() == 0 ? false : true);
        }

        /// <summary>
        /// Determine if a Username conflicts with an existing user.
        /// </summary>
        /// <param name="aUsername"></param>
        /// <returns></returns>
        public bool UsernameConflicts(string aUsername)
        {
            return (mUsers.Where<User>(tmpUser => tmpUser.UserName == aUsername).Count() == 0 ? false : true);
        }

        /// <summary>
        /// Get a user who has the email in the parameter - Throws an exception if the user does not exist.
        /// </summary>
        /// <param name="aUserName"></param>
        /// <returns>Returns the matching user, or returns null if the User does not exist.</returns>
        public User FindUser(string aEmail)
        {
            //FirstOrDefault returns null if not User with that email is found.
            return mUsers.FirstOrDefault<User>(tmpUser => tmpUser.Email == aEmail);
        }

        /// <summary>
        /// Returns a string containing the Username associated with an Email in the UserDB, else returns 'not found'
        /// </summary>
        /// <param name="aEmail"></param>
        /// <returns></returns>
        public string FindUserName(string aEmail)
        {
            for (int i =0;i< mUsers.Count;i++)
            {
                if(mUsers[i].Email == aEmail)
                {
                    return mUsers[i].UserName;
                }
            }
            return "not found";
        }

        /// <summary>
        /// Find the Email associated with a Username
        /// </summary>
        /// <param name="aUsername"></param>
        /// <returns></returns>
        public string FindEmail(string aUsername)
        {
            for (int i =0;i< mUsers.Count;i++)
            {
                if(mUsers[i].UserName == aUsername)
                {
                    return mUsers[i].Email;
                }
            }
            return "not found";
        }

        /// <summary>
        /// Add a new user to the database. Fails if there is a conflicting Username or email.
        /// </summary>
        /// <param name="aNewUser"></param>
        public bool AddUser(User aNewUser)
        {
            if(!EmailConflicts(aNewUser.Email) && !UsernameConflicts(aNewUser.UserName))
            {
                mUsers.Add(aNewUser);
                return true;
            }
            return false;
        }

        /// <summary>
        /// initialize a new UserDB with no users.
        /// </summary>
        public UserDB()
        {
            mUsers = new List<User>();
        }

        /// <summary>
        /// initialize a new UserDB with a number of fake users, determined by parameter.
        /// </summary>
        /// <param name="aSeedNum"></param>
        public UserDB(int aSeedNum)
        {
            mUsers = new List<User>();
            for(int i=0; i<aSeedNum;i++)
            {
                List<Address> lAddresses = new List<Address>();
                lAddresses.Add(new Address(i.ToString(), "Oak St", "Manhattan","KS","66502"));
                lAddresses.Add(new Address(i.ToString(), "Acorn St", "Richmond","VA","69696"));
                User lUser = new User("Username" + i, "Full Name" + i, "Email" + i + "@botnet.com", "pass" + i, "Recovery question " + i + "?", "Recovery Answer " + i, "000-000-" + i.ToString().PadLeft(4, '0'), lAddresses);
                lUser.LogIn("pass" + i);
                mUsers.Add(lUser);
            }
        }

        /// <summary>
        /// Load in a new userDB based off of a text file.
        /// </summary>
        /// <param name="aUserDBLocation">The location of the text file to read in.</param>
        public UserDB(string aUserDBLocation)
        {
            mUsers = new List<User>();

            //populate the lists of users                               -ayy lmao-
            string[] lUserDB = cypher(File.ReadAllLines(aUserDBLocation).ToList<string>(), true).ToArray();

            for (int i=0; i< lUserDB.Length; i++)
            {
                string[] lUserInfo = lUserDB[i].Split('|');
                List<Address> lUserAddresses = new List<Address>();
                string[] lUserDBAddresses = lUserDB[++i].Split('|');

                //Do we have any addresses for this user?
                if(lUserDBAddresses.Length == 1 && lUserDBAddresses[0] == "")
                {
                    lUserAddresses = new List<Address>();
                }
                else
                {
                   //make the address list
                    for (int j=0; j < lUserDBAddresses.Length; j++)
                    {
                        string[] lAddrInfo = lUserDBAddresses[j].Split('+');
                        lUserAddresses.Add(new Address(lAddrInfo[0], lAddrInfo[1], lAddrInfo[2], lAddrInfo[3], lAddrInfo[4]));
                    }
                }
                //add the new user to the User DB list
                mUsers.Add(new User(lUserInfo[0], lUserInfo[1], lUserInfo[2], lUserInfo[3], lUserInfo[4], lUserInfo[5], lUserInfo[6], lUserAddresses));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aEmail"></param>
        public void DeleteUser(string aEmail)
        {
            mUsers.RemoveAt(mUsers.FindIndex(tmpUser=>tmpUser.Email == aEmail));
        }

        /// <summary>
        /// Save a currently loaded database.
        /// </summary>
        /// <param name="aSaveLocation">The location of the text file to save.</param>
        /// <returns>Returns true if the file write is successful</returns>
        public bool Save(string aSaveLocation)
        {
            List<string> lSaveContent = new List<string>();
            for (int i=0;i<mUsers.Count;i++)
            {
                string UserString = mUsers[i].ToDBString();
                string UserDBString = UserString.Split('\n')[0];
                string AddressDBString = UserString.Split('\n')[1];
                lSaveContent.Add(UserDBString);
                lSaveContent.Add(AddressDBString);
            }

            //return the success of writing the file.
            File.WriteAllLines(aSaveLocation, cypher(lSaveContent, false).ToArray());
            return File.Exists(aSaveLocation);
        }

        /// <summary>
        /// cool haxxor storing method
        /// </summary>
        /// <param name="aInput"></param>
        /// <param name="uncypher"></param>
        /// <returns></returns>
        private List<string> cypher(List<string> aInput, bool uncypher)
        {
            List<string> toReturn = new List<string>();
            foreach(string aLine in aInput)
            {
                string result = string.Empty;
                foreach (char lChar in aLine)
                {
                    result += (uncypher ? (char)(lChar - 10) : (char)(lChar + 10));
                }
                toReturn.Add(result);
            }
            return toReturn;
        }
    }
}
