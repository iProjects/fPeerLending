
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Configuration;


namespace fPeerLending.MessageProcessor
{
    #region Message Parsing
    public class FanikiwaMessageFactory
    {

        public static FanikiwaMessage CreateMessage(string OriginatingAddress, DateTime MessageDate, string Body)
        {
            FanikiwaMessage fmessage = null;
            try
            {
                /*put condition that identifies a MpesaMessage*/
                if (OriginatingAddress.ToUpper().Equals("MPESA"))
                {
                    //convert to MpesaMessage
                    fmessage = ParseMpesaMessage(OriginatingAddress, MessageDate, Body);
                }
                else //test if this message is structured
                {
                    char[] delimiters = new char[] { '\r', '\n', '*', '#', ' ' };
                    List<string> msgParams = Body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

                    //Help message syntax = H | HELP <Command>
                    if ("H".Equals(msgParams.First().ToUpper()) || "H2".Equals(msgParams.First().ToUpper()) || "H3".Equals(msgParams.First().ToUpper()) || "HELP".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseHelpMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = B|BAL ; try parsing Balance Enquiry <BAL><account><Password>
                    else if ("B".Equals(msgParams.First().ToUpper()) || "BAL".Equals(msgParams.First().ToUpper()) || "BALANCE".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseBalanceEnquiryMessage(OriginatingAddress, MessageDate, Body, msgParams);
                    }
                    //Message starts with = S|STAT ; try parsing Statement Enquiry <STAT><account><Startdate><EndDate><Password>
                    else if ("S".Equals(msgParams.First().ToUpper()) || "STAT".Equals(msgParams.First().ToUpper()) || "STATEMENT".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseStatementEnquiryMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    else if ("M".Equals(msgParams.First().ToUpper()) || "MS".Equals(msgParams.First().ToUpper()) || "MINI".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseStatementEnquiryMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = LR|PAY ; LR<OfferID><Amount><Password>
                    else if ("LR".Equals(msgParams.First().ToUpper()) || "PAY".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseEarlyLoanRepaymentMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = R|RE|REG|REGISTER ; R<Email><Password>
                    else if ("R".Equals(msgParams.First().ToUpper()) || "RE".Equals(msgParams.First().ToUpper()) || "REG".Equals(msgParams.First().ToUpper()) || "REGISTER".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseRegisterMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = D|DE|DEREG|DEREGISTER ; D<Password>
                    else if ("D".Equals(msgParams.First().ToUpper()) || "DE".Equals(msgParams.First().ToUpper()) || "DEREG".Equals(msgParams.First().ToUpper()) || "DEREGISTER".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseDeRegisterMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = MLO ; MLO<Amount><Term><Interest><Password>
                    else if ("MLO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseMakeLendOfferMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = MBO ; MBO<Amount><Term><Interest><Password>
                    else if ("MBO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseMakeBorrowOfferMessage(OriginatingAddress, MessageDate, Body, msgParams);
                    }
                    //Message starts with = ALO ; ALO<OfferId><Amount><Password>
                    else if ("ALO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseAcceptLendOfferMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = ABO ; ABO<OfferId><Amount><Password>
                    else if ("ABO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseAcceptBorrowOfferMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = LO ; LO<Password>
                    else if ("LO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseLendOffersMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = BO ; BO<Password>
                    else if ("BO".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseBorrowOffersMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = C|CP ; CP<OldPassword><NewPassword><ConfirmPassword>
                    else if ("C".Equals(msgParams.First().ToUpper()) || "CP".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseChangePinMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    //Message starts with = W|WIT|WITHDRAW ; W<Amount><Password>
                    else if ("W".Equals(msgParams.First().ToUpper()) || "WITHDRAW".Equals(msgParams.First().ToUpper()))
                    {
                        fmessage = ParseWithdrawMessage(OriginatingAddress, MessageDate, Body, msgParams);

                    }
                    else
                    {
                        //this is not a fanikiwa message -- Do nothing; it will be removed from the sim
                        //inform sender with help commands
                        fmessage = ParseHelpMessage(OriginatingAddress, MessageDate, Body, msgParams);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMessage error = new ErrorMessage();
                //populate generic from abstract 
                error.SenderTelno = OriginatingAddress;
                error.MessageDate = MessageDate;
                error.FanikiwaMessageType = FanikiwaMessageType.ErrorMessage;
                error.Body = Body;
                error.Status = "NEW";
                error.Exception = ex;
                return error;
            }
            return fmessage;
        } 


 
        //implementations
        private static BalanceEnquiryMessage ParseBalanceEnquiryMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = BALSymbol*Pwd*<AccountID|AccountLabel>
            BalanceEnquiryMessage balEnq = new BalanceEnquiryMessage();
            //populate generic from abstract
            balEnq.SenderTelno = OriginatingAddress;
            balEnq.MessageDate = MessageDate;
            balEnq.FanikiwaMessageType = FanikiwaMessageType.BalanceEnquiryMessage;
            balEnq.Body = Body;
            balEnq.Status = "NEW";
            balEnq.AccountLabel = "C";

            //parse pwd: Not Optional
            balEnq.Pwd = msgParams.Skip(1).FirstOrDefault();

            //parse account: Optional
            if (msgParams.Count() > 2)
            {
                string accstr = msgParams.Skip(2).FirstOrDefault();
                int AccId;
                if (!int.TryParse(accstr, out AccId))
                {
                    balEnq.AccountLabel = accstr;
                }
                else
                {
                    balEnq.AccountLabel = null;
                    balEnq.AccountId = AccId;
                }
            }
            else
            {
                return balEnq;
            }

            return balEnq;
        }
        private static StatementEnquiryMessage ParseStatementEnquiryMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //nSyntax = STATSymbol*Pwd[*<AccountID|AccountLabel>*StartDate*EndDate]
            StatementEnquiryMessage st = new StatementEnquiryMessage();

            //populate generic from abstract
            st.SenderTelno = OriginatingAddress;
            st.MessageDate = MessageDate;
            st.FanikiwaMessageType = FanikiwaMessageType.StatementEnquiryMessage;
            st.Body = Body;
            st.Status = "NEW";
            st.AccountLabel = "C";
            DateTime startdate = DateTime.Today.AddMonths(-1 *  fCommon.Utility.Config.GetInt("STATEMENTMONTHS"));
            st.StartDate = startdate;
            DateTime enddate = DateTime.Today;
            st.EndDate = enddate;

            //parse pwd: Not Optional
            st.Pwd = msgParams.Skip(1).FirstOrDefault();

            //parse account: Optional
            if (msgParams.Count() > 2)
            {
                string accstr = msgParams.Skip(2).FirstOrDefault();
                int AccId;
                if (!int.TryParse(accstr, out AccId))
                {
                    st.AccountLabel = accstr;
                }
                else
                {
                    st.AccountLabel = null;
                    st.AccountId = AccId;
                }
            }
            else
            {
                return st;
            }


            //parse startdate: Optional
            
            if (msgParams.Count() > 3)
            {
                string sdate = msgParams.Skip(3).FirstOrDefault();
                DateTime.TryParse(sdate, out startdate);
            }
            else
            {
                return st;
            }

            //parse enddate: Optional
            
            if (msgParams.Count() > 4)
            {
                string edate = msgParams.Skip(4).FirstOrDefault();
                DateTime.TryParse(edate, out enddate);
            }
            else
            {
                return st;
            }
            return st;
        }

        private static MiniStatementEnquiryMessage ParseMiniStatementEnquiryMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = MS*Pwd*<AccountID|AccountLabel>
            MiniStatementEnquiryMessage st = new MiniStatementEnquiryMessage();

            //populate generic from abstract
            st.SenderTelno = OriginatingAddress;
            st.MessageDate = MessageDate;
            st.FanikiwaMessageType = FanikiwaMessageType.StatementEnquiryMessage;
            st.Body = Body;
            st.Status = "NEW";
            st.AccountLabel = "C";

            //parse pwd: Not Optional
            st.Pwd = msgParams.Skip(1).FirstOrDefault();

            //parse account: Optional
            if (msgParams.Count() > 2)
            {
                string accstr = msgParams.Skip(2).FirstOrDefault();
                int AccId;
                if (!int.TryParse(accstr, out AccId))
                {
                    st.AccountLabel = accstr;
                }
                else
                {
                    st.AccountLabel = null;
                    st.AccountId = AccId;
                }
            }
            else
            {
                return st;
            }

            
            return st;
        }

        private static EarlyLoanRepaymentMessage ParseEarlyLoanRepaymentMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            EarlyLoanRepaymentMessage lr = new EarlyLoanRepaymentMessage();
            //populate generic from abstract
            lr.SenderTelno = OriginatingAddress;
            lr.MessageDate = MessageDate;
            lr.FanikiwaMessageType = FanikiwaMessageType.EarlyLoanRepaymentMessage;
            lr.Body = Body;
            lr.Status = "NEW";

            //parse offerid: Not optional
            string offer = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(offer))
            {
                throw new ArgumentNullException("OfferID", "Offer id cannot be null in a Loan Repayment message. ");
            }

            int OfferId;
            if (!int.TryParse(offer, out OfferId))
            {
                throw new ArgumentNullException("OfferID", "Offer id is invalid. ");
            }
            lr.OfferId = OfferId;

            //parse amount: Not optional
            string amountstr = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(amountstr))
            {
                throw new ArgumentNullException("Amount", "Amount cannot be null in a Loan Repayment message. ");
            }

            decimal Amount;
            if (!decimal.TryParse(amountstr, out Amount))
            {
                throw new ArgumentNullException("Amount", "Amount is invalid. ");
            }
            lr.Amount = Amount;

            //parse pwd: Not optional
            string pwd = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Loan Repayment message. ");
            }
            lr.Pwd = pwd;
             
            return lr;
        }
        private static RegisterMessage ParseRegisterMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            RegisterMessage rm = new RegisterMessage();

            //populate generic from abstract 
            rm.SenderTelno = OriginatingAddress;
            rm.MessageDate = MessageDate;
            rm.FanikiwaMessageType = FanikiwaMessageType.RegisterMessage;
            rm.Body = Body;
            rm.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Register message. ");
            }
            rm.Pwd = pwd;

            //parse email: Not optional
            string email = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email", "Email cannot be null in a Register message. ");
            }
            rm.Email = email.ToLower();

            //parse nationalId: Not optional
            string nationalId = msgParams.Skip(3).FirstOrDefault();
            if (string.IsNullOrEmpty(nationalId))
            {
                throw new ArgumentNullException("NationalID", "NationalID cannot be null in a Register message. ");
            }
            rm.NationalID = nationalId;

            
            //parse Notification method: Optional
            //default Notification method is SMS for SMS requests
             rm.NotificationMethod = "SMS";
            if (msgParams.Count() > 4)
            {
                string param = msgParams.Skip(4).FirstOrDefault();

                if (!string.IsNullOrEmpty(param))
                {
                    rm.NotificationMethod = param;
                }
            }
            return rm;
        }
        private static DeRegisterMessage ParseDeRegisterMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            DeRegisterMessage drm = new DeRegisterMessage();

            //populate generic from abstract
            drm.SenderTelno = OriginatingAddress;
            drm.MessageDate = MessageDate;
            drm.FanikiwaMessageType = FanikiwaMessageType.DeRegisterMessage;
            drm.Body = Body;
            drm.Status = "NEW";

            //parse email: Not optional
            string email = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email", "Email cannot be null in a DeRegister message. ");
            }
            drm.Email = email.ToLower();

            //parse pwd: Not optional
            string pwd = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a DeRegister message. ");
            }
            drm.Pwd = pwd;

            return drm;
        }
        private static MakeLendOfferMessage ParseMakeLendOfferMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = MLO*Pwd*Amount*InterestRate*Term
            MakeLendOfferMessage mlo = new MakeLendOfferMessage();

            //populate generic from abstract
           mlo.SenderTelno = OriginatingAddress;
            mlo.MessageDate = MessageDate;
            mlo.FanikiwaMessageType = FanikiwaMessageType.MakeLendOfferMessage;
            mlo.Body = Body;
            mlo.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Make Lend Offer message. ");
            }
            mlo.Pwd = pwd;

