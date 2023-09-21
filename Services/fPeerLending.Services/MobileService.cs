
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using fPeerLending.Business; 
using fPeerLending.Entities;
using fPeerLending.Framework;
using fPeerLending.Framework.ExceptionHandlers;
using fPeerLending.Framework.ExceptionTypes;
using fPeerLending.Services;
using fPeerLending.Services.Contracts;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace fPeerLending.Services
{
    public class MobileService : IMobileService
    {
        #region "Constructor"
        public MobileService()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            IConfigurationSource config = ConfigurationSourceFactory.Create();
            ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            Logger.SetLogWriter(new LogWriterFactory(config).Create(), false);
            ExceptionManager exManager = factory.CreateManager();
            ExceptionPolicy.SetExceptionManager(factory.CreateManager(), false);
        }
        #endregion "Constructor"

        public Offer MakeBorrowOffer(OfferModel offerModel)
        {
            try
            {
                MakeOfferComponent mc = new MakeOfferComponent();
                return mc.MakeBorrowOffer(offerModel);
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
                if (rethrow)
                {
                    CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
                    throw new FaultException<CustomExpMsg>(customMsg,
                        new FaultReason(customMsg.ErrorMsg),
                        new FaultCode("MakeBorrowOffer"));
                }

                return null;
            }
        }

        public Offer MakeLendOffer(OfferModel offerModel)
        {
            try
            {
                MakeOfferComponent mc = new MakeOfferComponent();
                return mc.MakeLendOffer(offerModel);
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
                if (rethrow)
                {
                    CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
                    throw new FaultException<CustomExpMsg>(customMsg,
                        new FaultReason(customMsg.ErrorMsg),
                        new FaultCode("MakeLendOffer"));
                }

                return null;
            }
        }

        public Offer GetOfferById(int Id)
        {
            try
            {
                ListOffersComponent lc = new ListOffersComponent();
                return lc.GetOfferById(Id);
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
                if (rethrow)
                {
                    CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
                    throw new FaultException<CustomExpMsg>(customMsg,
                        new FaultReason(customMsg.ErrorMsg),
                        new FaultCode("GetOfferById"));
                }

                return null;
            }
        }

        public List<Offer> GetOffers()
        {
            try
            {
                ListOffersComponent lc = new ListOffersComponent();
                return lc.GetAllOffers();
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
                if (rethrow)
                {
                    CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
                    throw new FaultException<CustomExpMsg>(customMsg,
                        new FaultReason(customMsg.ErrorMsg),
                        new FaultCode("GetOffers"));
                }

                return null;
            }
        }



    }
}