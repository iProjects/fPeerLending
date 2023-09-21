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
    public class OfferModel
    {
        /// <summary>
        /// Gets or sets a int value for the Id column.
        /// </summary>
        [DataMember] 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a string value for the OfferType column.
        /// </summary>

        [DataMember]
        [Display(Name = "Offer Type")]
        public string OfferType { get; set; }

        /// <summary>
        /// Gets or sets a int value for the MemberId column.
        /// </summary>
        [DataMember]
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Description column.
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Description cannot be null, e.g borrow offer 10,000 or lend offer 20,000 ")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the ExpiryDate column.
        /// </summary>
        [DataMember]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the CreatedDate column.
        /// </summary>
        [DataMember]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a decimal value for the Amount column.
        /// </summary>
        [DataMember]
        [Range(0, 1000000, ErrorMessage = "Amount must be between 0 and 1,000,000")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets a int value for the Term column.
        /// </summary>
        [DataMember]
        [Display(Name = "Term (in months)")]
        public int Term { get; set; }

        /// <summary>
        /// Gets or sets a double value for the Interest column.
        /// </summary>
        [DataMember]
        [Display(Name = "Interest")]
        public double Interest { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Status column.
        /// </summary>
        [DataMember]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [DataMember]
        [Display(Name = "Offer Owner")]
        public string OfferOwner { get; set; }

        [DataMember]
        [Display(Name = "Public Offer")]
        public string PublicOffer { get; set; }

        [DataMember]
        [Display(Name = "Partial Pay")]
        public bool PartialPay { get; set; }


        public List<Offeree> Reciepients { get; set; }
    }

    public abstract class Offeree
    {
        public string Name { get; set; }
        public string IconSource { get; set; }
    }
    
    public class EmailOfferee : Offeree
    {
        public string Email { get; set; }
    }
    public class TelephoneOfferee : Offeree
    {
        public string Telephone { get; set; }
    }
    public class MemberOfferee : Offeree
    {
        public MemberOfferee(int Member)
        {
            MemberId = Member;
        }
        public int MemberId { get; set; }
    }
    public class GroupOfferee : Offeree
    {
        public string MailingGroup { get; set; }
    }

    public enum OffereeType
    {
        Email,
        Telephone,
        Member
    }
}
