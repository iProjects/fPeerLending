using System;
using System.Collections.Generic;

namespace fPeerLending.MessageProcessor
{
    #region Message definitions
    public abstract class FanikiwaMessage
    {
        public int MemberId { get; set; }
        public string SenderTelno { get; set; }
        public DateTime MessageDate { get; set; }
        public FanikiwaMessageType FanikiwaMessageType { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }

    }
    public enum FanikiwaMessageType
    {
        //Financial Transactions
        MpesaDepositMessage = 1, //Identified by source being MPESA 
        WithdrawMessage = 2,//Identified by message structure e.g. W*<Amount><Password> - means try withdraw the amount
        EarlyLoanRepaymentMessage = 3,//Identified by message structure e.g. PAY*<LoanId>*<Amount><Password> - means try repay the identified loan by the amount
        BalanceEnquiryMessage = 4, //Identified by message structure e.g. B*[C*/<AccountId>]<Password> - means return current account balance
        StatementEnquiryMessage = 5,//Identified by message structure e.g. S*[C*/<AccountId>]<Password> - means return current account mini statement
        //Registration
        RegisterMessage = 6,//Identified by message structure e.g. R*<Email><Password> - means try register with the email and password supplied
        DeRegisterMessage = 7,//Identified by message structure e.g. D*<Password> - means try deregister the user
        //offers
        AcceptLendOfferMessage = 8,//Identified by message structure e.g. ALO*<OfferId><Amount><Password> - means try accept the identified lend offer by the amount
        AcceptBorrowOfferMessage = 9,//Identified by message structure e.g. ABO*<OfferId><Amount><Password> - means try accept the identified borrow offer by the amount
        MakeLendOfferMessage = 10,//Identified by message structure e.g. MLO*<Amount><Term><Interest><Password> - means try create a lend offer by the supplied params
        MakeBorrowOfferMessage = 11,//Identified by message structure e.g. MBO*<Amount><Term><Interest><Password> - means try create a borrow offer by the supplied params
        LendOffersMessage = 12,//Identified by message structure e.g. LO*<Amount><Password> - means try list lend offers for the identified member
        BorrowOffersMessage = 13,//Identified by message structure e.g. BO*<Password> - means try create a borrow offers for the identified member
        //Profile   
        ChangePINMessage = 14,//Identified by message structure e.g. CP*<OldPin><NewPin><ConfirmPin> - means try Change the identified oldpin by the newpin
        HelpMessage = 15,
        ErrorMessage = 16,
        MiniStatementEnquiryMessage = 17,
        MailingGroupMessage = 18
    }
    public class HelpMessage : FanikiwaMessage
    {
        public string HelpCommand { get; set; }
        public HelpMessage()
        {
        }
        public HelpMessage(string command)
        {
            HelpCommand = command;
        }
    }
    public class ErrorMessage : FanikiwaMessage
    {
        public string Error_Message { get; set; }
        public string FriendlyMessage { get; set; }
        public Exception Exception{get;set;}
        public ErrorMessage()
        {
        }
        public ErrorMessage(string message)
        {
            Error_Message = message;
        }
    }
    public class BalanceEnquiryMessage : FanikiwaMessage
    {
        public int AccountId { get; set; }
        public string AccountLabel { get; set; }
        public string Pwd { get; set; }

        public BalanceEnquiryMessage()
        {

        }
        public BalanceEnquiryMessage(int acc, string pwd)
        {
            this.AccountId = acc;
            this.Pwd = pwd;
        }
        public BalanceEnquiryMessage(string accLabel, string pwd)
        {
            AccountLabel = accLabel;
            this.Pwd = pwd;
        }
    }
    public class StatementEnquiryMessage : FanikiwaMessage
    {
        public int AccountId { get; set; }
        public string AccountLabel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Pwd { get; set; }

        public StatementEnquiryMessage()
        {

        }
        public StatementEnquiryMessage(int acc, string pwd)
        {
            this.AccountId = acc;
            this.Pwd = pwd;
        }
    }
    public class MiniStatementEnquiryMessage : FanikiwaMessage
    {
        public int AccountId { get; set; }
        public string AccountLabel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Pwd { get; set; }

