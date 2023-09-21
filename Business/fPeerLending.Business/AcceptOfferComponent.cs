//====================================================================================================
// Code generated with Motion: BC Gen (Build 2.2.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 08/03/2013 18:41:52 
//====================================================================================================


using fPeerLending.Data;
using fPeerLending.Entities;
using System;
using System.Collections.Generic;

using fanikiwaGL.Business;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fPeerLending.Framework.ExceptionTypes;

using fCommon.Utility;
using fMessagingSystem.Entities;
using fanikiwaGL.Framework.ExceptionTypes;


namespace fPeerLending.Business
{

    /// <summary>
    /// AcceptOffer business component.
    /// </summary>
    public partial class AcceptOfferComponent
    {
        public AcceptOfferComponent()
        {

        }

        /// <summary>
        /// AcceptBorrowOffer business method. 
        /// </summary>
        /// <param name="id">A id value.</param>
        /// <param name="account">A account value.</param>
        /// <param name="loan">A loan value.</param>
        /// <param name="aBorrowOffer">A offer value.</param>
        /// 
        string userID = "SYS";
        string Authorizer = "Auth";

        public void AcceptBorrowOffer(Member lender, Offer aBorrowOffer)
        {
            ///TODO realize accept offer usecase
            ///
            ValidateOffer(aBorrowOffer, lender);
            StaticOfferChange.SetOfferStatus(aBorrowOffer, OfferStatus.Processing);

            //get the borrower from the offer
            MemberDAC mDAC = new MemberDAC();
            Member borrower = mDAC.SelectById(aBorrowOffer.MemberId);
            if (borrower.MemberId == lender.MemberId)
            {
                //Before throwing the error, revert status to open
                StaticOfferChange.SetOfferStatus(aBorrowOffer, OfferStatus.Open);
                throw new OffersException("Cannot accept self offers");
            }

            //Check ability to pay
            List<Transaction> txns = LoanTransactions(lender, borrower, aBorrowOffer);
            if (txns.Count < 4)
            {
                //Before throwing the error, revert status to open
                StaticOfferChange.SetOfferStatus(aBorrowOffer, OfferStatus.Open);
                throw new LoansException("Loan Transactions not well formed");
            }
            BatchSimulateStatus bss = TransactionFactory.SimulatePost(txns);
            if (!bss.CanPost)
            {
                //Before throwing the error, revert status to open
                StaticOfferChange.SetOfferStatus(aBorrowOffer, OfferStatus.Open);
                throw new BatchSimulationException(bss, "Posting Validation Failed");
            }

            //create loan
            CreateLoan(borrower, lender, aBorrowOffer);
            StaticOfferChange.SetOfferStatus(aBorrowOffer, OfferStatus.Closed);
        }

        //<summary>
        //AcceptLendOffer business method. 
        //</summary>
        //<param name="loan">A loan value.</param>

        public void AcceptLendOffer(Member borrower, Offer aLendOffer)
        {
            ///TODO realize accept offer usecase

            ValidateOffer(aLendOffer, borrower);
            StaticOfferChange.SetOfferStatus(aLendOffer, OfferStatus.Processing);

            //get the lender from the offer
            MemberDAC mDAC = new MemberDAC();
            Member lender = mDAC.SelectById(aLendOffer.MemberId);
            if (borrower.MemberId == lender.MemberId)
            {
                //Before throwing the error, revert status to open
                StaticOfferChange.SetOfferStatus(aLendOffer, OfferStatus.Open);
                throw new OffersException("Cannot accept self offers");
            }

            if (CheckLimit(lender.CurrentAccountId, aLendOffer.Amount))
            {
                ClearLimit(lender.CurrentAccountId, aLendOffer.Amount);
                CreateLoan(borrower, lender, aLendOffer);
            }
            else
            {
                //Before throwing the error, revert status to open
                StaticOfferChange.SetOfferStatus(aLendOffer, OfferStatus.Open);
                throw new StaticPostingException("Insufficient Limit");
            }

            StaticOfferChange.SetOfferStatus(aLendOffer, OfferStatus.Closed);
        }

