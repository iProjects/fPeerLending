using fPeerLending.Data;
using fPeerLending.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fPeerLending.Business
{
    public static class StaticOfferChange
    {
        public static  void SetOfferStatus(Offer offer, OfferStatus status)
        {
            OfferDAC offerDAC = new OfferDAC();
            offer.Status = status.ToString();
            offerDAC.UpdateById(offer);
        }
    }
}