            //parse Amount: Not optional
            string amountstr = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(amountstr))
            {
                throw new ArgumentNullException("Amount", "Amount cannot be null in a Make Lend Offer message. ");
            }
            decimal Amount;
            if (!decimal.TryParse(amountstr, out Amount))
            {
                throw new ArgumentNullException("Amount", "Amount is invalid. ");
            }
            mlo.Amount = Amount;

            //parse InterestRate: Not optional
            string InterestRatestr = msgParams.Skip(3).FirstOrDefault();
            if (string.IsNullOrEmpty(InterestRatestr))
            {
                throw new ArgumentNullException("InterestRate", "Interest Rate cannot be null in a Make Lend Offer message. ");
            }
            double InterestRate;
            if (!double.TryParse(InterestRatestr, out InterestRate))
            {
                throw new ArgumentNullException("InterestRate", "Interest Rate is invalid. ");
            }
            mlo.InterestRate = InterestRate;

            //parse Term: Not optional
            string termstr = msgParams.Skip(4).FirstOrDefault();
            if (string.IsNullOrEmpty(termstr))
            {
                throw new ArgumentNullException("Term", "Term cannot be null in a Make Lend Offer message. ");
            }
            int Term;
            if (!int.TryParse(termstr, out Term))
            {
                throw new ArgumentNullException("Term", "Term is invalid. ");
            }
            mlo.Term = Term;
            return mlo;
        }
        private static MakeBorrowOfferMessage ParseMakeBorrowOfferMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = MBO*Pwd*Amount*InterestRate*Term
            MakeBorrowOfferMessage mbo = new MakeBorrowOfferMessage();

            //populate generic from abstract
            mbo.SenderTelno = OriginatingAddress;
            mbo.MessageDate = MessageDate;
            mbo.FanikiwaMessageType = FanikiwaMessageType.MakeBorrowOfferMessage;
            mbo.Body = Body;
            mbo.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Make Borrow Offer message. ");
            }
            mbo.Pwd = pwd;

            //parse Amount: Not optional
            string amountstr = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(amountstr))
            {
                throw new ArgumentNullException("Amount", "Amount cannot be null in a Make Borrow Offer message. ");
            }
            decimal Amount;
            if (!decimal.TryParse(amountstr, out Amount))
            {
                throw new ArgumentNullException("Amount", "Amount is invalid. ");
            }
            mbo.Amount = Amount;

            //parse InterestRate: Not optional
            string InterestRatestr = msgParams.Skip(3).FirstOrDefault();
            if (string.IsNullOrEmpty(InterestRatestr))
            {
                throw new ArgumentNullException("Interest Rate", "Interest Rate cannot be null in a Make Borrow Offer message. ");
            }
            int InterestRate;
            if (!int.TryParse(InterestRatestr, out InterestRate))
            {
                throw new ArgumentNullException("Interest Rate", "Interest Rate is invalid. ");
            }
            mbo.InterestRate = InterestRate;

            //parse Term: Not optional
            string termstr = msgParams.Skip(4).FirstOrDefault();
            if (string.IsNullOrEmpty(termstr))
            {
                throw new ArgumentNullException("Term", "Term cannot be null in a Make Borrow Offer message. ");
            }
            int Term;
            if (!int.TryParse(termstr, out Term))
            {
                throw new ArgumentNullException("Term", "Term is invalid. ");
            }
            mbo.Term = Term;

            return mbo;
        }
        private static AcceptLendOfferMessage ParseAcceptLendOfferMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            AcceptLendOfferMessage alo = new AcceptLendOfferMessage();

            //populate generic from abstract
            alo.SenderTelno = OriginatingAddress;
            alo.MessageDate = MessageDate;
            alo.FanikiwaMessageType = FanikiwaMessageType.AcceptLendOfferMessage;
            alo.Body = Body;
            alo.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in Accept Lend Offer message. ");
            }
            alo.Pwd = pwd;

            //parse offerid: Not optional
            string offer = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(offer))
            {
                throw new ArgumentNullException("OfferID", "Offer id cannot be null in Accept Lend Offer message. ");
            }

            int OfferId;
            if (!int.TryParse(offer, out OfferId))
            {
                throw new ArgumentNullException("OfferID", "Offer id is invalid. ");
            }
            alo.OfferId = OfferId;
            
            return alo;
        }
        private static AcceptBorrowOfferMessage ParseAcceptBorrowOfferMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = ABO*Pwd*OfferId
            AcceptBorrowOfferMessage abo = new AcceptBorrowOfferMessage();

            //populate generic from abstract
            abo.SenderTelno = OriginatingAddress;
            abo.MessageDate = MessageDate;
            abo.FanikiwaMessageType = FanikiwaMessageType.AcceptBorrowOfferMessage;
            abo.Body = Body;
            abo.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in Accept Borrow Offer message. ");
            }
            abo.Pwd = pwd;

            //parse offerid: Not optional
            string offer = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(offer))
            {
                throw new ArgumentNullException("OfferID", "Offer id cannot be null in a Accept Borrow message. ");
            }

            int OfferId;
            if (!int.TryParse(offer, out OfferId))
            {
                throw new ArgumentNullException("OfferID", "Offer id is invalid. ");
            }
            abo.OfferId = OfferId;

            return abo;
        }
        private static LendOffersMessage ParseLendOffersMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            LendOffersMessage lo = new LendOffersMessage();

            //populate generic from abstract
            lo.SenderTelno = OriginatingAddress;
            lo.MessageDate = MessageDate;
            lo.FanikiwaMessageType = FanikiwaMessageType.LendOffersMessage;
            lo.Body = Body;
            lo.Status = "NEW";
             
            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in Lend Offers message. ");
            }
            lo.Pwd = pwd;

            return lo;
        }
         private static BorrowOffersMessage ParseBorrowOffersMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            BorrowOffersMessage bo = new BorrowOffersMessage();

            //populate generic from abstract
            bo.SenderTelno = OriginatingAddress;
            bo.MessageDate = MessageDate;
            bo.FanikiwaMessageType = FanikiwaMessageType.BorrowOffersMessage;
            bo.Body = Body;
            bo.Status = "NEW";
              
            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Borrow Offers message. ");
            }
            bo.Pwd = pwd;

            return bo;
        }
        private static ChangePinMessage ParseChangePinMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            ChangePinMessage cp = new ChangePinMessage();

            //populate generic from abstract
           cp.SenderTelno = OriginatingAddress;
            cp.MessageDate = MessageDate;
            cp.FanikiwaMessageType = FanikiwaMessageType.ChangePINMessage;
            cp.Body = Body;
            cp.Status = "NEW";
            
            //parse oldpwd: Not optional
            string oldpwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(oldpwd))
            {
                throw new ArgumentNullException("Password", "Old Password cannot be null in Change Pin message. ");
            }
            cp.OldPassword = oldpwd;

            //parse newpwd: Not optional
            string newpwd = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(newpwd))
            {
                throw new ArgumentNullException("Password", "New Password cannot be null in Change Pin message. ");
            }
            cp.NewPassword = newpwd;

            //parse confirmpwd: Not optional
            string confirmpwd = msgParams.Skip(3).FirstOrDefault();
            if (string.IsNullOrEmpty(confirmpwd))
            {
                throw new ArgumentNullException("Password", "Confirm Password cannot be null in Change Pin message. ");
            }
            cp.ConfirmPassword = confirmpwd;

            return cp;
        }
        private static WithdrawMessage ParseWithdrawMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax = WithdrawSymbol*Pwd*Amount
            WithdrawMessage mo = new WithdrawMessage();

            //populate generic from abstract
            mo.SenderTelno = OriginatingAddress;
            mo.MessageDate = MessageDate;
            mo.FanikiwaMessageType = FanikiwaMessageType.WithdrawMessage;
            mo.Body = Body;
            mo.Status = "NEW";

            //parse pwd: Not optional
            string pwd = msgParams.Skip(1).FirstOrDefault();
            if (string.IsNullOrEmpty(pwd))
            {
                throw new ArgumentNullException("Password", "Password cannot be null in a Withdraw message. ");
            }
            mo.Pwd = pwd;

            //parse Amount: Not optional
            string amountstr = msgParams.Skip(2).FirstOrDefault();
            if (string.IsNullOrEmpty(amountstr))
            {
                throw new ArgumentNullException("Amount", "Amount cannot be null in a Withdraw message. ");
            }
            decimal Amount;
            if (!decimal.TryParse(amountstr, out Amount))
            {
                throw new ArgumentNullException("Amount", "Amount is invalid. ");
            }
            
            return mo;
        }
        private static HelpMessage ParseHelpMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            HelpMessage help = new HelpMessage();
            //populate generic from abstract
            //help.MemberId = GetMemberFromPhone(OriginatingAddress);//get member id from telno
            help.SenderTelno = OriginatingAddress;
            help.MessageDate = MessageDate;
            help.FanikiwaMessageType = FanikiwaMessageType.HelpMessage;
            help.Body = Body;
            help.Status = "NEW";

            string helpParam = msgParams.Skip(1).FirstOrDefault();
            if (!string.IsNullOrEmpty(helpParam)) help.HelpCommand = helpParam;
            return help;
        }

        //private static string GetConfigurationValue(string key)
        //{
        //    return Config.GetInt[key];
        //}


        private static MpesaDepositMessage ParseMpesaMessage(string OriginatingAddress, DateTime MessageDate, string Body)
        {
            MpesaDepositMessage mo = new MpesaDepositMessage();

            /*
              FX12UB729 Confirmed.  
on 31/10/14 at 7:49 PM  
Ksh220.00 received from FRANCIS MURAYA 254715413144.  
Account Number 10  
New Utility balance is Ksh420.00 
             */
            //populate generic from abstract
            // mo.MemberId = GetMemberFromPhone(OriginatingAddress);//get member id from telno

            string[] lines = Body.ToUpper().Trim().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string MpesaRef = lines[0].Split(new char[] { ' ' }).First();
            string date = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).First();
            string time = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(3).First();
            string ampm = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            string money = lines[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).First().Split(new string[] { "KSH" }, StringSplitOptions.RemoveEmptyEntries).First();
            string telno = lines[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            string accno = lines[3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            string bal = lines[4].Split(new char[] { ' ' }).Last().Split(new string[] { "KSH" }, StringSplitOptions.RemoveEmptyEntries).First();

            
            mo.CustomerTelno = telno.Split(new char[] { '.' }).First();
            mo.SenderTelno = mo.CustomerTelno;//OriginatingAddress;
            mo.AccountId = accno;
            mo.Amount = decimal.Parse(money);
            mo.Bal = decimal.Parse(bal);
            mo.Mpesaref = MpesaRef;
            DateTime dd = Convert.ToDateTime(date);
            string d = dd.ToString("dd/MM/yy") + " " + time + ":00 " + ampm;
            mo.SentDate = DateTime.ParseExact(d, "dd/MM/yy h:mm:ss tt", CultureInfo.InvariantCulture);
            mo.MessageDate = MessageDate;
            mo.FanikiwaMessageType = FanikiwaMessageType.MpesaDepositMessage;
            mo.Body = Body;
            mo.Status = "NEW";


            return mo;
        }



        private static MailingGroupMessage ParseMailingGroupMessage(string OriginatingAddress, DateTime MessageDate, string Body, List<string> msgParams)
        {
            //Syntax =   Syntax = MGSymbol*Pwd*GroupName*[Members]
            MailingGroupMessage mg = new MailingGroupMessage();
            //populate generic from abstract
            mg.SenderTelno = OriginatingAddress;
            mg.MessageDate = MessageDate;
            mg.FanikiwaMessageType = FanikiwaMessageType.MailingGroupMessage;
            mg.Body = Body;
            mg.Status = "NEW";


            //parse pwd: Not Optional
            mg.Pwd = msgParams.Skip(1).FirstOrDefault();

            //parse Groupname: Not Optional
            mg.GroupName= msgParams.Skip(2).FirstOrDefault();

            //parse members: Optional
            if (msgParams.Count() > 3)
            {
                mg.Members = msgParams.Skip(3).ToList();
            }
            else
            {
                return mg;
            }

            return mg;
        }
        
    }
    #endregion     
}
