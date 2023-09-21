using fPeerLending.Data;
using fPeerLending.Entities;
using fPeerLending.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Runtime.Serialization;

using fanikiwaGL.Business;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fCommon.Utility;
using fCommissions.Commission.Business;
using fMessagingSystem.Entities;

namespace fPeerLending.Business
{
    public class ContactsComponent
    {

        #region "Contacts"
        public Contact CreateContact(Contact acc)
        {
            ContactDAC stc = new ContactDAC();
            return stc.Create(acc);
        }
        public void UpdateContact(Contact acc)
        {
            ContactDAC stc = new ContactDAC();
            stc.UpdateById(acc);
        }
        public void DeleteContact(int Id)
        {
            ContactDAC stc = new ContactDAC();
            stc.DeleteById(Id);
        }
        public List<Contact> GetAllContacts()
        {
            ContactDAC stc = new ContactDAC();
            return stc.Select();
        }
        public Contact GetContactById(int Id)
        {
            ContactDAC stc = new ContactDAC();
            return stc.SelectById(Id);
        }
        #endregion "Contacts"
    }
}