        private void ValidateOffer(Offer offer, Member acceptee)
        {

            if (!offer.Status.Equals("Open"))
            {
                throw new ArgumentException("OfferId", string.Format("Offer [{0}] not available, Status is [" + offer.Status + "]. ", offer.Id));
            }


            if (offer.ExpiryDate < DateTime.Today)
            {
                throw new OffersException(string.Format("Offer [{0}] is expired. ", offer.Id));
            }
            if (!offer.PublicOffer.Equals("B") && !PrivateOfferred(offer, acceptee))
            { //the offer is a private offer and you dont exist in th offerees list
                throw new OffersException(string.Format("Offer [{0}] is not offerred to you. ", offer.Id));
            }
        }

        private bool PrivateOfferred(Offer offer, Member member)
        {
            if (offer.Offerees.Contains(member.Email)) return true;
            if (offer.Offerees.Contains(member.Telephone)) return true;
            //Check if member belongs to the group
            //if (offer.Offerees.Contains(member.Telephone)) return true;
            return false;
        }

        private void CreateLoan(Member borrower, Member lender, Offer offer)
        {
            /*
            1.	The system blocks the �lend offer� so that other potential borrowers do not accept the offer
            2.	The system establishes the loan in the loan book
            3.	The system logs the loan repayment schedule in the diary
            4.	The systems creates electronic loan contract
            5.	The AccountingSystem posts the loan transaction with its attendant commission
            6.	The system closes the �lend offer�
         * //these two are done by messaging component
            7.	The MessagingSystem sends the electronic loan contract
            8.	The MessagingSystem informs both the lender and borrower of the successful transaction

     */

            //STEP 1  Establish Loan
            Loan loan = this.EstablishLoan(lender, borrower, offer);


            //STEP 2 Log Loan repayment scedhule
            LogLoanRepayment(lender, borrower, loan);


            //SETP 3 Post loan transation
            TransactionFactory.Post(LoanTransactions(lender, borrower, offer));

        }

        private void ClearLimit(int AccountId, decimal Amount)
        {
            //Unblock the blocked amount
            StaticTransactionsComponent sc = new StaticTransactionsComponent();
            sc.UnBlockFunds(AccountId, Amount);
        }

        private bool CheckLimit(int AccountId, decimal Amount)
        {
            StaticTransactionsComponent sc = new StaticTransactionsComponent();
            return sc.GetAccountLimitAmount(AccountId) >= Amount;
        }

        //     <summary>
        //     AcceptPartialBorrowOffer business method. 
        //     </summary>
        //     <param name="loan">A loan value.</param>
        //     <param name="offer">A offer value.</param>
        public void AcceptPartialBorrowOffer(Member lender, Offer partialoffer)
        {
            ///TODO realize accept offer usecase

            //Get offer               
            ListOffersComponent lc = new ListOffersComponent();
            Offer _offer = lc.GetOfferById(partialoffer.Id);
            //check that partial offer is less or equal to offer amount
            if (partialoffer.Amount > _offer.Amount)
            {
                throw new OffersException( "Accepted Amount is greater than Offer Amount!");
            }

            ///
            //get the borrower from the offer
            MemberDAC mDAC = new MemberDAC();
            Member borrower = mDAC.SelectById(partialoffer.MemberId);

            CreateLoan(borrower, lender, partialoffer);

            //decrease offer amount. Use the OfferDAC
            _offer.Amount = _offer.Amount - partialoffer.Amount;

            //offer.Amount = offer.Amount - loan.Amount;
            OfferDAC offerDAC = new OfferDAC();
            if (_offer.Amount <= 0)
            {
                //Offer fully subscribed. Change the offer status to closed. 
                StaticOfferChange.SetOfferStatus(_offer, OfferStatus.Closed);
            }
            else
            {
                //unlock the offer. Change the offer status to Open.

            }

        }

        #region private methods used by acceptoffer

