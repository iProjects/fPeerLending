using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace fPeerLending.Framework.ExceptionTypes
{


    [Serializable]
    public class PeerLendingException : BusinessLogicCustomException
    {
        public PeerLendingException()
            : base() { }

        public PeerLendingException(string message)
            : base(message) { }

        public PeerLendingException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public PeerLendingException(string message, Exception innerException)
            : base(message, innerException) { }

        public PeerLendingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected PeerLendingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }


    [Serializable]
    public class OffersException : PeerLendingException
    {
        public OffersException()
            : base() { }

        public OffersException(string message)
            : base(message) { }

        public OffersException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public OffersException(string message, Exception innerException)
            : base(message, innerException) { }

        public OffersException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected OffersException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
      public class LoansException : PeerLendingException
    {
        public LoansException()
            : base() { }

        public LoansException(string message)
            : base(message) { }

        public LoansException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public LoansException(string message, Exception innerException)
            : base(message, innerException) { }

        public LoansException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected LoansException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }


    [Serializable]
    public class DepositException : PeerLendingException
      {
          public DepositException()
              : base() { }

          public DepositException(string message)
              : base(message) { }

          public DepositException(string format, params object[] args)
              : base(string.Format(format, args)) { }

          public DepositException(string message, Exception innerException)
              : base(message, innerException) { }

          public DepositException(string format, Exception innerException, params object[] args)
              : base(string.Format(format, args), innerException) { }

          protected DepositException(SerializationInfo info, StreamingContext context)
              : base(info, context) { }
      }

    [Serializable]
    public class DeregistrationException : PeerLendingException
    {
        public DeregistrationException()
            : base() { }

        public DeregistrationException(string message)
            : base(message) { }

        public DeregistrationException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DeregistrationException(string message, Exception innerException)
            : base(message, innerException) { }

        public DeregistrationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DeregistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }



    }

    [Serializable]
    public class TransactionException : PeerLendingException
    {
        public TransactionException()
            : base() { }

        public TransactionException(string message)
            : base(message) { }

        public TransactionException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public TransactionException(string message, Exception innerException)
            : base(message, innerException) { }

        public TransactionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected TransactionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class WithdrawalException : PeerLendingException
    {
        //public List<SimulatePostStatus> SimulatePostStatus { get; set; }
        public WithdrawalException()
            : base() { }
        //public WithdrawalException(List<SimulatePostStatus> sme, string message)
        //    : base(message) { }// { SimulatePostStatus = sme; }

        public WithdrawalException(string message)
            : base(message) { }

        public WithdrawalException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public WithdrawalException(string message, Exception innerException)
            : base(message, innerException) { }

        public WithdrawalException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected WithdrawalException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}
