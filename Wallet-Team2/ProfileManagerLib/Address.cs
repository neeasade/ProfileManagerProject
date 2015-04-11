using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    class Address
    {
        private string mStreetNumber;
        private string mZip;
        private string mZipExt;
        private string mStreet;
        private string mCity;
        private string mState;

        /// <summary>
        /// Create a new addreess with values = to parameters. you may leave the aZipExt parameter as an empty string if there is no zip extension("")
        /// </summary>
        /// <param name="aStreetNumber"></param>
        /// <param name="aStreetName"></param>
        /// <param name="?"></param>
        public Address(string aStreetNumber,string aStreetName, string aCity, string aState, string aZip, string aZipExt){
          mStreetNumber = aStreetNumber;
          mZip = aZip;
          mZipExt = aZip;
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
            //account for if mStreet has 'st' in it nad add if it is not:
            return mStreetNumber + " " + mStreet + ", " + mCity + ", " + mState + " " + mZip + mZipExt;
        }

        public string toDBString()
        {
            string lDelim = "+";
            return mStreetNumber + lDelim + mStreet + lDelim + mCity + lDelim + mState + lDelim + mZip + lDelim + mZipExt;
        }

    }
}
