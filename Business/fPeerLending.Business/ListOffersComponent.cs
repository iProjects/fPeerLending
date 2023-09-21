
using fanikiwaGL.Business;
using fPeerLending.Data;
using fPeerLending.Entities;
using fPeerLending.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fPeerLending.Business
{
    public partial class ListOffersComponent
    {
        #region Offer Receipients
        public List<OfferReceipient> GetOfferReceipients(int OfferId)
        {
            OfferReceipientDAC offDac = new OfferReceipientDAC();
            return offDac.SelectByOfferId(OfferId);
        }

        public OfferReceipient CreateOfferReceipient(OfferReceipient or)
        {
            OfferReceipientDAC offDac = new OfferReceipientDAC();
            return offDac.Create(or);
        }
        #endregion
        public List<Offer> SelectOffersToMember(Member m, string OfferType)
        {
            OfferDAC or = new OfferDAC();

            List<Offer> oM = or.SelectOffersToMemberId(m.MemberId, OfferType);

            List<Offer> oT = new List<Offer>();
            if (!string.IsNullOrEmpty(m.Telephone))
            {
                oT = or.SelectOffersToMemberTelno(m.Telephone, OfferType);
            }

            List<Offer> oE = new List<Offer>();
            if (!string.IsNullOrEmpty(m.Email))
            {
                oE = or.SelectOffersToMemberEmail(m.Email, OfferType);
            }
            return oM.Union(oT).Union(oE).ToList(); //Union - removes duplicates; concat - doesnt
        }
        public List<Offer> SelectOffersToMember(int MemberId, string OfferType)
        {
            MemberDAC mDac = new MemberDAC();
            return SelectOffersToMember(mDac.SelectById(MemberId), OfferType);
        }


        public List<Offer> ListLendOffers(Member m)
        {
            List<Offer> offers = ListPrivateLendOffers( m);
            return offers.Union(GetPublicLendOffers(m)).Where(o => o.Status.Equals("Open")).ToList();
        }
        public List<Offer> ListBorrowOffers(Member m)
        {
            List<Offer> offers = ListPrivateBorrowOffers(m);
            return offers.Concat(GetPublicBorrowOffers(m)).Where(o => o.Status.Equals("Open")).ToList();
        }
        public List<Offer> ListMyOffers(Member m)
        {
            OfferDAC or = new OfferDAC();
            List<Offer> myBorrowOffers = ListMyBorrowOffers(m);
            List<Offer> myLendOffers = ListMyLendOffers(m);
            List<Offer> myOffers = myLendOffers.Union(myBorrowOffers).ToList(); 
            return myOffers;
        } 
        public List<Offer> GetPublicLendOffers(Member m)
        {
            OfferDAC or = new OfferDAC();
            return or.SelectPublicOffers(m.MemberId,"L");
        }
        public List<Offer> GetPublicBorrowOffers(Member m)
        {//gets all public offers not made by me
            OfferDAC or = new OfferDAC();
            return or.SelectPublicOffers(m.MemberId,"B");
        }

        public List<Offer> ListPrivateLendOffers(Member m)
        {
            return SelectOffersToMember(m, "L");
        }
        public List<Offer> ListPrivateBorrowOffers(Member m)
        {
            OfferDAC or = new OfferDAC();
            return SelectOffersToMember(m, "B");
        }

        public List<Offer> ListMyBorrowOffers(Member m)
        {
            OfferDAC or = new OfferDAC();
            return or.SelectOffersByMember(m.MemberId, "B");
        }
        public List<Offer> ListMyLendOffers(Member m)
        {
            OfferDAC or = new OfferDAC();
            return or.SelectOffersByMember(m.MemberId, "L");
        }
        public Offer GetOfferById(int Id)
        {
            OfferDAC or = new OfferDAC();
            return or.SelectById(Id);
        }
        public void DeleteOfferById( Offer offer)
        {
            //if it is a lend offer, clear the limit first 
            if (offer.OfferType.Equals("L"))
            {
                MemberDAC mDAC = new MemberDAC();
                Member member = mDAC.SelectById(offer.MemberId);

                //reverse the limit
                StaticTransactionsComponent sc = new StaticTransactionsComponent();
                sc.UnBlockFunds(member.CurrentAccountId, offer.Amount);
            }

            //remove the offer from database
            OfferDAC or = new OfferDAC();
            or.DeleteById(offer.Id);
        }
        public List<Offer> GetAllOffers()
        {
            OfferDAC or = new OfferDAC();
            return or.GetAllOffers();
        }

        public List<Offer> GetAllMyOffersByMember(string OfferOwner, int total_display_records)
        {
            OfferDAC or = new OfferDAC();
            return or.GetAllMyOffersByMember(OfferOwner).Take(total_display_records).ToList();
        }

        public List<Offer> GetAllMyOffersByOfferType(string OfferType, int total_display_records)
        {
            OfferDAC or = new OfferDAC();
            return or.GetAllMyOffersByOfferType(OfferType).Take(total_display_records).ToList();
        }

        public List<Offer> GetAllMyOffersByDate(DateTime datefrom, DateTime dateto, int total_display_records)
        {
            OfferDAC or = new OfferDAC();
            return or.GetAllMyOffersByDate(datefrom, dateto).Take(total_display_records).ToList();
        }

        public List<Offer> GetAllNonExpiredOffers()
        {
            OfferDAC or = new OfferDAC();
            return or.GetAllNonExpiredOffers();
        }





    }
}