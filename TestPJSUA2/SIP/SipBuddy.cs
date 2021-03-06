﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;

namespace TestPJSUA2.SIP
{
    public class SipBuddy : Buddy
    {
        public string Name;//frank: let op deze properties zijn erbij gezet.
        public string Domain;
        public Account Account;

        //brief SipBuddy::SipBuddy
        //param name
        //param domain
        //param account
        public SipBuddy(String name, String domain, Account account)
        {
            Name = name;
            Domain = domain;
            Account = account;
        }


        /*!
         * \brief SipBuddy::onBuddyState
         */
        public override void onBuddyState()
        {
            BuddyInfo bi = getInfo();
            Classes.WCFcaller.SetSIPStatusMessage("Buddy " + bi.uri + " is " + bi.presStatus.statusText);
        }

        /*!
         * \brief SipBuddy::getName
         * \return
         */
        public String getName() //const
        {
            return Name;
        }

        /*!
         * \brief SipBuddy::getDomain
         * \return
         */
        public String getDomain() //const
        {
            return Domain;
        }
    }
}
