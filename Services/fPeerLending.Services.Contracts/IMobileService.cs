using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using fPeerLending.Entities;
using fPeerLending.Framework;
using fPeerLending.Framework.ExceptionTypes;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace fPeerLending.Services.Contracts
{
    [ServiceContract]
    public interface IMobileService
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "MakeBorrowOffer/{offerModel}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Offer MakeBorrowOffer(OfferModel offerModel);

        [OperationContract]
        [WebGet(
            UriTemplate = "MakeLendOffer/{offerModel}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Offer MakeLendOffer(OfferModel offerModel);

        [OperationContract]
        [WebGet(
            UriTemplate = "GetOfferById/{Id}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Offer GetOfferById(int Id);

        [OperationContract]
        [WebGet(
            UriTemplate = "GetOffers",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Offer> GetOffers();
         
    }
}
