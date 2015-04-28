using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    interface PlaceHolderSubject
    {
        void RegisterObserver(PlaceHolderObserver observer);
        void UnregisterObserver(PlaceHolderObserver observer);
        void NotifyObservers(bool purchasing);
    }
}