        public MiniStatementEnquiryMessage()
        {

        }
        public MiniStatementEnquiryMessage(int acc, string pwd)
        {
            this.AccountId = acc;
            this.Pwd = pwd;
        }
    }

    public class MpesaIPNMessage
    {


        long id;

        private String mpesaIPNMessageID; //	This is the IPN notification unique number 	: Eg. 	100 
        private String orig; //	This will be source of the notification. 	: Eg. 	MPESA 

        private String dest; //	This will be same as your business terminal Msisdn	: Eg. 	254722123456 
        private String tstamp; //	This is the timestamp for when the IPN notification was received from MPESA. 	: Eg. 	2011-04-26 12:47:31.0 
        private String text; //	This is the full text message as received from MPESA.	: Eg. 	This is will be the full SMS message. 
        private String user; //	If you provide us a username and password to access your URL this parameter will contain the username	: Eg. 	Username for Developer URL 
        private String pass; //	If you provide us a username and password to access your URL this parameter will contain the password. 	: Eg. 	Password for Developer URL 
        private String mpesa_code; //	The MPESA transaction code. 	: Eg. 	BI55EQ862 
        private String mpesa_acc; //	The Account as entered by the subscriber 	: Eg. 	TEST 
        private String mpesa_msisdn; //	The mobile number of the subscriber that has sent the funds 	: Eg. 	254722123456
        private String mpesa_trx_date; //	The transaction date 	: Eg. 	26/4/11 
        private String mpesa_trx_time; //	The transaction time 	: Eg. 	1:03 PM 
        private Double mpesa_amt; //	The transaction amount 	: Eg. 	50.0 
        private String mpesa_sender; //	The name of the sender (subscriber). 	: Eg. 	ALFRED

        private String status;


        public long getId()
        {
            return id;
        }
        public void setId(long id)
        {
            this.id = id;
        }
        public String getOrig()
        {
            return orig;
        }
        public void setOrig(String orig)
        {
            this.orig = orig;
        }
        public String getDest()
        {
            return dest;
        }
        public void setDest(String dest)
        {
            this.dest = dest;
        }
        public String getTstamp()
        {
            return tstamp;
        }
        public void setTstamp(String tstamp)
        {
            this.tstamp = tstamp;
        }
        public String getText()
        {
            return text;
        }
        public void setText(String text)
        {
            this.text = text;
        }
        public String getUser()
        {
            return user;
        }
        public void setUser(String user)
        {
            this.user = user;
        }
        public String getPass()
        {
            return pass;
        }
        public void setPass(String pass)
        {
            this.pass = pass;
        }
        public String getMpesa_code()
        {
            return mpesa_code;
        }
        public void setMpesa_code(String mpesa_code)
        {
            this.mpesa_code = mpesa_code;
        }
        public String getMpesa_acc()
        {
            return mpesa_acc;
        }
        public void setMpesa_acc(String mpesa_acc)
        {
            this.mpesa_acc = mpesa_acc;
        }
        public String getMpesa_msisdn()
        {
            return mpesa_msisdn;
        }
        public void setMpesa_msisdn(String mpesa_msisdn)
        {
            this.mpesa_msisdn = mpesa_msisdn;
        }
        public String getMpesa_trx_date()
        {
            return mpesa_trx_date;
        }
        public void setMpesa_trx_date(String mpesa_trx_date)
        {
            this.mpesa_trx_date = mpesa_trx_date;
        }
        public String getMpesa_trx_time()
        {
            return mpesa_trx_time;
        }
        public void setMpesa_trx_time(String mpesa_trx_time)
        {
            this.mpesa_trx_time = mpesa_trx_time;
        }
        public Double getMpesa_amt()
        {
            return mpesa_amt;
        }
        public void setMpesa_amt(Double mpesa_amt)
        {
            this.mpesa_amt = mpesa_amt;
        }
        public String getMpesa_sender()
        {
            return mpesa_sender;
        }
        public void setMpesa_sender(String mpesa_sender)
        {
            this.mpesa_sender = mpesa_sender;
        }
        public String getStatus()
        {
            return status;
        }
        public void setStatus(String status)
        {
            this.status = status;
        }
        public String getMpesaIPNMessageID()
        {
            return mpesaIPNMessageID;
        }
        public void setMpesaIPNMessageID(String mpesaIPNMessageID)
        {
            this.mpesaIPNMessageID = mpesaIPNMessageID;
        }
    }

