using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public class Address
    {
        private string mStreetNumber;
        private string mZip;
        private string mZipExt;
        private string mStreet;
        private string mCity;
        private string mState;

        public string StreetNumber
        {
            get { return mStreetNumber; }
            set { mStreetNumber = value; }
        }
        public string Zip
        {
            get { return mZip; }
            set { mZip = value; }
        }
        public string ZipExt
        { 
            get { return mZipExt; }
            set { mZipExt = value; }
        }
        public string Street
        {
            get { return mStreet; }
            set { mStreet = value; }
        }
        public string City
        {
            get { return mCity; }
            set { mCity = value; }
        }
        public string State
        {
            get { return mState; }
            set { mState= value; }
        }

 

        /// <summary>
        /// Create a new addreess with values = to parameters. you may leave the aZipExt parameter as an empty string if there is no zip extension("")
        /// </summary>
        /// <param name="aStreetNumber"></param>
        /// <param name="aStreetName"></param>
        /// <param name="?"></param>
        public Address(string aStreetNumber,string aStreetName, string aCity, string aState, string aZip, string aZipExt){
          mStreetNumber = aStreetNumber;
          mZip = aZip;
          mZipExt = aZipExt;
          mStreet = aStreetName;
          mCity = aCity;
          mState = aState;
        }

        /// <summary>
        /// Returns the content of the address, in the following example form:
        /// 123 Oak St, Manhattan, KS 66502 4234
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return mStreetNumber + " " + mStreet + ", " + mCity + ", " + mState + " " + mZip + " " + mZipExt;
        }

        /// <summary>
        /// Returns a string with the address parts delimited for storage in a db text file.
        /// </summary>
        /// <returns></returns>
        public string toDBString()
        {
            string lDelim = "+";
            return mStreetNumber + lDelim + mStreet + lDelim + mCity + lDelim + mState + lDelim + mZip + lDelim + mZipExt;
        }

    }
}
