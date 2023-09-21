using CommonLib.Business;
using CommonLib.Business.GeneralLedgerServiceProxy;
using CommonLib.Business.MessagingServiceProxy;
using CommonLib.Data;
using CommonLib.Entities;
using fanikiwaDiary.Business;
using fanikiwaDiary.Entities;
using fPeerLending.Business;
using fPeerLending.Data;
using fPeerLending.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace fPeerLending.Test
{
    [TestClass]
    public class PeerLendingUnitTest
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        //(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog errorLog = log4net.LogManager.GetLogger("ErrorLogger");
        private static readonly log4net.ILog infoLog = log4net.LogManager.GetLogger("InfoLogger");

        [TestMethod]
        public void TestRegister()
        {
            try
            { 
                //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false); 
                 
                //RegistrationComponent rc = new RegistrationComponent();

                //Member member = new Member();
                //member.Email = "joan@softwareproviders.co.ke";
                //member.Surname = "joan";
                //member.OtherNames = "Wanjiku";
                //member.Status = "N";
                //member.Gender = "F";
                //member.Telephone = "+254721120552";
                //member.DateActivated = null;
                //member.DateJoined = DateTime.Today.Date;
                //member.DateOfBirth = DateTime.Today.Date;
                //member.InformBy = "EMAIL";
                ////rc.Register(member);

                //Member member1 = new Member();
                //member1.Email = "grace@softwareproviders.co.ke";
                //member1.Surname = "grace";
                //member1.OtherNames = "Mutio";
                //member1.Status = "N";
                //member1.Gender = "F";
                //member1.Telephone = "+254716400317";
                //member1.DateActivated = null;
                //member1.DateJoined = DateTime.Today.Date;
                //member1.DateOfBirth = DateTime.Today.Date;
                //member1.InformBy = "EMAIL";
                ////rc.Register(member1);

                //Member member2 = new Member();
                //member2.Email = "caro@softwareproviders.co.ke";
                //member2.Surname = "caro";
                //member2.OtherNames = "Wanjiku";
                //member2.Status = "N";
                //member2.Gender = "F";
                //member2.Telephone = "+254704215521";
                //member2.DateActivated = null;
                //member2.DateJoined = DateTime.Today.Date;
                //member2.DateOfBirth = DateTime.Today.Date;
                //member2.InformBy = "EMAIL";
                ////rc.Register(member2);

                //Member member3 = new Member();
                //member3.Email = "kevinmk30@gmail.com";
                //member3.Surname = "matin";
                //member3.OtherNames = "kevin mk";
                //member3.Status = "N";
                //member3.Gender = "M";
                //member3.Telephone = "+254717769329";
                //member3.DateActivated = null;
                //member3.DateJoined = DateTime.Today.Date;
                //member3.DateOfBirth = DateTime.Today.Date;
                //member3.InformBy = "EMAIL";
                ////rc.Register(member3);
            }
            catch (Exception ex)
            {
                // TODO: Handle your exceptions here. Remove any try-catch blocks if you
                //       are not handling any exceptions. 
                //throw ex;
                //bool rethrow = false;
                //rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
                //if (rethrow)
                //{
                //    CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
                //    throw new FaultException<CustomExpMsg>(customMsg,
                //        new FaultReason(customMsg.ErrorMsg),
                //        new FaultCode("TestSaveReceivedSMStoDB"));
                //} 
            }
        }

        [TestMethod]
        public void TestMakeOffer()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //MakeOfferComponent mk = new MakeOfferComponent();
            //MemberDAC mc = new MemberDAC();

            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer = new Offer();
            //offer.MemberId = member.MemberId;
            //offer.Description = "Lend Offer 35000";
            //offer.Interest = 3;
            //offer.Term = 6;
            //offer.Amount = 35000;
            //offer.OfferType = "L";
            //offer.PublicOffer = "B";
            //offer.Status = OfferStatus.Open.ToString();
            //offer.CreatedDate = DateTime.Today;
            //offer.ExpiryDate = offer.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer);

            //Member member1 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer1 = new Offer();
            //offer1.MemberId = member1.MemberId;
            //offer1.Description = "Borrow Offer";
            //offer1.Interest = 3;
            //offer1.Term = 3;
            //offer1.Amount = 15000;
            //offer1.OfferType = "B";
            //offer1.PublicOffer = "B";
            //offer1.Status = OfferStatus.Open.ToString();
            //offer1.CreatedDate = DateTime.Today;
            //offer1.ExpiryDate = offer1.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer1);

            //Member member2 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer2 = new Offer();
            //offer2.MemberId = member2.MemberId;
            //offer2.Description = "Lend Offer";
            //offer2.Interest = 3;
            //offer2.Term = 5;
            //offer2.Amount = 25000;
            //offer2.OfferType = "L";
            //offer2.PublicOffer = "B";
            //offer2.Status = OfferStatus.Open.ToString();
            //offer2.CreatedDate = DateTime.Today;
            //offer2.ExpiryDate = offer2.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer2);

            //Member member3 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer3 = new Offer();
            //offer3.MemberId = member3.MemberId;
            //offer3.Description = "Borrow Offer";
            //offer3.Interest = 4;
            //offer3.Term = 5;
            //offer3.Amount = 20000;
            //offer3.OfferType = "B";
            //offer3.PublicOffer = "B";
            //offer3.Status = OfferStatus.Open.ToString();
            //offer3.CreatedDate = DateTime.Today;
            //offer3.ExpiryDate = offer3.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer3);

            //Member member4 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer4 = new Offer();
            //offer4.MemberId = member4.MemberId;
            //offer4.Description = "Lend Offer";
            //offer4.Interest = 6;
            //offer4.Term = 8;
            //offer4.Amount = 40000;
            //offer4.OfferType = "L";
            //offer4.PublicOffer = "B";
            //offer4.Status = OfferStatus.Open.ToString();
            //offer4.CreatedDate = DateTime.Today;
            //offer4.ExpiryDate = offer.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer4);

            //Member member5 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer5 = new Offer();
            //offer5.MemberId = member5.MemberId;
            //offer5.Description = "Borrow Offer";
            //offer5.Interest = 5;
            //offer5.Term = 7;
            //offer5.Amount = 16000;
            //offer5.OfferType = "B";
            //offer5.PublicOffer = "B";
            //offer5.Status = OfferStatus.Open.ToString();
            //offer5.CreatedDate = DateTime.Today;
            //offer5.ExpiryDate = offer5.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer5);

            //Member member6 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer6 = new Offer();
            //offer6.MemberId = member6.MemberId;
            //offer6.Description = "Lend Offer";
            //offer6.Interest = 4;
            //offer6.Term = 7;
            //offer6.Amount = 56000;
            //offer6.OfferType = "L";
            //offer6.PublicOffer = "B";
            //offer6.Status = OfferStatus.Open.ToString();
            //offer6.CreatedDate = DateTime.Today;
            //offer6.ExpiryDate = offer.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer6);

            //Member member7 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer7 = new Offer();
            //offer7.MemberId = member7.MemberId;
            //offer7.Description = "Borrow Offer";
            //offer7.Interest = 2;
            //offer7.Term = 4;
            //offer7.Amount = 23000;
            //offer7.OfferType = "B";
            //offer7.PublicOffer = "B";
            //offer7.Status = OfferStatus.Open.ToString();
            //offer7.CreatedDate = DateTime.Today;
            //offer7.ExpiryDate = offer7.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer7);

            //Member member8 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer8 = new Offer();
            //offer8.MemberId = member8.MemberId;
            //offer8.Description = "Borrow Offer";
            //offer8.Interest = 2;
            //offer8.Term = 4;
            //offer8.Amount = 23000;
            //offer8.OfferType = "B";
            //offer8.PublicOffer = "B";
            //offer8.Status = OfferStatus.Open.ToString();
            //offer8.CreatedDate = DateTime.Today;
            //offer8.ExpiryDate = offer8.CreatedDate.AddMonths(1);
            //mk.MakeOffer(offer8);
        }
        [TestMethod]
        public void TestCreateCustomer()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            //StaticTransactionsServiceClient sPostingClient = new StaticTransactionsServiceClient();

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Customer customer = new Customer();
            //customer.Name = "kevin";
            //customer.Email = "kevin@softwareproviders.co.ke";
            //customer.Telephone = "+254717769329";
            //customer.CreatedDate = DateTime.Now.Date;

            //sPostingClient.CreateCustomer(customer);
        }
        [TestMethod]
        public void TestAcceptLendOffer()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer = oc.SelectById(1);
            //ac.AcceptLendOffer(member, offer);

            //Member member1 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer1 = oc.SelectById(2);
            //ac.AcceptLendOffer(member1, offer1);

            //Member member2 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer2 = oc.SelectById(3);
            //ac.AcceptLendOffer(member2, offer2);

            //Member member3 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer3 = oc.SelectById(4);
            //ac.AcceptLendOffer(member3, offer3);
        }


        [TestMethod]
        public void TestAcceptBorrowOffer()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer = oc.SelectById(5);
            //ac.AcceptBorrowOffer(member, offer);

            //Member member1 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer1 = oc.SelectById(6);
            //ac.AcceptBorrowOffer(member1, offer1);

            //Member member2 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer2 = oc.SelectById(7);
            //ac.AcceptBorrowOffer(member2, offer2);

            //Member member3 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer3 = oc.SelectById(8);
            //ac.AcceptBorrowOffer(member3, offer3);
        }

        [TestMethod]
        public void TestAcceptPartialBorrowOffer()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer = oc.SelectById(1);
            //offer.Amount = 1000;
            //ac.AcceptPartialBorrowOffer(member, offer);

            //Member member3 = mc.SelectByEmail("kevinmk30@gmail.com");
            //Offer offer3 = oc.SelectById(10);
            //offer3.Amount = 1000;
            //ac.AcceptPartialBorrowOffer(member3, offer3);
        }
        [TestMethod]
        public void TestEstablishLoan()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Member borrower = mc.SelectByEmail("kevin@softwareproviders.co.ke");
            //Offer offer = oc.SelectById(26);
            //Member lender = mc.SelectById(offer.MemberId);

            //Loan loan = ac.EstablishLoan(lender, borrower, offer);
            //Assert.AreEqual(loan.Term, offer.Term, "loan.Term and offer.Term Not Equal!");
        }


        [TestMethod]
        public void TestLogLoanRepayment()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();
            //LoanDAC lDAC = new LoanDAC();

            //Member borrower = mc.SelectByEmail("kevin@softwareproviders.co.ke");
            //Offer offer = oc.SelectById(32);
            //Member lender = mc.SelectById(offer.MemberId);
            //Loan loan = lDAC.SelectById(53);

            //ac.LogLoanRepayment(lender, borrower, loan);

        }


        [TestMethod]
        public void TestPostLoanTransaction()
        {

            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();
            //LoanDAC lDAC = new LoanDAC();

            //Member borrower = mc.SelectByEmail("grace@softwareproviders.co.ke");
            //Offer offer = oc.SelectById(28);
            //Member lender = mc.SelectById(offer.MemberId);
            //Loan loan = lDAC.SelectById(53);

            //ac.PostLoanTransaction(lender, borrower, loan);
        }



        [TestMethod]
        public void TestInformLenderAndBorrower()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //AcceptOfferComponent ac = new AcceptOfferComponent();
            //MemberDAC mc = new MemberDAC();
            //OfferDAC oc = new OfferDAC();

            //Member borrower = mc.SelectByEmail("caro@softwareproviders.co.ke");
            //Offer offer = oc.SelectById(45);
            //Member lender = mc.SelectById(offer.MemberId);

            //ac.InformLenderAndBorrower(lender, borrower, offer);
        }

        [TestMethod]
        public void TestGetNextCustomerNo()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //RegistrationComponent rc = new RegistrationComponent();
            //DepositComponent dc = new DepositComponent();
            //AccountsDetsModel model = new AccountsDetsModel();

            //string _CustomerNo = rc.NextCustomerNo();

            //Debug.WriteLine("Last Customer No: " + _CustomerNo + Environment.NewLine);

        }

        [TestMethod]
        public void TestInformUser()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //MemberDAC mc = new MemberDAC();
            //Member member = mc.SelectByEmail("kevin@softwareproviders.co.ke");

            //RegistrationComponent rc = new RegistrationComponent();
            //rc.InformRegisteredUser(member);
        }

        [TestMethod]
        public void TestInformaccountholder()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            DepositComponent dc = new DepositComponent();
            MemberDAC mc = new MemberDAC();

            Member member = mc.SelectByPhone("254717769329");

            dc.Informaccountholder(member, 50900);
        }


        [TestMethod]
        public void TestDepositViaMpesa()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //DepositComponent dc = new DepositComponent();
            //MemberDAC mc = new MemberDAC();
            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //if (member != null)
            //{
            //    dc.DepositViaMpesa(5000, member);
            //}
        }

        [TestMethod]
        public void TestWithdrawViaMpesa()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //WithdrawComponent wc = new WithdrawComponent();
            //MemberDAC mc = new MemberDAC();
            //Member member = mc.SelectByEmail("kevinmk30@gmail.com");
            //if (member != null)
            //{
            //    wc.WithdrawViaMpesa(5000, member);
            //}
        }

        [TestMethod]
        public void TestDepositOverTheCounter()
        {
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

        //    DepositComponent dc = new DepositComponent();
        //    RegistrationComponent rc = new RegistrationComponent();
            //string email = "kevinmk30@gmail.com";
            //Member member = rc.GetMemberByEmail(email);
            //if (member != null)
            //{
            //    dc.DepositOverTheCounter(5000, member);
            //}
        }

        [TestMethod]
        public void TestWithdrawOverTheCounter()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //WithdrawComponent wc = new WithdrawComponent();
            //RegistrationComponent rc = new RegistrationComponent();
            //string email = "kevinmk30@gmail.com";
            //Member member = rc.GetMemberByEmail(email);
            //if (member != null)
            //{
            //    wc.WithdrawOverTheCounter(500, member);
            //}
        }

        [TestMethod]
        public void TestSimulateWithdrawOverTheCounter()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //WithdrawComponent wc = new WithdrawComponent();
            //RegistrationComponent rc = new RegistrationComponent();
            //string email = "kevin@softwareproviders.co.ke";
            //Member member = rc.GetMemberByEmail(email);

            //decimal commision = wc.SimulateWithdrawOverTheCounter(9000, member);

            //Debug.WriteLine("Commission: " + commision.ToString());
        } 

        [TestMethod]
        public void TestGetAccountsAmount()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //RegistrationComponent rc = new RegistrationComponent();
            //DepositComponent dc = new DepositComponent();
            //AccountsDetsModel model = new AccountsDetsModel();

            //string email = "kevinmk30@gmail.com";
            //Member member = rc.GetMemberByEmail(email);

            //model.CurrentAcc = dc.GetCurrentAccountAmount(member);
            //model.LoanAcc = dc.GetLoanAccountAmount(member);
            //model.InvestmentAcc = dc.GetInvestmentAccountAmount(member);

            //Debug.WriteLine("Current Account: " + model.CurrentAcc + Environment.NewLine + "Loan Account: " + model.LoanAcc + Environment.NewLine + "Investment Account: " + model.InvestmentAcc);

        }

        [TestMethod]
        public void TestUpdateById()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //RegistrationComponent rc = new RegistrationComponent();
            //MemberDAC mDac = new MemberDAC();
            //string email = "kevinmk30@gmail.com";
            //Member member = rc.GetMemberByEmail(email);
            //member.InformBy = "SMS";

            //mDac.UpdateById(member);
        }

        [TestMethod]
        public void TestGetCommissionRule()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            ////get transactiontype from TT
            ////Use this for all general ledger methods that does not post
            //StaticTransactionsServiceClient sPostingClient = new StaticTransactionsServiceClient();
            //TransactionType TT = sPostingClient.GetTransactionType(1);
            //// 
            //switch (TT.CommComputationMethod)
            //{
            //    case "T": //tiered

            //        TieredRule rule = new TieredRule();
            //        //fill up rule  

            //        List<LookupRow> lr = new List<LookupRow>();
            //        TieredDet[] trd = sPostingClient.GetTieredTable(TT.TieredTableId);
            //        for (int i = 0; i < trd.Length; i++)
            //        {
            //            LookupRow lookupTable = new LookupRow();
            //            lookupTable.Id = trd[i].Id;
            //            lookupTable.Min = trd[i].Min;
            //            lookupTable.Max = trd[i].Max;
            //            lookupTable.Rate = trd[i].Rate;

            //            lr.Add(lookupTable);
            //        }
            //        LookupRow[] terms = lr.ToArray();
            //        rule.LookupTable = lr;
            //        foreach (var l in lr)
            //        {
            //            Debug.WriteLine("Min: " + l.Min + Environment.NewLine + "Max: " + l.Max + Environment.NewLine + "Rate: " + l.Rate);
            //        }

            //        break;
            //    case "F": //flat rule
            //        //fill it up 
            //        FlatRateRule flat = new FlatRateRule();
            //        flat.Absolute = TT.Absolute;
            //        flat.Rate = TT.CommissionAmount;

            //        Debug.WriteLine("Absolute: " + flat.Absolute + Environment.NewLine + "Rate: " + flat.Rate + Environment.NewLine);
            //        break;
            //}

        }

        [TestMethod]
        public void TestComputeInterest()
        {

            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            //ListOffersComponent lc = new ListOffersComponent();
            //InterestComponent ic = new InterestComponent();

            //Offer offer = lc.GetOfferById(1);
            //decimal interest = ic.ComputeSimpleInterest(offer.Amount, offer.Term, decimal.Parse((offer.Interest).ToString()));
            //Debug.WriteLine("interest: " + interest);
        }

        [TestMethod]
        public void TestCompute()
        {

            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            //WithdrawComponent wc = new WithdrawComponent();

            //Transaction drtransaction = new Transaction();
            //drtransaction.Amount = 450;
            //drtransaction.AccountID = Convert.ToInt32(ConfigurationManager.AppSettings["CASHACCOUNT"]);
            //drtransaction.PostDate = DateTime.Today;
            //drtransaction.RecordDate = DateTime.Today;
            //drtransaction.TransactionTypeId = Convert.ToInt32(ConfigurationManager.AppSettings["CASHDEPOSITTRANSACTIONTYPE"]);

            //CommissionRule rule = wc.GetCommissionRule(drtransaction.TransactionTypeId);

            //decimal commision = 0.00M;
            //if (rule is FlatRateRule) //return a flat figure
            //{
            //    FlatRateRule flat = (FlatRateRule)rule; //cast the rule into flat rate rule

            //    if (flat.Absolute)
            //    {
            //        commision = flat.Rate;
            //    } //return a flat rate
            //    else
            //    {
            //        commision = drtransaction.Amount * flat.Rate;
            //    } //return a flat figure based on percent %
            //}
            //else if (rule is TieredRule)  //lookup a commission from a table
            //{
            //    TieredRule tiered = (TieredRule)rule; //cast the rule into Tiered rule

            //    LookupRow[] terms = tiered.LookupTable.ToArray();

            //    for (int i = 0; i < terms.Length; i++)
            //    {
            //        bool _absolute = terms[i].Absolute;
            //        decimal _max = terms[i].Max;
            //        decimal _min = terms[i].Min;
            //        decimal _rate = terms[i].Rate;

            //        if (drtransaction.Amount >= _min && drtransaction.Amount <= _max)
            //        {
            //            if (_absolute)
            //            {
            //                commision = _rate;
            //            } //return a flat rate
            //            else
            //            {
            //                commision = drtransaction.Amount * _rate;
            //            }
            //        }
            //    }
            //}
            //Debug.WriteLine("commision: " + commision);
        }

        [TestMethod]
        public void TestGetAllRegisteredMembers()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            List<Member> members = new List<Member>();
            RegistrationComponent rc = new RegistrationComponent();
            MemberDAC mDac = new MemberDAC();
            string _Output = string.Empty;

            members = mDac.Select();

            if (members != null)
            {
                foreach (var _member in members)
                {
                    Debug.WriteLine("MemberId: " + _member.MemberId + Environment.NewLine + "Surname: " + _member.Surname + Environment.NewLine + "OtherNames: " + _member.OtherNames + Environment.NewLine + "DateOfBirth: " + _member.DateOfBirth + Environment.NewLine + "Gender: " + _member.Gender + Environment.NewLine + "Telephone: " + _member.Telephone + Environment.NewLine + "Email: " + _member.Email + Environment.NewLine + "DateJoined: " + _member.DateJoined + Environment.NewLine + "CustomerId: " + _member.CustomerId + Environment.NewLine + "CurrentAccountId: " + _member.CurrentAccountId + Environment.NewLine + "LoanAccountId: " + _member.LoanAccountId + Environment.NewLine + "InvestmentAccountId: " + _member.InvestmentAccountId + Environment.NewLine + "Status: " + _member.Status + Environment.NewLine + "DateActivated: " + _member.DateActivated + Environment.NewLine + "RefferedBy: " + _member.RefferedBy + Environment.NewLine + "InformBy: " + _member.InformBy);
                    _Output += Environment.NewLine + "MemberId: " + _member.MemberId + Environment.NewLine + "Surname: " + _member.Surname + Environment.NewLine + "OtherNames: " + _member.OtherNames + Environment.NewLine + "DateOfBirth: " + _member.DateOfBirth + Environment.NewLine + "Telephone: " + _member.Telephone + Environment.NewLine + "Email: " + _member.Email + Environment.NewLine + "DateJoined: " + _member.DateJoined + Environment.NewLine + "CustomerId: " + _member.CustomerId + Environment.NewLine + "CurrentAccountId: " + _member.CurrentAccountId + Environment.NewLine + "LoanAccountId: " + _member.LoanAccountId + Environment.NewLine + "InvestmentAccountId: " + _member.InvestmentAccountId + Environment.NewLine + "Status: " + _member.Status + Environment.NewLine + "DateActivated: " + _member.DateActivated + Environment.NewLine + "RefferedBy: " + _member.RefferedBy + Environment.NewLine + "InformBy: " + _member.InformBy + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllRegisteredMembers"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllTransactionTypes()
        {

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            StaticTransactionsServiceClient sPostingClient = new StaticTransactionsServiceClient();
            WithdrawComponent wc = new WithdrawComponent();
            string _Output = string.Empty;

            List<TransactionType> _lstTransactionTypes = wc.GetAllTransactionTypes();

            if (_lstTransactionTypes != null)
            {
                foreach (var _transactionType in _lstTransactionTypes)
                {
                    Debug.WriteLine("TransactionTypeID: " + _transactionType.TransactionTypeID + Environment.NewLine + "DebitCredit: " + _transactionType.DebitCredit + Environment.NewLine + "ShortCode: " + _transactionType.ShortCode + Environment.NewLine + "TxnClass: " + _transactionType.TxnClass + Environment.NewLine + "Description: " + _transactionType.Description + Environment.NewLine + "DefaultAmount: " + _transactionType.DefaultAmount + Environment.NewLine + "AmountExpression: " + _transactionType.AmountExpression + Environment.NewLine + "DefaultMainAccount: " + _transactionType.DefaultMainAccount + Environment.NewLine + "DefaultContraAccount: " + _transactionType.DefaultContraAccount + Environment.NewLine + "NarrativeFlag: " + _transactionType.NarrativeFlag + Environment.NewLine + "DefaultMainNarrative: " + _transactionType.DefaultMainNarrative + Environment.NewLine + "DefaultContraNarrative: " + _transactionType.DefaultContraNarrative + Environment.NewLine + "TxnTypeView: " + _transactionType.TxnTypeView + Environment.NewLine + "ChargeCommission: " + _transactionType.ChargeCommission + Environment.NewLine + "ChargeCommissionToTransaction: " + _transactionType.ChargeCommissionToTransaction + Environment.NewLine + "CommissionAmountExpression: " + _transactionType.CommissionAmountExpression + Environment.NewLine + "CommissionDrAnotherAccount: " + _transactionType.CommissionDrAnotherAccount + Environment.NewLine + "CommissionDrAccount: " + _transactionType.CommissionDrAccount + Environment.NewLine + "CommissionCrAccount: " + _transactionType.CommissionCrAccount + Environment.NewLine + "CommissionNarrativeFlag: " + _transactionType.CommissionNarrativeFlag + Environment.NewLine + "CommissionMainNarrative: " + _transactionType.CommissionMainNarrative + Environment.NewLine + "CommissionContraNarrative: " + _transactionType.CommissionContraNarrative + Environment.NewLine + "CommComputationMethod: " + _transactionType.CommComputationMethod + Environment.NewLine + "Absolute: " + _transactionType.Absolute + Environment.NewLine + "TieredTableId: " + _transactionType.TieredTableId + Environment.NewLine + "CommissionAmount: " + _transactionType.CommissionAmount + Environment.NewLine + "DialogFlag: " + _transactionType.DialogFlag + Environment.NewLine + "ForcePost: " + _transactionType.ForcePost + Environment.NewLine + "Screen: " + _transactionType.Screen + Environment.NewLine + "ValueDateOffset: " + _transactionType.ValueDateOffset + Environment.NewLine + "ChargeWho: " + _transactionType.ChargeWho + Environment.NewLine + "DrCommCalcMethod: " + _transactionType.DrCommCalcMethod + Environment.NewLine + "CrCommCalcMethod: " + _transactionType.CrCommCalcMethod + Environment.NewLine);
                    _Output += Environment.NewLine + "TransactionTypeID: " + _transactionType.TransactionTypeID + Environment.NewLine + "DebitCredit: " + _transactionType.DebitCredit + Environment.NewLine + "ShortCode: " + _transactionType.ShortCode + Environment.NewLine + "TxnClass: " + _transactionType.TxnClass + Environment.NewLine + "Description: " + _transactionType.Description + Environment.NewLine + "DefaultAmount: " + _transactionType.DefaultAmount + Environment.NewLine + "AmountExpression: " + _transactionType.AmountExpression + Environment.NewLine + "DefaultMainAccount: " + _transactionType.DefaultMainAccount + Environment.NewLine + "DefaultContraAccount: " + _transactionType.DefaultContraAccount + Environment.NewLine + "NarrativeFlag: " + _transactionType.NarrativeFlag + Environment.NewLine + "DefaultMainNarrative: " + _transactionType.DefaultMainNarrative + Environment.NewLine + "DefaultContraNarrative: " + _transactionType.DefaultContraNarrative + Environment.NewLine + "TxnTypeView: " + _transactionType.TxnTypeView + Environment.NewLine + "ChargeCommission: " + _transactionType.ChargeCommission + Environment.NewLine + "ChargeCommissionToTransaction: " + _transactionType.ChargeCommissionToTransaction + Environment.NewLine + "CommissionAmountExpression: " + _transactionType.CommissionAmountExpression + Environment.NewLine + "CommissionDrAnotherAccount: " + _transactionType.CommissionDrAnotherAccount + Environment.NewLine + "CommissionDrAccount: " + _transactionType.CommissionDrAccount + Environment.NewLine + "CommissionCrAccount: " + _transactionType.CommissionCrAccount + Environment.NewLine + "CommissionNarrativeFlag: " + _transactionType.CommissionNarrativeFlag + Environment.NewLine + "CommissionMainNarrative: " + _transactionType.CommissionMainNarrative + Environment.NewLine + "CommissionContraNarrative: " + _transactionType.CommissionContraNarrative + Environment.NewLine + "CommComputationMethod: " + _transactionType.CommComputationMethod + Environment.NewLine + "Absolute: " + _transactionType.Absolute + Environment.NewLine + "TieredTableId: " + _transactionType.TieredTableId + Environment.NewLine + "CommissionAmount: " + _transactionType.CommissionAmount + Environment.NewLine + "DialogFlag: " + _transactionType.DialogFlag + Environment.NewLine + "ForcePost: " + _transactionType.ForcePost + Environment.NewLine + "Screen: " + _transactionType.Screen + Environment.NewLine + "ValueDateOffset: " + _transactionType.ValueDateOffset + Environment.NewLine + "ChargeWho: " + _transactionType.ChargeWho + Environment.NewLine + "DrCommCalcMethod: " + _transactionType.DrCommCalcMethod + Environment.NewLine + "CrCommCalcMethod: " + _transactionType.CrCommCalcMethod + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllTransactionTypes"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllMpesaMessagesFromDB()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            DepositComponent dc = new DepositComponent();
            string _Output = string.Empty;

            List<GSMMessage> _sms = dc.GetGSMMessagesFromDB();

            if (_sms != null)
            {
                foreach (var _message in _sms)
                {
                    Debug.WriteLine("Id: " + _message.Id + Environment.NewLine + "UserDataText: " + _message.UserDataText + Environment.NewLine + "OriginatingAddress: " + _message.OriginatingAddress + Environment.NewLine + "SCTimestamp: " + _message.SCTimestamp + Environment.NewLine + "MessageStatus: " + _message.MessageStatus + Environment.NewLine + "Storage: " + _message.Storage + Environment.NewLine + "SmscAddressType: " + _message.SmscAddressType + Environment.NewLine + "SmscAddress: " + _message.SmscAddress + Environment.NewLine + "OriginatingAddressType: " + _message.OriginatingAddressType + Environment.NewLine + "MessageType: " + _message.MessageType + Environment.NewLine + "MessageIndex: " + _message.MessageIndex + Environment.NewLine + "MessageBody: " + _message.MessageBody + Environment.NewLine + "Status: " + _message.Status + Environment.NewLine + "Processed: " + _message.Processed + Environment.NewLine + "IsDeleted: " + _message.IsDeleted + Environment.NewLine + "MpesaAmount: " + _message.MpesaAmount + Environment.NewLine + "FirstName: " + _message.FirstName + Environment.NewLine + "LastName: " + _message.LastName + Environment.NewLine + "LastName: " + _message.LastName + Environment.NewLine + "PhoneNo: " + _message.PhoneNo + Environment.NewLine + "DateSent: " + _message.DateSent + Environment.NewLine + "DateReceived: " + _message.DateReceived + Environment.NewLine + "MpesaBalance: " + _message.MpesaBalance + Environment.NewLine + "TimeSent: " + _message.TimeSent + Environment.NewLine + "AmPm: " + _message.AmPm + Environment.NewLine);
                    _Output += Environment.NewLine + "Id: " + _message.Id + Environment.NewLine + "UserDataText: " + _message.UserDataText + Environment.NewLine + "OriginatingAddress: " + _message.OriginatingAddress + Environment.NewLine + "SCTimestamp: " + _message.SCTimestamp + Environment.NewLine + "MessageStatus: " + _message.MessageStatus + Environment.NewLine + "Storage: " + _message.Storage + Environment.NewLine + "SmscAddressType: " + _message.SmscAddressType + Environment.NewLine + "SmscAddress: " + _message.SmscAddress + Environment.NewLine + "OriginatingAddressType: " + _message.OriginatingAddressType + Environment.NewLine + "MessageType: " + _message.MessageType + Environment.NewLine + "MessageIndex: " + _message.MessageIndex + Environment.NewLine + "MessageBody: " + _message.MessageBody + Environment.NewLine + "Status: " + _message.Status + Environment.NewLine + "Processed: " + _message.Processed + Environment.NewLine + "IsDeleted: " + _message.IsDeleted + Environment.NewLine + "MpesaAmount: " + _message.MpesaAmount + Environment.NewLine + "FirstName: " + _message.FirstName + Environment.NewLine + "LastName: " + _message.LastName + Environment.NewLine + "LastName: " + _message.LastName + Environment.NewLine + "PhoneNo: " + _message.PhoneNo + Environment.NewLine + "DateSent: " + _message.DateSent + Environment.NewLine + "DateReceived: " + _message.DateReceived + Environment.NewLine + "MpesaBalance: " + _message.MpesaBalance + Environment.NewLine + "TimeSent: " + _message.TimeSent + Environment.NewLine + "AmPm: " + _message.AmPm + Environment.NewLine + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllMpesaMessagesFromDB"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllAccounts()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            WithdrawComponent wc = new WithdrawComponent();
            string _Output = string.Empty;

            List<Account> _accounts = wc.GetAllAccounts();

            if (_accounts != null)
            {
                foreach (var _account in _accounts)
                {
                    Debug.WriteLine("AccountID: " + _account.AccountID + Environment.NewLine + "CustomerId: " + _account.CustomerId + Environment.NewLine + "AccountName: " + _account.AccountName + Environment.NewLine + "AccountNo: " + _account.AccountNo + Environment.NewLine + "AccountTypeId: " + _account.AccountTypeId + Environment.NewLine + "COAId: " + _account.COAId + Environment.NewLine + "Branch: " + _account.Branch + Environment.NewLine + "PassFlag: " + _account.PassFlag + Environment.NewLine + "BookBalance: " + _account.BookBalance + Environment.NewLine + "ClearedBalance: " + _account.ClearedBalance + Environment.NewLine + "InterestRate: " + _account.InterestRate + Environment.NewLine + "AccruedInt: " + _account.AccruedInt + Environment.NewLine + "Limit: " + _account.Limit + Environment.NewLine + "LimitFlag: " + _account.LimitFlag + Environment.NewLine + "LimitCheckFlag: " + _account.LimitCheckFlag + Environment.NewLine + "Bal30: " + _account.Bal30 + Environment.NewLine + "Bal60: " + _account.Bal60 + Environment.NewLine + "Bal90: " + _account.Bal90 + Environment.NewLine + "BalOver90: " + _account.BalOver90 + Environment.NewLine + "IntRate30: " + _account.IntRate30 + Environment.NewLine + "IntRate60: " + _account.IntRate60 + Environment.NewLine + "IntRate90: " + _account.IntRate90 + Environment.NewLine + "IntRateOver90: " + _account.IntRateOver90 + Environment.NewLine + "Closed: " + _account.Closed);
                    _Output += Environment.NewLine + "AccountID: " + _account.AccountID + Environment.NewLine + "CustomerId: " + _account.CustomerId + Environment.NewLine + "AccountName: " + _account.AccountName + Environment.NewLine + "AccountNo: " + _account.AccountNo + Environment.NewLine + "AccountTypeId: " + _account.AccountTypeId + Environment.NewLine + "COAId: " + _account.COAId + Environment.NewLine + "Branch: " + _account.Branch + Environment.NewLine + "PassFlag: " + _account.PassFlag + Environment.NewLine + "BookBalance: " + _account.BookBalance + Environment.NewLine + "ClearedBalance: " + _account.ClearedBalance + Environment.NewLine + "InterestRate: " + _account.InterestRate + Environment.NewLine + "AccruedInt: " + _account.AccruedInt + Environment.NewLine + "Limit: " + _account.Limit + Environment.NewLine + "LimitFlag: " + _account.LimitFlag + Environment.NewLine + "LimitCheckFlag: " + _account.LimitCheckFlag + Environment.NewLine + "Bal30: " + _account.Bal30 + Environment.NewLine + "Bal60: " + _account.Bal60 + Environment.NewLine + "Bal90: " + _account.Bal90 + Environment.NewLine + "BalOver90: " + _account.BalOver90 + Environment.NewLine + "IntRate30: " + _account.IntRate30 + Environment.NewLine + "IntRate60: " + _account.IntRate60 + Environment.NewLine + "IntRate90: " + _account.IntRate90 + Environment.NewLine + "IntRateOver90: " + _account.IntRateOver90 + Environment.NewLine + "Closed: " + _account.Closed + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllAccounts"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllAccountTransactions()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            DepositComponent dc = new DepositComponent();
            string _Output = string.Empty;

            List<Transaction> _Transactions = dc.GetAccountTransactions(104);

            if (_Transactions != null)
            {
                foreach (var _Transaction in _Transactions)
                {
                    Debug.WriteLine("TransactionID: " + _Transaction.TransactionID + Environment.NewLine + "TransactionTypeId: " + _Transaction.TransactionTypeId + Environment.NewLine + "AccountID: " + _Transaction.AccountID + Environment.NewLine + "Amount: " + _Transaction.Amount + Environment.NewLine + "PostDate: " + _Transaction.PostDate + Environment.NewLine + "RecordDate: " + _Transaction.RecordDate + Environment.NewLine + "ValueDate: " + _Transaction.ValueDate + Environment.NewLine + "Narrative: " + _Transaction.Narrative + Environment.NewLine + "ForcePostFlag: " + _Transaction.ForcePostFlag + Environment.NewLine + "StatementFlag: " + _Transaction.StatementFlag + Environment.NewLine + "Authorizer: " + _Transaction.Authorizer + Environment.NewLine + "UserID: " + _Transaction.UserID + Environment.NewLine + "Reference: " + _Transaction.Reference + Environment.NewLine + "ContraReference: " + _Transaction.ContraReference);
                    _Output += Environment.NewLine + "TransactionID: " + _Transaction.TransactionID + Environment.NewLine + "TransactionTypeId: " + _Transaction.TransactionTypeId + Environment.NewLine + "AccountID: " + _Transaction.AccountID + Environment.NewLine + "Amount: " + _Transaction.Amount + Environment.NewLine + "PostDate: " + _Transaction.PostDate + Environment.NewLine + "RecordDate: " + _Transaction.RecordDate + Environment.NewLine + "ValueDate: " + _Transaction.ValueDate + Environment.NewLine + "Narrative: " + _Transaction.Narrative + Environment.NewLine + "ForcePostFlag: " + _Transaction.ForcePostFlag + Environment.NewLine + "StatementFlag: " + _Transaction.StatementFlag + Environment.NewLine + "Authorizer: " + _Transaction.Authorizer + Environment.NewLine + "UserID: " + _Transaction.UserID + Environment.NewLine + "Reference: " + _Transaction.Reference + Environment.NewLine + "ContraReference: " + _Transaction.ContraReference + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllAccountTransactions"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllCustomers()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            WithdrawComponent wc = new WithdrawComponent();
            string _Output = string.Empty;

            List<Customer> _Customers = wc.GetAllCustomers();

            if (_Customers != null)
            {
                foreach (var _customer in _Customers)
                {
                    Debug.WriteLine("CustomerId: " + _customer.CustomerId + Environment.NewLine + "CustomerNo: " + _customer.CustomerNo + Environment.NewLine + "Name: " + _customer.Name + Environment.NewLine + "Address: " + _customer.Address + Environment.NewLine + "Telephone: " + _customer.Telephone + Environment.NewLine + "Email: " + _customer.Email + Environment.NewLine + "Branch: " + _customer.Branch + Environment.NewLine + "BillToName: " + _customer.BillToName + Environment.NewLine + "BillToAddress: " + _customer.BillToAddress + Environment.NewLine + "BillToTelephone: " + _customer.BillToTelephone + Environment.NewLine + "BillToEmail: " + _customer.BillToEmail + Environment.NewLine + "MemberId: " + _customer.MemberId + Environment.NewLine + "CreatedDate: " + _customer.CreatedDate);
                    _Output += Environment.NewLine + "CustomerId: " + _customer.CustomerId + Environment.NewLine + "CustomerNo: " + _customer.CustomerNo + Environment.NewLine + "Name: " + _customer.Name + Environment.NewLine + "Address: " + _customer.Address + Environment.NewLine + "Telephone: " + _customer.Telephone + Environment.NewLine + "Email: " + _customer.Email + Environment.NewLine + "Branch: " + _customer.Branch + Environment.NewLine + "BillToName: " + _customer.BillToName + Environment.NewLine + "BillToAddress: " + _customer.BillToAddress + Environment.NewLine + "BillToTelephone: " + _customer.BillToTelephone + Environment.NewLine + "BillToEmail: " + _customer.BillToEmail + Environment.NewLine + "MemberId: " + _customer.MemberId + Environment.NewLine + "CreatedDate: " + _customer.CreatedDate + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllCustomers"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }

        }

        [TestMethod]
        public void TestGetAllOffers()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            ListOffersComponent lc = new ListOffersComponent();
            AcceptOfferComponent ac = new AcceptOfferComponent();
            MemberDAC mc = new MemberDAC();
             
            string _Output = string.Empty;
             
            List<Offer> _Offers = lc.GetAllOffers();

            if (_Offers != null)
            {
                foreach (var _Offer in _Offers)
                {
                    Debug.WriteLine("MemberId: " + _Offer.MemberId + Environment.NewLine + "OfferType: " + _Offer.OfferType + Environment.NewLine + "Description: " + _Offer.Description + Environment.NewLine + "Amount: " + _Offer.Amount + Environment.NewLine + "Term: " + _Offer.Term + Environment.NewLine + "Interest: " + _Offer.Interest + Environment.NewLine + "PublicOffer: " + _Offer.PublicOffer + Environment.NewLine + "Status: " + _Offer.Status + Environment.NewLine + "CreatedDate: " + _Offer.CreatedDate + Environment.NewLine + "ExpiryDate: " + _Offer.ExpiryDate + Environment.NewLine + "Offerees: " + _Offer.Offerees);
                    _Output += Environment.NewLine + "MemberId: " + _Offer.MemberId + Environment.NewLine + "OfferType: " + _Offer.OfferType + Environment.NewLine + "Description: " + _Offer.Description + Environment.NewLine + "Amount: " + _Offer.Amount + Environment.NewLine + "Term: " + _Offer.Term + Environment.NewLine + "Interest: " + _Offer.Interest + Environment.NewLine + "PublicOffer: " + _Offer.PublicOffer + Environment.NewLine + "Status: " + _Offer.Status + Environment.NewLine + "CreatedDate: " + _Offer.CreatedDate + Environment.NewLine + "ExpiryDate: " + _Offer.ExpiryDate + Environment.NewLine + "Offerees: " + _Offer.Offerees + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllOffers"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }

        }

        [TestMethod]
        public void TestGetAccountViewTransactionsByDate()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            TransactionsComponent tc = new TransactionsComponent();
            string _Output = string.Empty;

            List<TransactionModel> transactions = tc.GetAccountViewTransactionsByDate(104, DateTime.Now.Date.AddMonths(-3), DateTime.Now.Date.AddMonths(3));

            if (transactions != null)
            {
                foreach (var _transaction in transactions)
                {
                    Debug.WriteLine("TransactionID: " + _transaction.TransactionID + Environment.NewLine + "Narrative: " + _transaction.Narrative + Environment.NewLine + "Credit: " + _transaction.Credit + Environment.NewLine + "Debit: " + _transaction.Debit + Environment.NewLine + "PostDate: " + _transaction.PostDate + Environment.NewLine + "Balance: " + _transaction.Balance + Environment.NewLine);
                    _Output += Environment.NewLine + "TransactionID: " + _transaction.TransactionID + Environment.NewLine + "Narrative: " + _transaction.Narrative + Environment.NewLine + "Credit: " + _transaction.Credit + Environment.NewLine + "Debit: " + _transaction.Debit + Environment.NewLine + "PostDate: " + _transaction.PostDate + Environment.NewLine + "Balance: " + _transaction.Balance + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAccountViewTransactionsByDate"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }

        }

        [TestMethod]
        public void TestGetAllMailingGroupAddress()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            List<Member> members = new List<Member>();
            RegistrationComponent rc = new RegistrationComponent();
            MemberDAC mDac = new MemberDAC();
            string _Output = string.Empty;

            members = mDac.Select();

            if (members != null)
            {
                foreach (var _member in members)
                {
                    Debug.WriteLine("MemberId: " + _member.MemberId + Environment.NewLine + "Surname: " + _member.Surname + Environment.NewLine + "OtherNames: " + _member.OtherNames + Environment.NewLine + "DateOfBirth: " + _member.DateOfBirth + Environment.NewLine + "Gender: " + _member.Gender + Environment.NewLine + "Telephone: " + _member.Telephone + Environment.NewLine + "Email: " + _member.Email + Environment.NewLine + "DateJoined: " + _member.DateJoined + Environment.NewLine + "CustomerId: " + _member.CustomerId + Environment.NewLine + "CurrentAccountId: " + _member.CurrentAccountId + Environment.NewLine + "LoanAccountId: " + _member.LoanAccountId + Environment.NewLine + "InvestmentAccountId: " + _member.InvestmentAccountId + Environment.NewLine + "Status: " + _member.Status + Environment.NewLine + "DateActivated: " + _member.DateActivated + Environment.NewLine + "RefferedBy: " + _member.RefferedBy + Environment.NewLine + "InformBy: " + _member.InformBy);
                    _Output += Environment.NewLine + "MemberId: " + _member.MemberId + Environment.NewLine + "Surname: " + _member.Surname + Environment.NewLine + "OtherNames: " + _member.OtherNames + Environment.NewLine + "DateOfBirth: " + _member.DateOfBirth + Environment.NewLine + "Telephone: " + _member.Telephone + Environment.NewLine + "Email: " + _member.Email + Environment.NewLine + "DateJoined: " + _member.DateJoined + Environment.NewLine + "CustomerId: " + _member.CustomerId + Environment.NewLine + "CurrentAccountId: " + _member.CurrentAccountId + Environment.NewLine + "LoanAccountId: " + _member.LoanAccountId + Environment.NewLine + "InvestmentAccountId: " + _member.InvestmentAccountId + Environment.NewLine + "Status: " + _member.Status + Environment.NewLine + "DateActivated: " + _member.DateActivated + Environment.NewLine + "RefferedBy: " + _member.RefferedBy + Environment.NewLine + "InformBy: " + _member.InformBy + Environment.NewLine;
                }


                string _filePath = ConfigurationManager.AppSettings["TestGetAllRegisteredMembers"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }

        [TestMethod]
        public void TestGetAllSTO()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            LoanDiaryComponent lc = new LoanDiaryComponent();
            string _Output = string.Empty;

            List<STO> _STOs = lc.GetAllSTO();

            if (_STOs != null)
            {
                foreach (var _sto in _STOs)
                {
                    Debug.WriteLine("Id: " + _sto.Id + Environment.NewLine + "AmountPaid: " + _sto.AmountPaid + Environment.NewLine + "ChargeWho: " + _sto.ChargeWho + Environment.NewLine + "CommissionAccount: " + _sto.CommissionAccount + Environment.NewLine + "CreateDate: " + _sto.CreateDate + Environment.NewLine + "CrMember: " + _sto.CrMember + Environment.NewLine + "CrTxnType: " + _sto.CrTxnType + Environment.NewLine + "DrMember: " + _sto.DrMember + Environment.NewLine + "DrTxnType: " + _sto.DrTxnType + Environment.NewLine + "EndDate: " + _sto.EndDate + Environment.NewLine + "FeesFlag: " + _sto.FeesFlag + Environment.NewLine + "Interval: " + _sto.Interval + Environment.NewLine + "LimitFlag: " + _sto.LimitFlag + Environment.NewLine + "LoanId: " + _sto.LoanId + Environment.NewLine + "NextPayDate: " + _sto.NextPayDate + Environment.NewLine + "NoOfPayments: " + _sto.NoOfPayments + Environment.NewLine + "PartialPay: " + _sto.PartialPay + Environment.NewLine + "PayAmount: " + _sto.PayAmount + Environment.NewLine + "StartDate: " + _sto.StartDate + Environment.NewLine + "STOType: " + _sto.STOType + Environment.NewLine + "TotalToPay: " + _sto.TotalToPay + Environment.NewLine + "CrAccount: " + _sto.CrAccount + Environment.NewLine + "DrAccount: " + _sto.DrAccount + Environment.NewLine);
                    _Output += Environment.NewLine + "Id: " + _sto.Id + Environment.NewLine + "AmountPaid: " + _sto.AmountPaid + Environment.NewLine + "ChargeWho: " + _sto.ChargeWho + Environment.NewLine + "CommissionAccount: " + _sto.CommissionAccount + Environment.NewLine + "CreateDate: " + _sto.CreateDate + Environment.NewLine + "CrMember: " + _sto.CrMember + Environment.NewLine + "CrTxnType: " + _sto.CrTxnType + Environment.NewLine + "DrMember: " + _sto.DrMember + Environment.NewLine + "DrTxnType: " + _sto.DrTxnType + Environment.NewLine + "EndDate: " + _sto.EndDate + Environment.NewLine + "FeesFlag: " + _sto.FeesFlag + Environment.NewLine + "Interval: " + _sto.Interval + Environment.NewLine + "LimitFlag: " + _sto.LimitFlag + Environment.NewLine + "LoanId: " + _sto.LoanId + Environment.NewLine + "NextPayDate: " + _sto.NextPayDate + Environment.NewLine + "NoOfPayments: " + _sto.NoOfPayments + Environment.NewLine + "PartialPay: " + _sto.PartialPay + Environment.NewLine + "PayAmount: " + _sto.PayAmount + Environment.NewLine + "StartDate: " + _sto.StartDate + Environment.NewLine + "STOType: " + _sto.STOType + Environment.NewLine + "TotalToPay: " + _sto.TotalToPay + Environment.NewLine + "CrAccount: " + _sto.CrAccount + Environment.NewLine + "DrAccount: " + _sto.DrAccount + Environment.NewLine + Environment.NewLine;
                }

                string _filePath = ConfigurationManager.AppSettings["TestGetAllSTO"];
                using (System.IO.FileStream aFile = new System.IO.FileStream(_filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(_Output);
                    sw.WriteLine("**********************************************************************");
                }
            }
        }









    }
}