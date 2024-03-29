//====================================================================================================
// Base code generated with Inertia: BE Gen (Build 2.5.5049.15162)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by francis.muraya at KPC0201M on 11/27/2014 09:03:51 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;


namespace fPeerLending.Entities
{
    /// <summary>
    /// Represents a row of OfferReceipient data.
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class OfferReceipient
    {
        /// <summary>
        /// Gets or sets a int value for the OfferReceipientId column.
        /// </summary>
        [DataMember] 
        public int OfferReceipientId { get; set; }

        /// <summary>
        /// Gets or sets a int value for the OfferId column.
        /// </summary>
        [DataMember]
        public int OfferId { get; set; }

        /// <summary>
        /// Gets or sets a string value for the IdType column.
        /// </summary>
        [DataMember]
        [Display(Name = "Type")]
        public string IdType { get; set; }

        /// <summary>
        /// Gets or sets a int value for the MemberId column.
        /// </summary>
        [DataMember]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets a string value for the MemberEmail column.
        /// </summary>
        [DataMember]
        [Display(Name = "Email")]
        public string MemberEmail { get; set; }

        /// <summary>
        /// Gets or sets a string value for the MemberTelno column.
        /// </summary>
        [DataMember]
        [Display(Name = "Telno")]
        public string MemberTelno { get; set; }

        /// <summary>
        /// Gets or sets a string value for the MailingGroup column.
        /// </summary>
        [DataMember]
        [Display(Name = "Group")]
        public string MailingGroup { get; set; }
    }
}