    public class MpesaDepositMessage : FanikiwaMessage
    {
        public string CustomerTelno { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Mpesaref { get; set; }
        public DateTime SentDate { get; set; }
        public decimal Bal { get; set; }
        public int id { get; set; }
        public string mpesaIPNMessageID { get; set; } //	This is the IPN notification unique number 	: Eg. 	100
        public string orig { get; set; } //	This will be source of the notification. 	: Eg. 	MPESA  
        public string dest { get; set; } //	This will be same as your business terminal Msisdn	: Eg. 	254722123456 
        public string tstamp { get; set; } //	This is the timestamp for when the IPN notification was received from MPESA. 	: Eg. 	2011-04-26 12:47:31.0 
        public string text { get; set; } //	This is the full text message as received from MPESA.	: Eg. 	This is will be the full SMS message. 
        public string user { get; set; } //	If you provide us a username and password to access your URL this parameter will contain the username	: Eg. 	Username for Developer URL 
        public string pass { get; set; } //	If you provide us a username and password to access your URL this parameter will contain the password. 	: Eg. 	Password for Developer URL 
        public string mpesa_code { get; set; } //	The MPESA transaction code. 	: Eg. 	BI55EQ862 
        public string mpesa_acc { get; set; } //	The Account as entered by the subscriber 	: Eg. 	TEST 
        public string mpesa_msisdn { get; set; } //	The mobile number of the subscriber that has sent the funds 	: Eg. 	254722123456
        public string mpesa_trx_date { get; set; } //	The transaction date 	: Eg. 	26/4/11 
        public string mpesa_trx_time { get; set; } //	The transaction time 	: Eg. 	1:03 PM 
        public decimal mpesa_amt { get; set; } //	The transaction amount 	: Eg. 	50.0 
        public string mpesa_sender { get; set; } //	The name of the sender (subscriber). 	: Eg. 	ALFRED

        public MpesaDepositMessage()
        {

        }
        public MpesaDepositMessage(string acc, decimal amount, string customerTelno,
          int id,
          string mpesaIPNMessageID,
          string orig,
          string dest,
          string tstamp,
          string text,
          string user,
          string pass,
          string mpesa_code,
          string mpesa_acc,
          string mpesa_msisdn,
          string mpesa_trx_date,
          string mpesa_trx_time,
          decimal mpesa_amt,
          string mpesa_sender)
        {
            this.Amount = amount;
            this.AccountId = acc;
            this.CustomerTelno = customerTelno;

            this.id = id;
            this.mpesaIPNMessageID = mpesaIPNMessageID;
            this.orig = orig;
            this.dest = dest;
            this.tstamp = tstamp;
            this.text = text;
            this.user = user;
            this.pass = pass;
            this.mpesa_code = mpesa_code;
            this.mpesa_msisdn = mpesa_msisdn;
            this.mpesa_trx_date = mpesa_trx_date;
            this.mpesa_trx_time = mpesa_trx_time;
            this.mpesa_amt = mpesa_amt;
            this.mpesa_sender = mpesa_sender;
        }
    }
    
    public class EarlyLoanRepaymentMessage : FanikiwaMessage
    {

        public int OfferId { get; set; }
        public decimal Amount { get; set; }
        public string Pwd { get; set; }

        public EarlyLoanRepaymentMessage()
        {

        }
        public EarlyLoanRepaymentMessage(int offer, decimal amount, string pwd)
        {
            this.Amount = amount;
            this.OfferId = offer;
            this.Pwd = pwd;
        }
    }
    public class RegisterMessage : FanikiwaMessage
    {
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string NationalID { get; set; }
        public string NotificationMethod { get; set; }

