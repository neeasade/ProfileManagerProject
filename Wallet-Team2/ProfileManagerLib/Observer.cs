using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    public interface Observer
    {
        // Welcome to Cosco, I love you.
        // This function is to update the Transaction status of a user.
        void SetUserTransactionStatus(string aEmail, bool aTransaction);
    }
}
