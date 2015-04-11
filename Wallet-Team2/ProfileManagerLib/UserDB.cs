﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public class UserDB
    {
        string mDatabaseLocation;
        List<User> mUsers;

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="aNewUser"></param>
        public void AddUser(User aNewUser)
        {
            mUsers.Add(aNewUser);
        }

        public UserDB()
        {
            mUsers = new List<User>();
        }

        /// <summary>
        /// Load in a new userDB based off of a text file.
        /// </summary>
        /// <param name="aUserDBLocation">The location of the text file to read in.</param>
        public UserDB(string aUserDBLocation)
        {
            //warning this function is gross code.

            mUsers = new List<User>();

            //populate the lists of users 
            string[] lUserDB = File.ReadAllLines(aUserDBLocation);

            for (int i=0; i< lUserDB.Length; i++)
            {
                string[] lUserInfo = lUserDB[i].Split('|');
                List<Address> lUserAddresses = new List<Address>();
                string[] lUserDBAddresses = lUserDB[++i].Split('|');

               //make the address list
                for (int j; j<lUserDBAddresses.Length; j++)
                {
                    string[] lAddrInfo = lUserDBAddresses[j].Split('+');
                    lUserAddresses.Add(new Address(lAddrInfo[0], lAddrInfo[1], lAddrInfo[2], lAddrInfo[3], lAddrInfo[4], lAddrInfo[5]));
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
                lSaveContent.Add(mUsers[i].ToString());
            }

            //return the success of writing the file.
            File.WriteAllLines(aSaveLocation, lSaveContent.ToArray());
            return File.Exists(aSaveLocation);
        }
    }
}