        public Loan EstablishLoan(Member lender, Member borrower, Offer offer)
        {
            LoanDAC loanDAC = new LoanDAC();
            //create a new loan
            Loan loan = new Loan();

            //fill up loan details from offer details
            InterestComponent ic = new InterestComponent();
            decimal intr = ic.ComputeSimpleInterest(offer.Amount, offer.Term, (decimal)offer.Interest);
            loan.Term = offer.Term;
            loan.Amount = offer.Amount + intr;
            loan.Interest = offer.Interest;
            loan.MaturityDate = offer.ExpiryDate;
            loan.CreatedDate = DateTime.Today;
            loan.MemberId = borrower.MemberId;
            loan.OfferId = offer.Id;
            loan.PartialPay = offer.PartialPay;
            //loan.AccruedInterest = //compute accrued interest

            //Now create the loan in the loan book
            return loanDAC.Create(loan);
        }
        public void LogLoanRepayment(Member lender, Member borrower, Loan loan)
        {
            /*
             * We create 2 STOs
             * STO 1 - for Repaying
             *  Dr Borrower.CurrentID  with PayAmount
             *      Cr Investor.CurrentAcc with PayAmount
             *      
             */

            STO cashSTO = new STO();

            //STO 1 - for moving cash btween current accounts of lender and borrower
            cashSTO.STOAccType = (int)STOAccType.Cash;
            cashSTO.STOType = (int)STOTYPE.Normal;
            cashSTO.Interval = RepaymentInterval.M.ToString(); //Create enum called RapaymentInterval
            cashSTO.NoOfPayments = loan.Term; // total no of payments to make
            cashSTO.CreateDate = DateTime.Today;
            cashSTO.StartDate = DateTime.Today; //when does repayment start;
            cashSTO.NextPayDate = cashSTO.StartDate.AddMonths(1); //next repayment starts a month from today
            cashSTO.EndDate = cashSTO.StartDate.AddMonths(loan.Term); //when does repayment end? Repayment ends start date plus no of payments months
            cashSTO.DrAccount = borrower.CurrentAccountId; //during loan repayment debit borrower
            cashSTO.CrAccount = lender.CurrentAccountId; //during loan repayment credit lender
            cashSTO.CommissionAccount = Config.GetInt("COMMISSIONACCOUNT");
            cashSTO.DrTxnType = Config.GetInt("LOANREPAYMENTTRANSACTIONTYPE");
            cashSTO.CrTxnType = Config.GetInt("LOANREPAYMENTTRANSACTIONTYPE"); //not used for now
            cashSTO.AmountPaid = 0;
            cashSTO.PayAmount = (loan.Amount / loan.Term);
            cashSTO.TotalToPay = loan.Amount;
            cashSTO.CommFreqFlag = (short)Config.GetEnumValue<STOCommFreqFlag>("STOCommFreqFlag");
            cashSTO.ChargeWho = (short)Config.GetEnumValue<STOCommissionChargeWho>("CHARGEWHOFLAG");
            cashSTO.LimitFlag = 0;
            cashSTO.PartialPay = loan.PartialPay;
            cashSTO.AmountDefaulted = 0;
            cashSTO.NoOfDefaults = 0;
            cashSTO.NoOfPaymentsMade = 0; // will be set by the diary processing as and when a repayment is done
            cashSTO.LoanId = loan.Id;

            StaticTransactionsComponent stPost = new StaticTransactionsComponent();
            stPost.CreateSTO(cashSTO);


            //STO 2 - for reducing loans and investements
            STO lr = new STO();
            lr.STOAccType = (int)STOAccType.Loan; 
            lr.STOType = (int)STOTYPE.Normal;
            lr.Interval = RepaymentInterval.M.ToString(); //Create enum called RapaymentInterval
            lr.NoOfPayments = loan.Term; // total no of payments to make
            lr.CreateDate = DateTime.Today;
            lr.StartDate = DateTime.Today; //when does repayment start;
            lr.NextPayDate = lr.StartDate.AddMonths(1); //next repayment starts a month from today
            lr.EndDate = lr.StartDate.AddMonths(loan.Term); //when does repayment end? Repayment ends start date plus no of payments months
            lr.DrAccount =lender.InvestmentAccountId ; //during loan repayment debit lender
            lr.CrAccount = borrower.LoanAccountId; //during loan repayment credit borrower
            lr.CommissionAccount = Config.GetInt("COMMISSIONACCOUNT");
            lr.DrTxnType = Config.GetInt("LOANREPAYMENTTRANSACTIONTYPE");
            lr.CrTxnType = Config.GetInt("LOANREPAYMENTTRANSACTIONTYPE"); //not used for now
            lr.AmountPaid = 0;
            lr.PayAmount = (loan.Amount / loan.Term);
            lr.TotalToPay = loan.Amount;
            cashSTO.CommFreqFlag = (short)Config.GetEnumValue<STOCommFreqFlag>("STOCommFreqFlag");
            cashSTO.ChargeWho = (short)Config.GetEnumValue<STOCommissionChargeWho>("CHARGEWHOFLAG");
            
            lr.LimitFlag = 0;
            lr.PartialPay = loan.PartialPay;
            lr.AmountDefaulted = 0;
            lr.NoOfDefaults = 0;
            lr.NoOfPaymentsMade = 0; // will be set by the diary processing as and when a repayment is done
            lr.LoanId = loan.Id;

            stPost.CreateSTO(lr);
        }