        public RegisterMessage()
        {

        }
        public RegisterMessage(string email, string pwd, string nationalid, string notificationMethod)
        {
            this.Email = email;
            this.Pwd = pwd;
            this.NationalID = nationalid;
            this.NotificationMethod = notificationMethod;
        }
    }
    public class DeRegisterMessage : FanikiwaMessage
    {
        public string Email { get; set; }
        public string Pwd { get; set; }

        public DeRegisterMessage()
        {

        }
        public DeRegisterMessage(string pwd, string email)
        {
            this.Email = email;
            this.Pwd = pwd;
        }
    }
    public class MakeLendOfferMessage : FanikiwaMessage
    {
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
        public string Pwd { get; set; }

        public MakeLendOfferMessage()
        {

        }
        public MakeLendOfferMessage(decimal amount, int term, double interestrate, string pwd)
        {
            this.Amount = amount;
            this.Term = term;
            this.InterestRate = interestrate;
            this.Pwd = pwd;
        }
    }
    public class MakeBorrowOfferMessage : FanikiwaMessage
    {
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
        public string Pwd { get; set; }

        public MakeBorrowOfferMessage()
        {

        }
        public MakeBorrowOfferMessage(decimal amount, int term, double interestrate, string pwd)
        {
            this.Amount = amount;
            this.Term = term;
            this.InterestRate = interestrate;
            this.Pwd = pwd;
        }
    }
    public class AcceptLendOfferMessage : FanikiwaMessage
    {
        public int OfferId { get; set; }
        public decimal Amount { get; set; }
        public string Pwd { get; set; }

        public AcceptLendOfferMessage()
        {

        }
        public AcceptLendOfferMessage(int offerid, decimal amount, string pwd)
        {
            this.OfferId = offerid;
            this.Amount = amount;
            this.Pwd = pwd;
        }
    }
    public class AcceptBorrowOfferMessage : FanikiwaMessage
    {
        public int OfferId { get; set; } 
        public decimal Amount { get; set; }        
        public string Pwd { get; set; }

        public AcceptBorrowOfferMessage()
        {

        }
        public AcceptBorrowOfferMessage(int offerid, decimal amount, string pwd)
        {
            this.OfferId = offerid;
            this.Amount = amount; 
            this.Pwd = pwd;
        }
    }
    public class LendOffersMessage : FanikiwaMessage
    { 
        public string Pwd { get; set; }

        public LendOffersMessage()
        {

        }
        public LendOffersMessage(int offerid, decimal amount, string pwd)
        { 
            this.Pwd = pwd;
        }
    }
    public class BorrowOffersMessage : FanikiwaMessage
    { 
        public string Pwd { get; set; }

        public BorrowOffersMessage()
        {

        }
        public BorrowOffersMessage(string pwd)
        { 
            this.Pwd = pwd;
        }
    }
    public class ChangePinMessage : FanikiwaMessage
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public ChangePinMessage()
        {

        }
        public ChangePinMessage(string oldpassword, string newpassword, string confirmpassword)
        {
            this.OldPassword = oldpassword;
            this.NewPassword = newpassword;
            this.ConfirmPassword = confirmpassword; 
        }
    }
    public class WithdrawMessage : FanikiwaMessage
    {
        public decimal Amount { get; set; } 
        public string Pwd { get; set; }

        public WithdrawMessage()
        {

        }
        public WithdrawMessage(decimal amount, string pwd)
        {
            this.Amount = amount; 
            this.Pwd = pwd;
        }
    }

    public class MailingGroupMessage : FanikiwaMessage
    {
        public string Parent { get { return parent; } set { parent = value; } }
        public string GroupName { get; set; }
        public string Pwd { get; set; }
        public List<string> Members { get; set; }

        private string parent = "ROOT"; //Default
        public MailingGroupMessage()
        {

        }
        public MailingGroupMessage(string pwd, string groupname)
        {
            this.GroupName = groupname;
            this.Pwd = pwd;
        }
    }

    #endregion
}
