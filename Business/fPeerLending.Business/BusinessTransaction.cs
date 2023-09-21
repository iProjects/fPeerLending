using System;
using System.Collections.Generic;
using fanikiwaGL.Entities;
using fCommon.Utility;
using fCommon.Extension_Methods;
using fCommissions.Commission.Business;


namespace fPeerLending.Business
{
    public abstract class MoneyTransaction
    {
        #region Business Properties
        public DateTime PostDate { get; set; }
        public DateTime ValueDate { get { return valuedate; } set { valuedate = value; } }
        public DateTime RecordDate { get { return DateTime.Today; } }
        public bool ForcePostFlag { get; set; }
        public string StatementFlag { get; set; }
        public string Authorizer { get; set; }
        public string UserID { get; set; }
        public string Reference { get; set; }
        public string ContraReference { get { return contraref; } }
        public string ShortCode { get; set; }
        public int DebitAccount { get; set; }
        public int CreditAccount { get; set; }
        public int CommissionDebitAccount { get; set; }
        public int CommissionCreditAccount { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public bool ChargeCommission { get; set; }
        public bool ChargeCommissionToTransaction { get { return sepCommission; } set { sepCommission = value; } }
        public string Narrative { get; set; }
        public int TransactionType { get; set; }
        public int CommissionTransactionType { get; set; }

        #endregion

        #region Private Props

        TransactionType TType;
        bool sepCommission = false;
        string contraref;
        DateTime valuedate;
        #endregion

        public MoneyTransaction(TransactionType ttype,
            string shortCode, DateTime postDate,
            int drAccount,
            int crAccount,
            decimal amount,
            bool forcePost,
            string statFlag,
            string authorizer,
            string user,
            string reference
            )
        {
            if (ttype == null) throw new ArgumentNullException("Transaction type", "Transaction type cannot be null");
            TType = ttype;
            TransactionType = ttype.TransactionTypeID;

            PostDate = postDate;
            ShortCode = shortCode;
            DebitAccount = drAccount;
            CreditAccount = crAccount;
            Amount = amount;
            ForcePostFlag = forcePost;
            StatementFlag = statFlag;
            Authorizer = authorizer;
            UserID = user;
            Reference = reference;

            contraref = Utils.GetRandomHexNumber(10);
            ValueDate = postDate;
            //Call initialize

            Initialize();
        }


        private void Initialize()
        {
            /*
             * the following setting follow in order of priority
             * 1. If user pushes it during transction - use the user setting
             *   otherwise
             * 2. If it is set in the TType - use the TType setting
             *   otherwise
             * 3. If it is set in the config file - use the config setting
             *   otherwise
             * 4. Use the hardcoded setting hereabove
             */
            //Who to debit commission - look at the Charge who field of transaction type
            if (!string.IsNullOrEmpty(TType.ChargeWho) && TType.ChargeWho.Equals("C"))
            {
                CommissionDebitAccount = CreditAccount;
            }
            else
            {
                CommissionDebitAccount = DebitAccount;
            }

            //Who to debit commission
            CommissionCreditAccount = Config.GetInt("COMMISSIONACCOUNT");
            //if there is one set in the TType use it
            if (TType.CommissionCrAccount != 0) CommissionCreditAccount = TType.CommissionCrAccount;
            //Use generic comm txn type
            CommissionTransactionType = Config.GetInt("COMMISSIONTRANSACTIONTYPE");
            if (TType.CommissionTransactionType != 0) CommissionTransactionType = TType.CommissionTransactionType;

            //value dates
            if (TType.ValueDateOffset > 0) valuedate = PostDate.AddDays(TType.ValueDateOffset);

            ChargeCommission = TType.ChargeCommission;

        }

        #region Default behaviour
        public virtual Transaction DebitTransaction
        {
            get
            {
                Transaction txn = new Transaction();
                txn.AccountID = DebitAccount;
                txn.Amount = Amount * -1;
                txn.TransactionTypeId = TransactionType;
                txn.PostDate = PostDate;
                txn.ValueDate = ValueDate;
                txn.RecordDate = RecordDate;
                txn.ForcePostFlag = ForcePostFlag;
                txn.StatementFlag = StatementFlag;
                txn.Authorizer = Authorizer;
                txn.UserID = UserID;
                txn.Reference = Reference;
                txn.ContraReference = ContraReference;
                txn.Narrative = Narrative;
                return txn;
            }
        }
        public virtual Transaction CreditTransaction
        {
            get
            {
                Transaction txn = new Transaction();
                txn.AccountID = CreditAccount;
                txn.Amount = Amount;
                txn.TransactionTypeId = TransactionType;
                txn.PostDate = PostDate;
                txn.ValueDate = ValueDate;
                txn.RecordDate = RecordDate;
                txn.ForcePostFlag = ForcePostFlag;
                txn.StatementFlag = StatementFlag;
                txn.Authorizer = Authorizer;
                txn.UserID = UserID;
                txn.Reference = Reference;
                txn.ContraReference = ContraReference;
                txn.Narrative = Narrative;
                return txn;
            }
        }

        public virtual Transaction DebitCommissionTransaction
        {
            get
            {
                decimal commission = EvaluateCommission();
                //if (commission == 0) return null;

                //commission Dr
                Transaction Drtxn = new Transaction();
                Drtxn.AccountID = CommissionDebitAccount;
                Drtxn.Amount = commission * -1;
                Drtxn.TransactionTypeId = CommissionTransactionType;
                Drtxn.PostDate = PostDate;
                Drtxn.ValueDate = ValueDate;
                Drtxn.RecordDate = RecordDate;
                Drtxn.ForcePostFlag = ForcePostFlag;
                Drtxn.StatementFlag = StatementFlag;
                Drtxn.Authorizer = Authorizer;
                Drtxn.UserID = UserID;
                Drtxn.Reference = Reference;
                Drtxn.ContraReference = ContraReference;
                Drtxn.Narrative = "Comm";
                return Drtxn;
            }
        }
        public virtual Transaction CreditCommissionTransaction
        {
            get
            {

                decimal commission = EvaluateCommission();
                //if (commission == 0) return null;

                Transaction Crtxn = new Transaction();
                Crtxn.AccountID = CommissionCreditAccount;
                Crtxn.Amount = commission;
                Crtxn.TransactionTypeId = CommissionTransactionType; ;
                Crtxn.PostDate = PostDate;
                Crtxn.ValueDate = ValueDate;
                Crtxn.RecordDate = RecordDate;
                Crtxn.ForcePostFlag = ForcePostFlag;
                Crtxn.StatementFlag = StatementFlag;
                Crtxn.Authorizer = Authorizer;
                Crtxn.UserID = UserID;
                Crtxn.Reference = Reference;
                Crtxn.ContraReference = ContraReference;
                Crtxn.Narrative = "Comm";
                return Crtxn;
            }
        }

        public virtual DoubleEntry GetDoubleEntry(NarrativeFormat fmt)
        { return GetDoubleEntry(fmt, false); }
        public virtual DoubleEntry GetDoubleEntry(NarrativeFormat fmt, bool format)
        {

            return new DoubleEntry
               {
                   Debit = GetTransaction(DebitTransaction, fmt.DrNarrativeFommatter, format),
                   Credit = GetTransaction(this.CreditTransaction, fmt.CrNarrativeFommatter, format)
               };
        }
        public virtual Transaction GetTransaction(Transaction txn, string Fommatter, bool format)
        {
            if (format) txn.Narrative = Fommatter.FormatWith(this);
            return txn;
        }
        public virtual Transaction GetTransaction(Transaction txn, string Fommatter)
        {
            return GetTransaction(txn, Fommatter, false);
        }

        public virtual decimal EvaluateCommission()
        {
            CommissionComponent cc = new CommissionComponent();
            return cc.ComputeCommissionByTransactionTypeId(this.Amount, this.TransactionType);
        }
        public virtual DoubleEntry GetCommissionTransaction(NarrativeFormat fmt)
        { return GetCommissionTransaction(fmt, false); }
        public virtual DoubleEntry GetCommissionTransaction(NarrativeFormat fmt, bool format)
        {
            return new DoubleEntry
                {
                    Debit = GetTransaction(this.DebitCommissionTransaction, fmt.DrNarrativeCommissionFommatter, format),
                    Credit = GetTransaction(this.CreditCommissionTransaction, fmt.CrNarrativeCommissionFommatter, format)
                };
        }

        public virtual List<Transaction> GetChargeCommissionToTransaction(NarrativeFormat fmt)
        //false = The narrative will not be formatted
        { return GetChargeCommissionToTransaction(fmt, false); }
        public virtual List<Transaction> GetChargeCommissionToTransaction(NarrativeFormat fmt, bool format)
        {
            List<Transaction> txns = new List<Transaction>();
            Transaction dr = DebitTransaction;

            Transaction cr = CreditTransaction;
            Transaction cmm = CreditCommissionTransaction;
            dr.Amount -= cmm.Amount;

            txns.Add(GetTransaction(dr, fmt.DrNarrativeCommissionFommatter, format));
            txns.Add(GetTransaction(cr, fmt.DrNarrativeCommissionFommatter, format));
            txns.Add(GetTransaction(cmm, fmt.DrNarrativeCommissionFommatter, format));
            return txns;
        }

        public virtual List<Transaction> GetTransactionsIncludingCommission(NarrativeFormat MainFmt, NarrativeFormat CommFmt, bool UseFormat = false)
        {
            List<Transaction> txns = new List<Transaction>();
            DoubleEntry mainDe = GetDoubleEntry(MainFmt, UseFormat);
            if (mainDe.Debit != null) txns.Add(mainDe.Debit);
            if (mainDe.Credit != null) txns.Add(mainDe.Credit);

            if (ChargeCommission)
            {
                DoubleEntry cmmDe = GetCommissionTransaction(CommFmt, UseFormat);
                if (cmmDe.Debit != null) txns.Add(cmmDe.Debit);
                if (cmmDe.Credit != null) txns.Add(cmmDe.Credit);
            }
            return txns;
        }


        #endregion

    }

