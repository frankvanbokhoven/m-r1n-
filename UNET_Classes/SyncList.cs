using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{
    /// <summary>
    /// De SyncList is een normale List, maar geeft events zodra er iets gewijzigd wordt in de lijst (toevoegen of wijzigen)
    /// 
    /// zie: https://stackoverflow.com/questions/1351138/bindinglist-listchanged-event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncList<T> : System.ComponentModel.BindingList<T>
    {

        private System.ComponentModel.ISynchronizeInvoke _SyncObject;
        private System.Action<System.ComponentModel.ListChangedEventArgs> _FireEventAction;

        public SyncList() : this(null)
        {
        }

        public SyncList(System.ComponentModel.ISynchronizeInvoke syncObject)
        {

            _SyncObject = syncObject;
            _FireEventAction = FireEvent;
        }

        /// <summary>
        /// When something in the list changes, this event fires
        /// </summary>
        /// <param name="args"></param>
        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs args)
        {
            if (_SyncObject == null)
            {
                FireEvent(args);
            }
            else
            {
                _SyncObject.Invoke(_FireEventAction, new object[] { args });
            }
        }

        private void FireEvent(System.ComponentModel.ListChangedEventArgs args)
        {
            base.OnListChanged(args);
        }
    }
}
