
using fanikiwaGL.Business;
using fanikiwaGL.Entities;
using fCommon.Utility;
using fPeerLending.Data;
using fPeerLending.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;

namespace fPeerLending.Business
{
    public class LoansComponent
    {

        public List<STO> ListMyLoans(int MemberId)
        {           
            StaticTransactionsComponent st = new StaticTransactionsComponent();
            return st.SelectSTOByMember(MemberId);
        }
        public List<STO> ListMyInvestments(int MemberId)
        {
            StaticTransactionsComponent st = new StaticTransactionsComponent();
            return st.SelectSTOByMember(MemberId);
        }
        public List<STO> ListLoansByAdmin()
        {
            StaticTransactionsComponent st = new StaticTransactionsComponent();
            return st.SelectSTOByAdmin();
        } 


    }
}
