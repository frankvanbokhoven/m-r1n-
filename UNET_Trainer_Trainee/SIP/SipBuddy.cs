using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pjsua2;

namespace UNET_Trainer_Trainee.SIP
{
    public class SipBuddy: Buddy
    {
        public string Name;//frank: let op deze properties zijn erbij gezet.
        public string Domain;
        public SipAccount Account;

        //brief SipBuddy::SipBuddy
        //param name
        //param domain
        //param account
        public SipBuddy(String name, String domain, SipAccount account)
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
            Console.Write("Buddy " + bi.uri + " is " + bi.presStatus.statusText);
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
