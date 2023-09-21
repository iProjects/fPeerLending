using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using fPeerLending.Business;
using System.Collections.Generic;


namespace MemberDACUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
 

        [TestMethod]
        public void TestCount()
        {
            InterestComponent ic = new InterestComponent();
            decimal ans = ic.ComputeSimpleInterest("Y",3000, 9, 8);
            System.Diagnostics.Debug.WriteLine("The answe is "+ ans);
            //dc.DepositViaMpesa(300, "", 254, "Test");
        }
    }
}