        public List<Transaction> LoanTransactions(Member lender, Member borrower, Offer offer)
        {
            //Use this for all general ledger methods that does not post
            StaticTransactionsComponent sPostingClient = new StaticTransactionsComponent();

            //create all transactions
            List<Transaction> txns = new List<Transaction>();
            InterestComponent ic = new InterestComponent();
            decimal interest = ic.ComputeSimpleInterest(offer.Amount, offer.Term, (decimal)offer.Interest);

            //establish loan with attendant commission
            TransactionType tt = sPostingClient.GetTransactionType(Config.GetInt("ESTABLISHLOANTRANSACTIONTYPE"));
            if (tt == null) throw new ArgumentNullException("Transaction type", "Transaction type cannot be null");

            //the loan transaction also unblocks blocked funds
            LoanTransaction ltxn = new LoanTransaction(tt,
                "LES",
                DateTime.Today,
                borrower.LoanAccountId,
                lender.InvestmentAccountId,
                offer.Amount + interest,
                false,
                "Y",
                Authorizer,
                userID,
                offer.Id.ToString());

            txns.AddRange(ltxn.GetTransactionsIncludingCommission(new NarrativeFormat(tt), new NarrativeFormat(tt), true));

            //Disburse Amount
            TransactionType Distt = sPostingClient.GetTransactionType(Config.GetInt("DISBURSELOANTRANSACTIONTYPE"));
            GenericTransaction Distxn = new GenericTransaction(Distt,
                "DIS",
                DateTime.Today,
                lender.CurrentAccountId,
                borrower.CurrentAccountId,
                offer.Amount,
                false,
                "Y",
                Authorizer,
                userID,
                offer.Id.ToString());

            txns.AddRange(Distxn.GetTransactionsIncludingCommission(new NarrativeFormat(Distt), new NarrativeFormat(Distt),true));
            return txns;
        }

        #endregion

        #region Message creation using offer, lender and borrower -- LETS THINK WHETHER THESE SHOULD BE IN UI
        private SMSMessage CreateSMS(string fromTelno, string toTelno, string msg)
        {
            if (string.IsNullOrEmpty(fromTelno))
                throw new ArgumentNullException("fromTelno");
            if (string.IsNullOrEmpty(toTelno))
                throw new ArgumentNullException("toTelno");

            if (string.IsNullOrEmpty(msg))
                throw new ArgumentNullException("msg");

            SMSMessage sms = new SMSMessage();
            sms.addressFrom = fromTelno;
            sms.addressTo = toTelno;
            sms.messageDate = DateTime.Today;
            sms.Body = msg;

            return sms;
        }

        private EmailMessage CreateEmail(string addressFrom, string addressTo, string msg)
        {
            if (string.IsNullOrEmpty(addressFrom))
                throw new ArgumentNullException("addressFrom");
            if (string.IsNullOrEmpty(addressTo))
                throw new ArgumentNullException("addressTo");

            if (string.IsNullOrEmpty(msg))
                throw new ArgumentNullException("msg");

            EmailMessage email = new EmailMessage();
            email.addressFrom = addressFrom;
            email.addressTo = addressTo;
            email.messageDate = DateTime.Today;
            email.subject = "Fanikiwa Offer";
            email.Body = msg;

            return email;
        }