    public class DoubleEntry
    {
        public Transaction Debit { get; set; }
        public Transaction Credit { get; set; }
    }

    public class NarrativeFormat
    {
        #region Private Props

        TransactionType TType;

        private string narrativeFommatterDr = "{ShortCode}";
        private string narrativeFommatterCr = "{ShortCode}";
        private string narrativeCommissionFommatterDr = "{ShortCode} Comm";
        private string narrativeCommissionFommatterCr = "{ShortCode} Comm{DebitAccount}";
        #endregion

        public NarrativeFormat(TransactionType TransactionType)
        {
            if (TransactionType == null) throw new ArgumentNullException("TransactionType", "TransactionType cannot be null");
            TType = TransactionType;
            //NarrativeFormatter
            if (!string.IsNullOrEmpty(TType.DefaultMainNarrative))
                // DrNarrativeFommatter = TType.DefaultMainNarrative;
                narrativeFommatterDr = TType.DefaultMainNarrative;
            if (!string.IsNullOrEmpty(TType.DefaultContraNarrative))
                narrativeFommatterCr = TType.DefaultContraNarrative;
            // CrNarrativeFommatter = TType.DefaultContraNarrative;

            //CommNarrativeFormatter
            if (!string.IsNullOrEmpty(TType.CommissionMainNarrative))
                //DrNarrativeCommissionFommatter = TType.CommissionMainNarrative;
                narrativeCommissionFommatterDr = TType.CommissionMainNarrative;
            if (!string.IsNullOrEmpty(TType.CommissionContraNarrative))
                //CrNarrativeCommissionFommatter = TType.CommissionContraNarrative;
                narrativeCommissionFommatterCr = TType.CommissionContraNarrative;
        }
        public string DrNarrativeFommatter { get { return narrativeFommatterDr; } set { narrativeFommatterDr = value; } }
        public string CrNarrativeFommatter { get { return narrativeFommatterCr; } set { narrativeFommatterCr = value; } }
        public string DrNarrativeCommissionFommatter { get { return narrativeCommissionFommatterDr; } set { narrativeCommissionFommatterDr = value; } }
        public string CrNarrativeCommissionFommatter { get { return narrativeCommissionFommatterCr; } set { narrativeCommissionFommatterCr = value; } }
    }

