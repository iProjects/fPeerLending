using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fMessagingSystem.Framework;
using fMessagingSystem.Entities;

namespace MessageProcessor
{
    public class SMSDBProcessorComponent
    {
        public void ProcessSMSMessages()
        {
            FanikiwaDBMessageDAC smsDac = new FanikiwaDBMessageDAC();
            List<fMessagingSystem.Entities.FanikiwaDBMessage> _sms = new List<fMessagingSystem.Entities.FanikiwaDBMessage>();

            //get all new messages from db
            var _smsquery = from sms in smsDac.Select()
                            where sms.Status == "New".ToUpper()
                            select sms;
            _sms = _smsquery.ToList();

            if (EnableLog)
                log.Info(string.Format("Start processing [{0}] messages ", _sms.Count.ToString()));

            foreach (var message in _sms)
            {
                if (EnableLog)
                    log.Info(string.Format("Start processing  message: [{0}] ", message.Body.ToString()));
                ProcessDBMessage(message);
            }
        }

        public FanikiwaMessage ConvertToFanikiwaMessage(FanikiwaDBMessage message)
        {
            /*
                1) register / deregister
                2) make offer
                3) accept offer
                4) enquire -  account balance, statement , offers, etc
                5) change pin
                6) deposit
                7) pay loan
                8) withdraw
                9) help 
             */

            FanikiwaMessageType fmType;
            if (!Enum.TryParse<FanikiwaMessageType>(message.MessageType.ToString(), out fmType))
            {
                throw new ArgumentNullException("FanikiwaMessageType", string.Format("The value [{0}] can not parse into a FanikiwaMessageType", message.MessageType.ToString()));
            }

            switch (fmType)
            {
                /*
                 * we achieve this by a simple switch on one Thread.
                 * LATER THIS COULD BE CHANGED INTO A MULTITHREADED APP TO INCREASE THROUGHPUT
                 */
                //Registration
                case FanikiwaMessageType.RegisterMessage:
                    {
                        RegisterMessage r = new RegisterMessage();
                        r.Body = message.Body;
                        r.FanikiwaMessageType = FanikiwaMessageType.RegisterMessage;
                        r.MemberId = message.MemberId;
                        r.NationalID = message.NationalID;
                        r.Pwd = message.Pwd;
                        r.Email = message.Email;
                        r.NotificationMethod = message.NotificationMethod;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.DeRegisterMessage:
                    {
                        DeRegisterMessage r = new DeRegisterMessage();
                        r.Body = message.Body;
                        r.Email = message.Email;
                        r.Pwd = message.Pwd;
                        r.FanikiwaMessageType = FanikiwaMessageType.DeRegisterMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                //Offers
                case FanikiwaMessageType.AcceptLendOfferMessage:
                    {
                        AcceptLendOfferMessage r = new AcceptLendOfferMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.AcceptLendOfferMessage;
                        r.MemberId = message.MemberId;
                        r.OfferId = message.OfferId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.AcceptBorrowOfferMessage:
                    {
                        AcceptBorrowOfferMessage r = new AcceptBorrowOfferMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.AcceptBorrowOfferMessage;
                        r.MemberId = message.MemberId;
                        r.OfferId = message.OfferId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.MakeLendOfferMessage:
                    {
                        MakeLendOfferMessage r = new MakeLendOfferMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.MakeLendOfferMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.InterestRate = message.MO_Interest;
                        r.Term = message.MO_Term;
                        return r;
                    }
                case FanikiwaMessageType.MakeBorrowOfferMessage:
                    {
                        MakeBorrowOfferMessage r = new MakeBorrowOfferMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.MakeBorrowOfferMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.InterestRate = message.MO_Interest;
                        r.Term = message.MO_Term;
                        return r;
                    }
                case FanikiwaMessageType.LendOffersMessage:
                    {
                        LendOffersMessage r = new LendOffersMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.FanikiwaMessageType = FanikiwaMessageType.LendOffersMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.BorrowOffersMessage:
                    {
                        BorrowOffersMessage r = new BorrowOffersMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.FanikiwaMessageType = FanikiwaMessageType.BorrowOffersMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                //Financial Transactions
                case FanikiwaMessageType.MpesaDepositMessage:
                    {
                        MpesaDepositMessage r = new MpesaDepositMessage();
                        r.Body = message.Body;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.MpesaDepositMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.AccountId = message.AccountId;
                        r.Bal = message.MpesaBal;
                        r.CustomerTelno = message.CustomerTelno;
                        r.Mpesaref = message.MpesaRef;
                        r.SentDate = message.MpesaSentDate;
                        return r;
                    }
                case FanikiwaMessageType.WithdrawMessage:
                    {
                        WithdrawMessage r = new WithdrawMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.WithdrawMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.EarlyLoanRepaymentMessage:
                    {
                        EarlyLoanRepaymentMessage r = new EarlyLoanRepaymentMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.Amount = message.Amount;
                        r.FanikiwaMessageType = FanikiwaMessageType.EarlyLoanRepaymentMessage;
                        r.MemberId = message.MemberId;
                        r.OfferId = message.OfferId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.Amount = message.Amount;
                        return r;
                    }
                //Enquiry messages
                case FanikiwaMessageType.BalanceEnquiryMessage:
                    {
                        BalanceEnquiryMessage r = new BalanceEnquiryMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.FanikiwaMessageType = FanikiwaMessageType.BalanceEnquiryMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.AccountId = message.AccountId;
                        r.AccountLabel = message.BE_AccLabel;
                        return r;
                    }
                case FanikiwaMessageType.StatementEnquiryMessage:
                    {
                        StatementEnquiryMessage r = new StatementEnquiryMessage();
                        r.Body = message.Body;
                        r.Pwd = message.Pwd;
                        r.FanikiwaMessageType = FanikiwaMessageType.StatementEnquiryMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.AccountId = message.AccountId;
                        r.AccountLabel = message.BE_AccLabel;
                        return r;
                    }
                //Profile
                case FanikiwaMessageType.ChangePINMessage:
                    {
                        ChangePinMessage r = new ChangePinMessage();
                        r.Body = message.Body;
                        r.OldPassword = message.Pwd;
                        r.NewPassword = message.CP_NewPassword;
                        r.ConfirmPassword = message.CP_ConfirmPassword;
                        r.FanikiwaMessageType = FanikiwaMessageType.ChangePINMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        return r;
                    }
                case FanikiwaMessageType.HelpMessage:
                    {
                        HelpMessage r = new HelpMessage();
                        r.Body = message.Body;
                        r.FanikiwaMessageType = FanikiwaMessageType.HelpMessage;
                        r.MemberId = message.MemberId;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.HelpCommand = message.HM_Param;
                        return r;
                    }
                case FanikiwaMessageType.ErrorMessage:
                    {
                        ErrorMessage r = new ErrorMessage();
                        r.Body = message.Body;
                        r.FanikiwaMessageType = FanikiwaMessageType.ErrorMessage;
                        r.SenderTelno = message.SenderTelno;
                        r.Status = message.Status;
                        r.MessageDate = message.MessageDate;
                        r.Error_Message = message.Error;
                        return r;
                    }
                default:
                    break;
            }
            return null;
        }


        public string ProcessDBMessage(FanikiwaDBMessage DBfmessage)
        {
            string msg = ProcessFanikiwaMessage(ConvertToFanikiwaMessage(DBfmessage));

            //update message status in db
            FanikiwaDBMessageDAC mDac = new FanikiwaDBMessageDAC();
            DBfmessage.Status = "Processed".ToUpper();
            mDac.UpdateById(DBfmessage);

            return msg;
        }


        public void Inform(InformMessage message)
        {
            //logging to db
            InformDAC infoDac = new InformDAC();
            infoDac.Create(message);

            //invoke inform service
            //MessagerComponent messager = new MessagerComponent(comm);
            //messager.SendSMS(message);

            if (EnableLog)
                log.Info(string.Format("Sent message: [{0}] to phone[{1}] ", message.Body.ToString(), message.AddressTo));
        }
    }
}