        private Message CreateLenderMessage(Member lender, Member borrower, Offer o)
        {
            Message ret = null;

            decimal AvailableBalance = this.GetAvailableBalance(lender.CurrentAccountId);

            string msg = "Dear " + lender.Surname + " " + lender.OtherNames + " \n\nYou have made an investment of ksh " + o.Amount.ToString("N2") + " loaned to " + borrower.Surname + " " + borrower.OtherNames + "\nAvailable Balance: kshs " + AvailableBalance.ToString("N2") + "\n\nRegards, \nFanikiwa";

            string fanikiwaTelno = Config.GetString("FANIKIWATELNO");
            string fanikiwaEmail = Config.GetString("FANIKIWAEMAIL");

            if (lender.InformBy != null && "SMS".Equals(lender.InformBy.ToUpper()))
            {

                ret = this.CreateSMS(fanikiwaTelno, lender.Telephone, msg);

            }
            else if (lender.InformBy != null && "EMAIL".Equals(lender.InformBy.ToUpper()))
            {

                ret = this.CreateEmail(fanikiwaEmail, lender.Email, msg);
            }
            else
            {
                ret = this.CreateEmail(fanikiwaEmail, lender.Email, msg);
            }
            return ret;
        }

        private Message CreateBorrowerMessage(Member lender, Member borrower, Offer o)
        {
            Message ret = null;

            decimal AvailableBalance = this.GetAvailableBalance(borrower.CurrentAccountId);

            string msg = "Dear " + borrower.Surname + " " + borrower.OtherNames + "\n\nYou have been loaned ksh " + o.Amount.ToString("N2") + " from " + lender.Surname + " " + lender.OtherNames + "\nAvailable Balance: kshs " + AvailableBalance.ToString("N2") + "\n\nRegards, \nFanikiwa";

            string fanikiwaTelno = Config.GetString("FANIKIWATELNO");
            string fanikiwaEmail = Config.GetString("FANIKIWAEMAIL");

            if (borrower.InformBy != null && "SMS".Equals(borrower.InformBy.ToUpper()))
            {

                ret = this.CreateSMS(fanikiwaTelno, borrower.Telephone, msg);

            }
            else if (borrower.InformBy != null && "EMAIL".Equals(borrower.InformBy.ToUpper()))
            {

                ret = this.CreateEmail(fanikiwaEmail, borrower.Email, msg);
            }
            else
            {
                ret = this.CreateEmail(fanikiwaEmail, borrower.Email, msg);
            }
            return ret;
        }
        private Message CreatePartialBorrowerMessage(Member lender, Member borrower, Offer o)
        {
            Message ret = null;

            decimal AvailableOfferBalance = this.GetAvailableOfferBalance(o.Id);

            string msg = "Dear " + borrower.Surname + " " + borrower.OtherNames + " \n\nYou have been Partially loaned ksh " + o.Amount.ToString("N2") + " from " + lender.Surname + " " + lender.OtherNames + " \nOffer Balance: kshs" + AvailableOfferBalance.ToString("N2");

            string fanikiwaTelno = Config.GetString("FANIKIWATELNO");
            string fanikiwaEmail = Config.GetString("FANIKIWAEMAIL");

            if (borrower.InformBy != null && "SMS".Equals(borrower.InformBy.ToUpper()))
            {

                ret = this.CreateSMS(fanikiwaTelno, borrower.Telephone, msg);

            }
            else if (borrower.InformBy != null && "EMAIL".Equals(borrower.InformBy.ToUpper()))
            {

                ret = this.CreateEmail(fanikiwaEmail, borrower.Email, msg);
            }
            else
            {
                ret = this.CreateEmail(fanikiwaEmail, borrower.Email, msg);
            }
            return ret;
        }

        #endregion

        public Decimal GetAvailableOfferBalance(int _OfferId)
        {
            OfferDAC dac = new OfferDAC();

            return dac.GetAvailableOfferBalance(_OfferId);
        }
        public Decimal GetAvailableBalance(int _AccountId)
        {
            StaticTransactionsComponent sPostingClient = new StaticTransactionsComponent();

            Account acc = sPostingClient.GetAccount(_AccountId);

            return sPostingClient.GetAvailableBalance(acc);
        }


    }

    public enum OfferStatus
    {
        Processing,
        Closed,
        Open
    }

    public enum RepaymentInterval
    {
        D,
        M,
        W,
        Y
    }




}