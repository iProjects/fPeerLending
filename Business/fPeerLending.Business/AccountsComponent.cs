using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fanikiwaGL.Entities;
using fanikiwaGL.Business;
using fCommon.Utility;

namespace fPeerLending.Business
{
    public class AccountsComponent
    {

        #region "Accounts"
        public Account OpenAccount(Account acc)
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            return stc.OpenAccount(acc);
        }
        public void UpdateAccount(Account acc)
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            stc.UpdateAccount(acc);
        }
        public void CloseAccount(Account acc)
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            stc.CloseAccount(acc);
        }
        public List<Account> GetAllAccounts()
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            return stc.GetAllAccounts();
        }
        public Account GetAccount(int Id)
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            return stc.GetAccount(Id);
        }
        public List<Account> GetMemberAccounts(int MemberId)
        {
            StaticTransactionsComponent stc = new StaticTransactionsComponent();
            return stc.GetAccountsByMember(MemberId);
        }
        #endregion "Accounts"
    }
}
