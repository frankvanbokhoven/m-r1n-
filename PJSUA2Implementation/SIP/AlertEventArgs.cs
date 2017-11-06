using pjsua2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSUA2Implementation.SIP
{
    public class AlertEventArgs : EventArgs
    {
        #region AlertEventArgs Properties
        private string _uui = null;
        private int _id;
        private CallInfo _callinfo;
        private Media _mediaofcall;
        private string _caller_accountname;
        #endregion

        #region Get/Set Properties
        public string uuiData
        {
            get { return _uui; }
            set { _uui = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Media Media_Of_Call
        {
            get { return _mediaofcall; }
            set { _mediaofcall = value; }
        }

        public CallInfo CallInfo_Of_Call
        {
            get { return _callinfo; }
            set { _callinfo = value; }
        }
        
        public string Caller_AccountName
        {
            get { return _caller_accountname; }
            set { _caller_accountname = value; }
        }
        #endregion
    }
}