    public class DepositTransaction : MoneyTransaction
    {
        public DepositTransaction(TransactionType TType,
            string shortCode, DateTime postDate,
            int drAccount,
            int crAccount,
            decimal amount,
            bool forcePost,
            string statFlag,
            string authorizer,
            string user,
            string reference
            )
            : base(TType,
            shortCode, postDate,
             drAccount,
             crAccount,
             amount,
             forcePost,
             statFlag,
             authorizer,
             user,
             reference
            )
        {
        }



    }

    public class WithdrawalTransaction : MoneyTransaction
    {
        public WithdrawalTransaction(TransactionType TType,
            string shortCode, DateTime postDate,
            int drAccount,
            int crAccount,
            decimal amount,
            bool forcePost,
            string statFlag,
            string authorizer,
            string user,
            string reference
            )
            : base(TType,
            shortCode, postDate,
             drAccount,
             crAccount,
             amount,
             forcePost,
             statFlag,
             authorizer,
             user,
             reference
            )
        {
        }



    }

    public class GenericTransaction : MoneyTransaction
    {
        public GenericTransaction(TransactionType TType,
            string shortCode, DateTime postDate,
            int drAccount,
            int crAccount,
            decimal amount,
            bool forcePost,
            string statFlag,
            string authorizer,
            string user,
            string reference
            )
            : base(TType,
            shortCode, postDate,
             drAccount,
             crAccount,
             amount,
             forcePost,
             statFlag,
             authorizer,
             user,
             reference
            )
        {
        }

    }

    public class LoanTransaction : MoneyTransaction
    {
        public LoanTransaction(TransactionType TType,
            string shortCode, DateTime postDate,
            int drAccount,
            int crAccount,
            decimal amount,
            bool forcePost,
            string statFlag,
            string authorizer,
            string user,
            string reference
            )
            : base(TType,
            shortCode, postDate,
             drAccount,
             crAccount,
             amount,
             forcePost,
             statFlag,
             authorizer,
             user,
             reference
            )
        {

        }





    }
}