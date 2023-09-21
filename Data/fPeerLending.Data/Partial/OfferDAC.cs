using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using fPeerLending.Entities;

namespace fPeerLending.Data
{
    /// <summary>
    /// Offers data access component. Manages CRUD operations for the Offers table.
    /// </summary>
    public partial class OfferDAC
    {
        /// <summary>
        /// Conditionally retrieves one or more rows from the Offers table.
        /// </summary>
        /// <returns>A collection of Offer objects.</returns>		
        /// 

        public List<Offer> SelectOffersToMemberId(int MemberId, string OfferType)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.OfferReceipients recepient ON offer.Id =  recepient.OfferId " +
                " WHERE recepient.MemberId = @MemberId" +
                " AND offer.OfferType = @OfferType " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@MemberId", DbType.Int32, MemberId);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }
        public List<Offer> SelectOffersToMemberTelno(string Telno, string OfferType)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.OfferReceipients recepient ON offer.Id =  recepient.OfferId " +
                " WHERE recepient.MemberTelno = @Telno" +
                " AND offer.OfferType = @OfferType " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@Telno", DbType.String, Telno);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }
        public List<Offer> SelectOffersToMemberEmail(string Email, string OfferType)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.OfferReceipients recepient ON offer.Id =  recepient.OfferId " +
                " WHERE recepient.MemberEmail = @Email" +
                " AND offer.OfferType = @OfferType " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@Email", DbType.String, Email);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }
        public List<Offer> SelectOffersToMemberGroup(string Group, string OfferType)
        {
            // Using recursive CTE to get offers made to a group
            const string SQL_STATEMENT =
               //Tree flattening query - Recursive CTE
               //Anchor part
                " WITH FlattenedGroups AS(" +
                " SELECT	*" +
                " FROM	MailingGroups" +
                " WHERE	ShortCode = @Group" +
                " UNION ALL" +
                //Recursive part
                    " SELECT	t.*" +
                    " FROM	MailingGroups t INNER JOIN" +
                    " 	FlattenedGroups r ON t.ParentGroupId = r.GroupId" +
                    " )" +
                //Offer retrieving query using the flattend tree query
                " SELECT o.* FROM dbo.Offers o" +
                " INNER JOIN dbo.OfferReceipients r ON o.Id =  r.OfferId " +
                " inner join FlattenedGroups g on r.MailingGroup = g.GroupId " +
                " WHERE o.OfferType = @OfferType " +
                " AND o.ExpiryDate > @datetoday " +
                " ORDER BY o.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@Group", DbType.String, Group);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }
        


        public List<Offer> SelectOffersByMember(int MemberId, string OfferType)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " WHERE offer.MemberId = @MemberId " +
                " AND offer.OfferType = @OfferType" +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@MemberId", DbType.Int32, MemberId);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }

        public List<Offer> SelectPublicOffers(int MemberId,string offerType)
        {
            return (from v in SelectPublicOffers(MemberId)
                    where v.OfferType.Equals(offerType)
                    select v).ToList();
        }

        public List<Offer> SelectPublicOffers(int MemberId)
        {
            // Give public offerrs not made by member
            const string SQL_STATEMENT =
               " SELECT * FROM dbo.Offers offer " +
               " WHERE offer.PublicOffer = @PublicOffer " +
               " AND offer.MemberId <> @MemberId " +
               " AND offer.ExpiryDate > @datetoday " +
               " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@PublicOffer", DbType.String, "B");
                db.AddInParameter(cmd, "@MemberId", DbType.Int32, MemberId);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }

        public decimal GetAvailableOfferBalance(int id)
        {
            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " WHERE offer.Id = @Id " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            Offer offer = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new Offer
                        offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");
                    }
                }
            }

            return offer.Amount;
        }

        public List<Offer> GetAllMyOffersByMember(string OfferOwner)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.


            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.Members member ON offer.MemberId  = member.MemberId " +
                " WHERE member.Email LIKE '%' + @OfferOwner + '%' " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferOwner", DbType.String, OfferOwner);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }

        public List<Offer> GetAllMyOffersByOfferType(string OfferType)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.


            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.Members member ON offer.MemberId  = member.MemberId " +
                " WHERE offer.OfferType LIKE '%' + @OfferType + '%' " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@OfferType", DbType.String, OfferType);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }

        public List<Offer> GetAllMyOffersByDate(DateTime datefrom, DateTime dateto)
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.


            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.Members member ON offer.MemberId  = member.MemberId " +
                " WHERE offer.CreatedDate BETWEEN  @datefrom  AND  @dateto " +
                " AND offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@datefrom", DbType.Date, datefrom);
                db.AddInParameter(cmd, "@dateto", DbType.Date, dateto);
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }

        public List<Offer> GetAllNonExpiredOffers()
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.


            const string SQL_STATEMENT =
                " SELECT * FROM dbo.Offers offer " +
                " INNER JOIN dbo.Members member ON offer.MemberId  = member.MemberId " +
                " WHERE offer.ExpiryDate > @datetoday " +
                " ORDER BY offer.CreatedDate DESC ";

            List<Offer> result = new List<Offer>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@datetoday", DbType.Date, DateTime.Now.Date);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Offer
                        Offer offer = new Offer();

                        // Read values.
                        offer.Id = base.GetDataValue<int>(dr, "Id");
                        offer.OfferType = base.GetDataValue<string>(dr, "OfferType");
                        offer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        offer.Description = base.GetDataValue<string>(dr, "Description");
                        offer.Offerees = base.GetDataValue<string>(dr, "Offerees");
                        offer.ExpiryDate = base.GetDataValue<DateTime>(dr, "ExpiryDate");
                        offer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        offer.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        offer.Term = base.GetDataValue<int>(dr, "Term");
                        offer.Interest = base.GetDataValue<double>(dr, "Interest");
                        offer.Status = base.GetDataValue<string>(dr, "Status");
                        offer.PublicOffer = base.GetDataValue<string>(dr, "PublicOffer");
                        offer.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");

                        // Add to List.
                        result.Add(offer);
                    }
                }
            }

            return result;
        }








    }
}