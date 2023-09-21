using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using fanikiwaGL.Business;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fCommon.Utility;
using fCommissions.Commission.Business;
using fMessagingSystem.Entities;

namespace fPeerLending.Business
{
    public class FinancialTransactionComponent
    {

        public List<Transaction> CreateTransactionsFromTransactionType(int TransactionTypeId,
            int DrAccountID,
            int CrAccountID,
            decimal Amount,
            string Narr,
            string reference,
           string UserID,
           string Authorizer)
        {
            /*
             * DR & CR Whatever the TType says
             */
            //1. Get the transaction type
            //throw the deposit in suspense if account does not exist
            StaticTransactionsComponent sPostingClient = new StaticTransactionsComponent();
            TransactionType tt = sPostingClient.GetTransactionType(TransactionTypeId);

            
            //2. Get Accounts

            int MainAccId; int ContraAccId;
            if ("D".Equals(tt.DebitCredit)) //is a debit transation
            {
                MainAccId = DrAccountID;
                ContraAccId = CrAccountID;
            }
            else //is a credit transaction
            {
                MainAccId = CrAccountID; 
                ContraAccId =DrAccountID;
            }

            // 1.1 Get suspense accounts
            if (tt.CanSuspend)
            { 
                if (tt.SuspenseDrAccount == 0)
                    throw new ArgumentNullException("SuspenseDrAccount", "SuspenseDrAccount[0] not allowed");
                if (!sPostingClient.AccountExists(tt.SuspenseDrAccount))
                    throw new ArgumentNullException("SuspenseDrAccount", "SuspenseDrAccount[" + tt.SuspenseDrAccount + "] does not exist");

                if (tt.SuspenseCrAccount == 0)
                    throw new ArgumentNullException("SuspenseDrAccount", "SuspenseCrAccount[0] not allowed");
                if (!sPostingClient.AccountExists(tt.SuspenseCrAccount))
                    throw new ArgumentNullException("SuspenseCrAccount", "SuspenseCrAccount[" + tt.SuspenseCrAccount + "] does not exist");

                if(!sPostingClient.AccountExists(MainAccId)) MainAccId = tt.SuspenseDrAccount;
                if (!sPostingClient.AccountExists(ContraAccId)) ContraAccId = tt.SuspenseCrAccount;
            }


            //3. Get the transaction type
            
            GenericTransaction gt = new GenericTransaction(tt, tt.ShortCode,
                DateTime.Today,
                ContraAccId,
                MainAccId,
                Amount,
                tt.ForcePost,
                tt.StatFlag,
                UserID,
                Authorizer,
                reference);

            //4. Configure gt according to transaction type
            //eg a) dp.CommissionTransactionType = MPESACOMMACC
            gt.CommissionTransactionType = tt.CommissionTransactionType;
            gt.Narrative = Narr;
            

            //5.Get the GL transactions
            List<Transaction> txns = new List<Transaction>();
            
            //5.1 change formatting of narratives here
            NarrativeFormat fmt = new NarrativeFormat(tt);
            bool useTTNarrative=false;
            if (tt.NarrativeFlag == 1) useTTNarrative = true;
            //5.2 Get transctions double entry
            DoubleEntry de = gt.GetDoubleEntry(fmt, useTTNarrative);
            if (de.Credit != null) txns.Add(de.Credit);
            if (de.Debit != null) txns.Add(de.Debit);

            //5.3 charge commission if need be

            if (tt.ChargeCommission)
            {
                DoubleEntry comm = gt.GetCommissionTransaction(fmt, useTTNarrative);
                if (comm.Credit != null) txns.Add(comm.Credit);
                if (comm.Debit != null) txns.Add(comm.Debit);
            }

            if (tt.ChargeCommissionToTransaction)
            {
                return gt.GetChargeCommissionToTransaction(fmt, useTTNarrative);
            }


            return txns;
        }
    }
}
