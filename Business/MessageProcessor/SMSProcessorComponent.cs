using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

//using log4net;
using System.Configuration;

using fanikiwaGL.Entities;
using fanikiwaGL.Business;
using fPeerLending.Entities;
using fPeerLending.Business;

using fPeerLending.Framework;
using fCommon.Extension_Methods;
using fPeerLending.Framework.ExceptionTypes;
using fCommon.Utility;

namespace fPeerLending.MessageProcessor
{
    public class SMSProcessorComponent
    {

        Dictionary<string, FCommand> Commands = new Dictionary<string, FCommand>();

        int MaxListItems = 5;//default max records; can be changed by putting a[MaxListItems] entry in the config
        int OFFEREXPIRYTIMESPANINMONTHS = 1; //offers to expire in one month by default; can be changed by putting a[OFFEREXPIRYTIMESPANINMONTHS] entry in the config
        public SMSProcessorComponent()
        {
            InitializeCommands();
        }
         
        public string ProcessFanikiwaMessage(FanikiwaMessage message)
        {
            string info = "Unknown";
            //Get Processed
            
                info = GetProcessedMessage(message);
         
            return info;
        }

        #region Private Helpers
        private void InitializeCommands()
        {

            Commands.Add("R", new FCommand("Register", "Register command enables registration in Fanikiwa.\nSyntax = RegSymbol*Pwd*Email*NationalID\nWhere RegSymbol = R|REG|REGISTER, Email = your Email, NationalID = your National ID, Pwd = your password"));
            Commands.Add("B", new FCommand("Balance", "Balance equiry on various accounts\nSyntax = BALSymbol*Pwd*<AccountID|AccountLabel>\nWhere BALSymbol = B|BAL|BALANCE \nAccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
            Commands.Add("S", new FCommand("Statement", "Statement equiry on various accounts\nSyntax = STATSymbol*Pwd*<AccountID|AccountLabel>*StartDate*EndDate\nWhere STATSymbol = S|STAT|STATEMENT \nAccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
            Commands.Add("LR", new FCommand("Loan Repayment", "Early Loan Repaymenton on various loans\nSyntax = LOANREPAYSymbol*Pwd*LoanID*Amount\nWhere LOANREPAYSymbol = LR|PAY \nLoanID = Id sent to you after Accepting an Offer\nPwd = your password"));
            Commands.Add("M", new FCommand("Mini Statement", "Last 5 transactions\nSyntax = MS*Pwd*<AccountID|AccountLabel>\nWhere AccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
            Commands.Add("MLO", new FCommand("Make Lend Offer", "Make a Lend Offer\nSyntax = MLO*Pwd*Amount*InterestRate*Term\nWhere Amount = your Amount to Lend, InterestRate = your Interest Rate, Term = Number of Months to Repay the Loan, Pwd = your password"));
            Commands.Add("MBO", new FCommand("Make Borrow Offer", "Make a Borrow Offer\nSyntax = MBO*Pwd*Amount*InterestRate*Term\nWhere Amount = your Amount to Borrow, InterestRate = your Interest Rate, Term = Number of Months to Repay the Loan, Pwd = your password"));
            Commands.Add("ALO", new FCommand("Accept Lend Offer", "Accept a Lend Offer and get a Loan\nSyntax = ALO*Pwd*OfferId\nWhere OfferId = your Lend Offer Id, Pwd = your password"));
            Commands.Add("ABO", new FCommand("Accept Borrow Offer", "Accept a Borrow Offer and Make an Investment\nSyntax = ABO*Pwd*OfferId\nWhere OfferId = your Borrow Offer Id, Pwd = your password"));
            Commands.Add("LO", new FCommand("Lend Offers", "Get your Lend Offers\nSyntax = LO*Pwd\nWhere Pwd = your password"));
            Commands.Add("BO", new FCommand("Borrow Offers", "Get your Borrow Offers\nSyntax = BO*Pwd\nWhere Pwd = your password"));
            Commands.Add("C", new FCommand("Change Password", "Change Your Password\nSyntax = ChangePasswordSymbol*OldPwd*NewPwd*ConfirmPwd\nWhere ChangePasswordSymbol = C|CP\nOldPwd = your Old password, NewPwd = your New password, ConfirmPwd = your Confirm password"));
            Commands.Add("W", new FCommand("Withdraw", "Withdraw Cash from your Current Account\nSyntax = WithdrawSymbol*Pwd*Amount\nWhere WithdrawSymbol = W|WITHDRAW \nPwd = your Amount to withdraw, Pwd = your password"));
            Commands.Add("D", new FCommand("DeRegister", "DeRegister\nSyntax = DeRegisterSymbol*Pwd*Email\nWhere DeRegisterSymbol = D|DE|DREG|DEREGISTER\nEmail = your Email during registration, Pwd = your password  during registration"));
            Commands.Add("G", new FCommand("Mailing Group", "Create a mailing group\nSyntax = MGSymbol*Pwd*GroupName*[Members]\nWhere MGSymbol = G|GRP|MG\nGroupName = group name. must be unique in the system, Pwd = your password during registration\nMembers=comma separated telephone|email|groupname"));
            Commands.Add("AG", new FCommand("Add Member", "Add member to Mailing Group\nSyntax = MGSymbol*Pwd*GroupName*[Members]\nWhere MGSymbol = AG|AGRP|AMG\nGroupName = group name. must be unique in the system, Pwd = your password during registration\nMembers=comma separated telephone|email|groupname"));


            List<string> cmds = new List<string>();
            cmds = (from c in Commands
                    select c.Key).ToList();

            Commands.Add("H", new FCommand("Help", "H|Help - Help command\nUsage: H*<Command>" + "\nCommands are [" + string.Join(",", cmds) + "]\nE.g. Send H*R for help on registration "));
        }
        private string GetProcessedMessage(FanikiwaMessage message)
        {
            string help = "Send Help";
            if (message is HelpMessage) return ProcessHelpMessage((HelpMessage)message);
            if (message is BalanceEnquiryMessage) return ProcessBalanceEnquiryMessage((BalanceEnquiryMessage)message);
            if (message is StatementEnquiryMessage) return ProcessStatementEnquiryMessage((StatementEnquiryMessage)message);
            if (message is MiniStatementEnquiryMessage) return ProcessMiniStatementEnquiryMessage((MiniStatementEnquiryMessage)message);

            if (message is MpesaDepositMessage) return ProcessMpesaDepositMessage((MpesaDepositMessage)message);
            if (message is RegisterMessage) return ProcessRegisterMessage((RegisterMessage)message);
            if (message is DeRegisterMessage) return ProcessDeRegisterMessage((DeRegisterMessage)message);
            if (message is MakeLendOfferMessage) return ProcessMakeLendOfferMessage((MakeLendOfferMessage)message);
            if (message is MakeBorrowOfferMessage) return ProcessMakeBorrowOfferMessage((MakeBorrowOfferMessage)message);
            if (message is AcceptLendOfferMessage) return ProcessAcceptLendOfferMessage((AcceptLendOfferMessage)message);
            if (message is AcceptBorrowOfferMessage) return ProcessAcceptBorrowOfferMessage((AcceptBorrowOfferMessage)message);
            if (message is LendOffersMessage) return ProcessListLendOffersMessage((LendOffersMessage)message);
            if (message is BorrowOffersMessage) return ProcessListBorrowOffersMessage((BorrowOffersMessage)message);
            if (message is ChangePinMessage) return ProcessChangePinMessage((ChangePinMessage)message);
            if (message is WithdrawMessage) return ProcessWithdrawMessage((WithdrawMessage)message);

            if (message is ErrorMessage) return ProcessErrorMessage((ErrorMessage)message);  
            return help;
        }
        private Decimal GetAvailableBalance(int _AccountId)
        {
            decimal BookBal;
            decimal Available = GetAvailableBalance(_AccountId, out  BookBal);
            return Available;
        }
        private Decimal GetAvailableBalance(int _AccountId, out decimal BookBal)
        {
            StaticTransactionsComponent sPost = new StaticTransactionsComponent();
            Account acc = sPost.GetAccount(_AccountId);
            BookBal = acc.BookBalance;
            return sPost.GetAvailableBalance(acc);
        }
        private int GetAccountIDFromLabel(Member member, string Acclabel)
        {
            string label = Acclabel.ToUpper();
            if ("C".Equals(label) || "CUR".Equals(label))
            {
                return member.CurrentAccountId;
            }
            
            if ("I".Equals(label) || "INV".Equals(label))
            {
                return member.InvestmentAccountId;
            }

            if ("L".Equals(label) || "LOAN".Equals(label))
            {
                return member.LoanAccountId;
            }
            

          return member.CurrentAccountId;
            
        }
        private int GetAccountIDFromMessage(FanikiwaMessage message)
        {
            int AccId=0;
            RegistrationComponent rc = new RegistrationComponent();
            if (!string.IsNullOrEmpty(message.SenderTelno))
            {
                Member member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }

                if (message is BalanceEnquiryMessage)
                {
                    BalanceEnquiryMessage msg = ((BalanceEnquiryMessage)message);
                    if (!string.IsNullOrEmpty(msg.AccountLabel))
                        return GetAccountIDFromLabel(member, msg.AccountLabel);

                    AccId = msg.AccountId;
                }
                if (message is MiniStatementEnquiryMessage)
                {
                    MiniStatementEnquiryMessage msg = ((MiniStatementEnquiryMessage)message);
                    if (!string.IsNullOrEmpty(msg.AccountLabel))
                        return GetAccountIDFromLabel(member, msg.AccountLabel);

                    AccId = msg.AccountId;
                }
                if (message is StatementEnquiryMessage)
                {
                    StatementEnquiryMessage msg = ((StatementEnquiryMessage)message);
                    if (!string.IsNullOrEmpty(msg.AccountLabel))
                        return GetAccountIDFromLabel(member, msg.AccountLabel);

                    AccId = msg.AccountId;
                }
            }
            return AccId;
        }
        #endregion

        #region Message Proccessors

        #region Mpesa Deposit
        private string ProcessMpesaDepositMessage(MpesaDepositMessage message)
        {
            DepositComponent dc = new DepositComponent();
            string narr = message.CustomerTelno + " deposited on " + message.MessageDate.ToString();
           List<Transaction> txns =  dc.MpesaDeposit(message.AccountId,message.Amount,narr,  message.Mpesaref);
           TransactionFactory.Post(txns);
            return "Deposit Successful";
        }
        #endregion
        #region Help
        private string ProcessHelpMessage(HelpMessage message)
        {
            string hlp = "";
            if (!string.IsNullOrEmpty(message.HelpCommand))
            {
                hlp = ProcessHelpMessageCommand(message.HelpCommand);
                return hlp;
            }
            else
            {
                hlp = ProcessHelpMessageCommand("ALL");
                return hlp;
            }
        }
         string GetHelpMessage(string Key)
        {
            FCommand cmd = Commands[Key];
            return cmd.Usage;
        }

         private string ProcessHelpMessageCommand(string cmd)
        {
            string hlp = GetHelpMessage("H");

            switch (cmd.ToUpper())
            {
                case "H":
                case "HELP":
                    hlp = GetHelpMessage("H");
                    break;
                case "B":
                case "BAL":
                case "BALANCE":
                    hlp = GetHelpMessage("B");
                    break;
                case "S":
                case "STAT":
                case "STATEMENT":
                    hlp = GetHelpMessage("S");
                    break;
                case "M":
                case "MS":
                case "MINI":
                    hlp = GetHelpMessage("M");
                    break;
                case "LR":
                case "PAY":
                    hlp = GetHelpMessage("LR");
                    break;
                case "R":
                case "RE":
                case "REG":
                case "REGISTER":
                    hlp = GetHelpMessage("R");
                    break;
                case "D":
                case "DE":
                case "DEREG":
                case "DEREGISTER":
                    hlp = GetHelpMessage("D");
                    break;
                case "MLO":
                    hlp = GetHelpMessage("MLO");
                    break;
                case "MBO":
                    hlp = GetHelpMessage("MBO");
                    break;
                case "ALO":
                    hlp = GetHelpMessage("ALO");
                    break;
                case "ABO":
                    hlp = GetHelpMessage("ABO");
                    break;
                case "LO":
                    hlp = GetHelpMessage("LO");
                    break;
                case "BO":
                    hlp = GetHelpMessage("BO");
                    break;
                case "C":
                case "CP":
                    hlp = GetHelpMessage("C");
                    break;
                case "W":
                case "WITHDRAW":
                    hlp = GetHelpMessage("W");
                    break;
                case "ALL":
                    return hlp;
                default:
                    return hlp;
            }
            return hlp;
        }
        #endregion
        #region Error
        private string ProcessErrorMessage(ErrorMessage message)
        {
            string error = "An error occurred while processing your request. Please contact Administrator on " + Config.GetString("FANIKIWAADMIN");
            if (message.Exception is ArgumentException || message.Exception is ArgumentNullException)
            {
                error = message.Exception.Message+ "\nPlease send Help to " + Config.GetString("FANIKIWAMESSAGESTELNO");
            }

            return error;
        }
        #endregion
        #region Balance enquiry
        private string ProcessBalanceEnquiryMessage(BalanceEnquiryMessage message)
        {

            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not Authenticated or authorized. please check your credentials and status";

            int AccId = GetAccountIDFromMessage(message);
            StaticTransactionsComponent sc = new StaticTransactionsComponent();
            if (!sc.AccountExists(AccId)) throw new UserInterfaceException("Account [" + AccId + "] does not exist",
                new ArgumentException("Account [" + AccId + "] does not exist"));
            Account account = sc.GetAccount(AccId);

            return string.Format("Balance for[{3}]: {0}\nBook balance = {1}\nAvailable balance = {2}",
                
                account.AccountName,
                account.BookBalance,
                account.ClearedBalance - account.Limit,
                account.AccountID);
        }
        #endregion
        #region Statement Enquiry
        private string ProcessStatementEnquiryMessage(StatementEnquiryMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            int AccId = GetAccountIDFromMessage(message);
            StaticTransactionsComponent sc = new StaticTransactionsComponent();
            if (!sc.AccountExists(AccId)) throw new UserInterfaceException("Account [" + AccId + "] does not exist",new ArgumentException("Account [" + AccId + "] does not exist"));

            return GetStatement(AccId, message.StartDate, message.EndDate);
        }

        private string ProcessMiniStatementEnquiryMessage(MiniStatementEnquiryMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            int AccId = GetAccountIDFromMessage(message);
            StaticTransactionsComponent sc = new StaticTransactionsComponent();
            if (!sc.AccountExists(AccId)) throw new UserInterfaceException("Account [" + AccId + "] does not exist",new ArgumentException("Account [" + AccId + "] does not exist"));

            return GetStatement(AccId, Config.GetInt("MaxListItems", MaxListItems));
        }

        private string GetStatement(int accId, DateTime sdate, DateTime enddate) //for statement
        {
            TransactionsComponent tc = new TransactionsComponent();
            return tc.GetStatement(accId, sdate, enddate);
        }
        private string GetStatement(int accId, int Take) //for ministatement
        {
            TransactionsComponent tc = new TransactionsComponent();
            return tc.GetStatement(accId, Take);

        }

        #endregion
        #region Early Loan Repayment
        private string ProcessEarlyLoanRepaymentMessage(EarlyLoanRepaymentMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            int AccId = 0;
            Member member;
            decimal bal;
            string Accdes = string.Empty;

            if (message.OfferId != 0 && !string.IsNullOrEmpty(message.SenderTelno))
            {
                RegistrationComponent rc = new RegistrationComponent();
                member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    string errMsg = string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno);
                    throw new ArgumentNullException("SenderTelno", errMsg);
                }  
            } 
            bal = GetAvailableBalance(AccId);
            return string.Format("Your {0} balance is {1}", Accdes, bal);
        }
        #endregion
        #region Register
        private Member SMSToMember(RegisterMessage message)
        {
            Member member = new Member();
            member.Email = message.Email.ToLower();
            member.Pwd = message.Pwd;
            member.NationalID = message.NationalID;
            member.Status = "A";
            member.DateActivated = DateTime.Today;
            member.DateJoined = DateTime.Today;
            member.Telephone = message.SenderTelno;
            member.InformBy = "SMS";
            member.Pwd = message.Pwd;
            //member.DateOfBirth = DateTime.MinValue;

            char[] delimiters = new char[] { '@' };
            string[] emailParams = member.Email.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string surname = emailParams[0];
            member.Surname = surname;
            return member;
        }

        private string ProcessRegisterMessage(RegisterMessage message)
        {
            //check command parameters R*Email*NationalID*pwd
            if (string.IsNullOrEmpty(message.Email))
                throw new ArgumentNullException("Email", "Email is required for registration");
            if (string.IsNullOrEmpty(message.NationalID))
                throw new ArgumentNullException("NationalID", "NationalID is required for registration");
            if (string.IsNullOrEmpty(message.Pwd))
                throw new ArgumentNullException("Pwd", "Password is required for registration");


            //check email
            string emailpattern = "^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
            if (!Regex.IsMatch(message.Email, emailpattern))
            {
                throw new ArgumentNullException("Email", string.Format("The Email [{0}]  provided is invalid. Please enter a valid Email. ", message.Email));
            }

            //check pwd
            string pwdpattern = @"^(?=\S{6,}$)(?=.*?\d)(?=.*?[a-zA-Z]).+$";
            if (!Regex.IsMatch(message.Pwd, pwdpattern))
            {
                throw new ArgumentNullException("Password", string.Format("The Password provided is invalid. Valid Password must have at least 6 characters, and a mix of letters and numbers with no spaces. ", message.Pwd));
            }

            //register now
            RegistrationComponent rc = new RegistrationComponent();
            if (!rc.IsRegistered(message.Email, message.SenderTelno, message.NationalID)
                && !rc.IsEmailRegistered(message.Email)
                && !rc.IsPhoneRegistered(message.SenderTelno)
                && !rc.IsNationalIDRegistered(message.NationalID))
            {

                Member regmember = rc.Register(SMSToMember(message));
                if (regmember != null)
                {
                    return string.Format("Successfully Registered. Details\nMember Id {0}, Current Account Id {1}, Loan Account Id {2}, Investment Account Id {3}",
                       regmember.MemberId.ToString(),
                       regmember.CurrentAccountId.ToString(),
                       regmember.LoanAccountId.ToString(),
                       regmember.InvestmentAccountId.ToString());
                }
                else return "Member registration was not successful";
            }
            else
            {
                return "Member is already registered";
            }
        }

        #endregion
        #region DeRegister
        private string ProcessDeRegisterMessage(DeRegisterMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            string msg = string.Empty;

            if (!string.IsNullOrEmpty(message.Pwd) && !string.IsNullOrEmpty(message.SenderTelno))
            {
                RegistrationComponent rc = new RegistrationComponent();

                Member member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }

            }
            return msg;
        }
        #endregion
        #region Make Lend Offer
        private string ProcessMakeLendOfferMessage(MakeLendOfferMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

               RegistrationComponent rc = new RegistrationComponent();
                MakeOfferComponent mo = new MakeOfferComponent();

                Member member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }

                OfferModel offer = new OfferModel();
                offer.Amount = message.Amount;
                offer.Term = message.Term;
                offer.Interest = message.InterestRate;
                offer.OfferType = "L";
                offer.MemberId = member.MemberId;
                offer.Status = OfferStatus.Open.ToString();
                offer.CreatedDate = DateTime.Today;
                offer.ExpiryDate = offer.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS",OFFEREXPIRYTIMESPANINMONTHS));
                offer.PublicOffer = "B"; // SMS offers are public by default
                offer.Description = "Lend offer";

                mo.MakeLendOffer(offer);
                return "Offer successfully made";
        }
        #endregion
        #region Make Borrow Offer
        private string ProcessMakeBorrowOfferMessage(MakeBorrowOfferMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            string msg = string.Empty;
            RegistrationComponent rc = new RegistrationComponent();
            MakeOfferComponent mo = new MakeOfferComponent();

            Member member = rc.SelectMemberByPhone(message.SenderTelno);
            if (member == null)
            {
                throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
            }

            OfferModel offer = new OfferModel();
            offer.Amount = message.Amount;
            offer.Term = message.Term;
            offer.Interest = message.InterestRate;
            offer.OfferType = "B";
            offer.MemberId = member.MemberId;
            offer.Status = OfferStatus.Open.ToString();
            offer.CreatedDate = DateTime.Today;
            offer.ExpiryDate = offer.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS", OFFEREXPIRYTIMESPANINMONTHS));
            offer.PublicOffer = "B"; // SMS offers are public by default
            offer.Description = "Borrow offer";

            mo.MakeBorrowOffer(offer);
            return "Borrow offer successfully done";
        }
        #endregion
        #region Accept Lend Offer
        private string ProcessAcceptLendOfferMessage(AcceptLendOfferMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

                RegistrationComponent rc = new RegistrationComponent();
                AcceptOfferComponent ac = new AcceptOfferComponent();
                ListOffersComponent lc = new ListOffersComponent();

            //this is a lend offer so the offerer is the lender and the acceptor the borrower
                Member borrower = rc.GetMemberByPhone(message.SenderTelno);
                if (borrower == null)
                {
                    throw new ArgumentException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }

                Offer aLendOffer = lc.GetOfferById(message.OfferId);
                if (aLendOffer == null)
                {
                    throw new ArgumentException("OfferId", string.Format("Offer [{0}] does not exist in Fanikiwa. ", message.OfferId));
                }

                ac.AcceptLendOffer(borrower, aLendOffer);

                return ("Successful");
        }



        #endregion
        #region Accept Borrow Offer
        private string ProcessAcceptBorrowOfferMessage(AcceptBorrowOfferMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

                RegistrationComponent rc = new RegistrationComponent();
                AcceptOfferComponent ac = new AcceptOfferComponent();
                ListOffersComponent lc = new ListOffersComponent();

               //this is a borrow offer so the lender is the acceptor
                Member lender = rc.SelectMemberByPhone(message.SenderTelno);
                if (lender == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }

                Offer aBorrowOffer = lc.GetOfferById(message.OfferId);
                if (aBorrowOffer == null)
                {
                    throw new ArgumentNullException("OfferId", string.Format("Offer [{0}] does not exist in Fanikiwa. ", message.OfferId));
                }

                ac.AcceptBorrowOffer(lender, aBorrowOffer);

                return ("Successful");
        }
        #endregion
        #region Lend Offers
        private string ProcessListLendOffersMessage(LendOffersMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";

            RegistrationComponent rc = new RegistrationComponent();
            ListOffersComponent lc = new ListOffersComponent();

            Member member = rc.SelectMemberByPhone(message.SenderTelno);
            if (member == null)
            {
                throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
            }

            var q = from c in lc.ListLendOffers(member).OrderByDescending(r=>r.Amount).Take(Config.GetInt("MaxListItems", MaxListItems))
                    select c.Id.ToString() + " Amt=" + c.Amount + " Term=" + c.Term + " Interest=" + c.Interest;

            if (q.Count() > 0)
            {
                return q.Aggregate((i, j) => i + "\n" + j);
            }
            else
                return "No offers found";
        }
        #endregion
        #region Borrow Offers
        private string ProcessListBorrowOffersMessage(BorrowOffersMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not authenticated";
            RegistrationComponent rc = new RegistrationComponent();
            ListOffersComponent lc = new ListOffersComponent();

            Member member = rc.SelectMemberByPhone(message.SenderTelno);
            if (member == null)
            {
                throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
            }

            var q = from c in lc.ListBorrowOffers(member).OrderByDescending(r => r.Amount).Take(Config.GetInt("MaxListItems", MaxListItems))
                    select c.Id.ToString() + " Amt=" + c.Amount + " Term=" + c.Term + " Interest=" + c.Interest;
            
            if (q.Count() > 0)
            {
                return q.Aggregate((i, j) => i + "\n" + j);
            }
            else
                return "No offers found";

        }

        #endregion
        #region Change Pin
        private string ProcessChangePinMessage(ChangePinMessage message)
        {
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.OldPassword))
                return "Not authenticated";

