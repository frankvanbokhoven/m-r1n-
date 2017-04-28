using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer_Trainee.SIP
{
    public class SipBuddy
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
        public void onBuddyState()
        {

           // PJSUA2.BuddyInfo bi = getInfo();
            //todo: message   std::cout << "Buddy " << bi.uri << " is " << bi.presStatus.statusText << std::endl;
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
