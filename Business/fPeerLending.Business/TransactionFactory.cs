using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using fanikiwaGL.Entities;
using fanikiwaGL.Framework;

using fCommon.Utility;
using fCommon.Extension_Methods;
using fCommissions.Commission.Business;
using System.Configuration;
using fanikiwaGL.Business;

namespace fPeerLending.Business
{
    public class TransactionFactory
    {
        public static List<Transaction> CreateTransactions(FinanceTransactionType Ft,
            int tt,
            decimal Amount, 
            string Narr,
            int DrAccountID,
            int CrAccountID,
            string reference,
            string UserID,
            string Authorizer)
        {
            switch (Ft)
            {
                case FinanceTransactionType.Deposit:
                    { 
                        DepositComponent dc = new DepositComponent();
                        return dc.Deposit(tt, DrAccountID, CrAccountID, Amount, Narr, reference, UserID, Authorizer);
                    }
                case FinanceTransactionType.Withdraw:
                    {
                        WithdrawComponent wc = new WithdrawComponent();
                        return wc.Withdraw(tt, DrAccountID, CrAccountID, Amount, Narr, reference, UserID, Authorizer);
                    }
                default:
                    FinancialTransactionComponent fc = new FinancialTransactionComponent();
                    return fc.CreateTransactionsFromTransactionType(tt,DrAccountID,CrAccountID, Amount, Narr, reference, UserID, Authorizer);

            }
        }


        public static void Post(List<Transaction> txns)
        {
            //now request posting service to post
            FinancialPostingComponent fPostingClient = new FinancialPostingComponent();
            fPostingClient.BatchPost(txns);
        }

        public static void Post(DoubleEntry de)
        {
            Post(new List<Transaction>() { de.Debit, de.Credit });
        }

        public static BatchSimulateStatus SimulatePost(List<Transaction> txns)
        {
            FinancialPostingComponent fpost = new FinancialPostingComponent();
            return fpost.SimulatePostBatch(txns);
        }
    }


    public enum FinanceTransactionType
    {
        Deposit = 0,
        Withdraw = 1,
        Transfer = 2,
    }
}
