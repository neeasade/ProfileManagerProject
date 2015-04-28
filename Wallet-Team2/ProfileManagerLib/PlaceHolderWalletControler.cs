using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManagerLib
{
    class PlaceHolderWalletControler : PlaceHolderSubject
    {
        private List<PlaceHolderObserver> observers;
        private bool purchasing;
        public PlaceHolderWalletControler()
        {
            purchasing = false;
        }
        public void RegisterObserver(PlaceHolderObserver observer)
        {
            observers.Add(observer);
        }
        public void UnregisterObserver(PlaceHolderObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(bool purchasing)
        {
            foreach (PlaceHolderObserver x in observers)
            {
                x.Update(purchasing);
            }
        }
    }
}