            string msg = string.Empty;
            if (!string.IsNullOrEmpty(message.OldPassword) && !string.IsNullOrEmpty(message.SenderTelno))
            {
                RegistrationComponent rc = new RegistrationComponent();

                if (message.NewPassword != message.ConfirmPassword)
                {
                    throw new ArgumentNullException("NewPassword", string.Format("NewPassword does not match ConfirmPassword ", message.SenderTelno));
                }

                Member member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }


            }
            return msg;
        }
        #endregion
        #region Withdraw
        private string ProcessWithdrawMessage(WithdrawMessage message)
        {
            string msg = "Successful";
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not Authenticated or authorized. please check your credentials and status";

                RegistrationComponent rc = new RegistrationComponent();
                WithdrawComponent wc = new WithdrawComponent();

                Member member = rc.SelectMemberByPhone(message.SenderTelno);
                if (member == null)
                {
                    throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
                }
                //try
                //{
                //    //wc.Withdraw(member.CurrentAccountId,message.Amount, "",  "");
                //}
                //catch (WithdrawalException we)
                //{
                //    foreach (var sPost in we.SimulatePostStatus)
                //    {
                //        foreach (var er in sPost.Errors) msg += "\n"+er.Message;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    msg = ex.Message;
                //}
            return msg;
        }
        #endregion

        private void AddMemberToGroup(MailingGroupMember member, MailingGroup group)
        {
            member.GroupId = group.GroupId;
            MailingGroupsComponent mgc = new MailingGroupsComponent();
            mgc.CreateMailingGroupMember(member);
        }
        private string ProcessAddMemberToGroupMailingMessage(MailingGroupMessage message)
        {

            string msg = "Successful";
            if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
                return "Not Authenticated or authorized. please check your credentials and status";

            RegistrationComponent rc = new RegistrationComponent();
            MailingGroupsComponent mgc = new MailingGroupsComponent();

            Member member = rc.SelectMemberByPhone(message.SenderTelno);
            if (member == null)
            {
                throw new ArgumentNullException("SenderTelno", string.Format("Sender Telno [{0}] is not registered. ", message.SenderTelno));
            }
            try
            {
                MailingGroup mailgroup = mgc.GetOrCreateMailingGroup(member.MemberId, message.Parent, message.GroupName);

                foreach (var s in message.Members)
                {
                    MailingGroupMember mgm = new MailingGroupMember();
                    mgm.GroupId = mailgroup.GroupId;
                    if (s.IsEmail()) mgm.Email = s;
                    if (s.IsNumeric()) mgm.Telno = s;
                    //upto here it can only be a group.  see if one exists

                    //if (mgc.GroupExists(s, member.MemberId))
                    //{
                    //    //check that the group is not the top-level group to avoid circular reference
                    //    //MailingGroup grp = 
                    //    //mgm.MailingGroup = s;
                    //}

                    //create now; only if any of the three has something
                    if (!string.IsNullOrEmpty(mgm.Email)
                        || !string.IsNullOrEmpty(mgm.Telno)
                        || !string.IsNullOrEmpty(mgm.MailingGroup))
                        mgc.CreateMailingGroupMember(mgm);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        private string ProcessCreateGroupMailingMessage(MailingGroupMessage message)
        {
            return "Not Implemented";
        }

        #endregion


        #region Authenticate

        private bool AuthenticateAndAuthorize(string tel, string pwd)
        {
            if (string.IsNullOrEmpty(tel))
                throw  new ArgumentNullException("Telno", "Telno required");

            if (string.IsNullOrEmpty(pwd))
                throw new ArgumentNullException("Password", "Password required");

            RegistrationComponent rc = new RegistrationComponent();
            return rc.Authenticated(tel, pwd) && rc.Authorized(tel, pwd);

        }

        #endregion


        class FCommand
        {
            public string Name { get; set; }
            public string Usage { get; set; }
            public FCommand(string name, string usage)
            {
                Name = name; Usage = usage;
            }
        }

    }
}
