//====================================================================================================
// Code generated with Motion: BC Gen (Build 2.2.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 08/03/2013 18:31:33 
//====================================================================================================

using fPeerLending.Data;
using fPeerLending.Entities;
using fPeerLending.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace fPeerLending.Business
{

    public partial class InterestComponent
    {
        
        // term is in years and rate is in decimals
        //Interest = Pricipal x rate x time
        /*
         * A = P(1 + rt); R = r * 100

Where:

    A = Total Accrued Amount (principal + interest)
    P = Principal Amount
    I = Interest Amount
    r = Rate of Interest per year in decimal; r = R/100
    t = Time Period involved in months or years

From the base formula, A = P(1 + rt) derived from A = P + I and I = Prt so A = P + I = P + Prt = P(1 + rt)
         */
        public decimal ComputeSimpleInterest(decimal amount, int term, decimal rate) //Yearly
        {
            return ComputeSimpleInterest("Y", amount, term, rate);
        }
        public decimal ComputeSimpleInterest(string period, decimal amount, int term, decimal rate) 
        {
            if (period.ToUpper().Equals("D"))
                return amount * term * 30M * (rate / 100M);

            if(period.ToUpper().Equals("D360"))
                return amount * (term * 30M / 360M) * (rate / 100M);

            if (period.ToUpper().Equals("D365"))
                return amount * (term * 30M / 365M) * (rate / 100M);

            if(period.ToUpper().Equals("M"))
                return amount * term * (rate / 100M);
                
            //Yearly == defaulut
            decimal intr = amount * (term / 12M) * (rate / 100M);
            return intr;
        }


    }
}