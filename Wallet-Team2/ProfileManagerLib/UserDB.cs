using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public class UserDB
    {

        //List of Users loaded in the DB
        private List<User> mUsers;

        /// <summary>
        /// The list of Users  
        /// </summary>
        public List<User> Users
        {
            get { return mUsers; }
        }

        /// <summary>
        /// Get a user who has the email in the parameter - Throws an exception if the user does not exist.
        /// </summary>
        /// <param name="aUserName"></param>
        /// <returns>Returns the matching user, or throws an exception if the user does not exist.</returns>
        public User FindUser(string aEmail)
        {
            for (int i=0; i< mUsers.Count; i++)
            {
                if(mUsers[i].Email == aEmail)
                {
                    return mUsers[i];
                }
            }
            throw new Exception("User Not Found");
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
        /// Add a new user to the database
        /// </summary>
        /// <param name="aNewUser"></param>
        public void AddUser(User aNewUser)
        {
            mUsers.Add(aNewUser);
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
                lAddresses.Add(new Address(i.ToString(), "Oak St", "Manhattan","KS","66502",i.ToString().PadLeft(3,'0')));
                lAddresses.Add(new Address(i.ToString(), "Acorn St", "Richmond","VA","69696",i.ToString().PadLeft(3,'0')));
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
                        lUserAddresses.Add(new Address(lAddrInfo[0], lAddrInfo[1], lAddrInfo[2], lAddrInfo[3], lAddrInfo[4], lAddrInfo[5]));
                    }
                }
                //add the new user to the User DB list
                mUsers.Add(new User(lUserInfo[0], lUserInfo[1], lUserInfo[2], lUserInfo[3], lUserInfo[4], lUserInfo[5], lUserInfo[6], lUserAddresses));
            }
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
                    if(uncypher)
                    {
                        result += (char)(lChar - 10);
                    }
                    else
                    {
                        result += (char)(lChar + 10);
                    }
                }
                toReturn.Add(result);
            }
            return toReturn;
        }
    }
}
