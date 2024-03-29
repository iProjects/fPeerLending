﻿//====================================================================================================
// Code generated with Momentum: DAC Gen (Build 2.5.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 08/16/2013 13:32:23 
//====================================================================================================


using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using fPeerLending.Entities;

namespace fPeerLending.Data
{
    /// <summary>
    /// Members data access component. Manages CRUD operations for the Members table.
    /// </summary>
    public partial class MemberDAC
    {


        public bool IsPhoneRegistered(string Telephone)
        {
            return CountByTelephone(Telephone) > 0;
        }

        public bool IsEmailRegistered(string Email)
        {
            return CountByEmail(Email) > 0;
        }

        public bool IsNationalIDRegistered(string NationalID)
        {
            return CountByNationalID(NationalID) > 0;
        }
        public bool IsRegistered(string NationalID, string Email, string Telephone)
        {
            return CountByTelephoneEmailNationalID(Telephone, Email, NationalID) > 0;
        }
        /// <summary>
        /// Conditionally retrieves one or more rows from the Members table.
        /// </summary>
        /// <returns>A collection of Member objects.</returns>		
        public List<Member> SelectOpenMembers()
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                "SELECT * FROM dbo.Members " +
                "WHERE [Status]= 'N' ";

            List<Member> result = new List<Member>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Member
                        Member member = new Member();

                        // Read values.
                        member.MemberId = base.GetDataValue<int>(dr, "MemberId");
                        member.Email = base.GetDataValue<string>(dr, "Email");
                        member.Surname = base.GetDataValue<string>(dr, "Surname");
                        member.OtherNames = base.GetDataValue<string>(dr, "OtherNames");
                        member.DateOfBirth = base.GetDataValue<DateTime>(dr, "DateOfBirth");
                        member.Gender = base.GetDataValue<string>(dr, "Gender");
                        member.Telephone = base.GetDataValue<string>(dr, "Telephone");
                        member.DateJoined = base.GetDataValue<DateTime>(dr, "DateJoined");
                        member.CustomerId = base.GetDataValue<int>(dr, "CustomerId");
                        member.CurrentAccountId = base.GetDataValue<int>(dr, "CurrentAccountId");
                        member.LoanAccountId = base.GetDataValue<int>(dr, "LoanAccountId");
                        member.InvestmentAccountId = base.GetDataValue<int>(dr, "InvestmentAccountId");
                        member.Status = base.GetDataValue<string>(dr, "Status");
                        member.DateActivated = base.GetDataValue<DateTime>(dr, "DateActivated");
                        member.RefferedBy = base.GetDataValue<int>(dr, "RefferedBy");
                        member.InformBy = base.GetDataValue<string>(dr, "InformBy");
                        member.Photo = base.GetDataValue<string>(dr, "Photo");
                        member.MaxRecordsToDisplay = base.GetDataValue<int>(dr, "MaxRecordsToDisplay");


                        // Add to List.
                        result.Add(member);
                    }
                }
            }

            return result;
        }

        public void UploadMemberImage(Member member)
        {
            const string SQL_STATEMENT =
                "UPDATE dbo.Members " +
                "SET " +
                    "[Email]=@Email, " +
                    "[Surname]=@Surname, " +
                    "[OtherNames]=@OtherNames, " +
                    "[DateOfBirth]=@DateOfBirth, " +
                    "[Gender]=@Gender, " +
                    "[Telephone]=@Telephone, " +
                    "[DateJoined]=@DateJoined, " +
                    "[CustomerId]=@CustomerId, " +
                    "[CurrentAccountId]=@CurrentAccountId, " +
                    "[LoanAccountId]=@LoanAccountId, " +
                    "[InvestmentAccountId]=@InvestmentAccountId, " +
                    "[Status]=@Status, " +
                    "[Photo]=@Photo, " +
                    "[DateActivated]=@DateActivated, " +
                    "[RefferedBy]=@RefferedBy, " +
                    "[InformBy]=@InformBy, " +
                    "[MaxRecordsToDisplay]=@MaxRecordsToDisplay " +
                "WHERE [MemberId]=@MemberId ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@Email", DbType.String, member.Email);
                db.AddInParameter(cmd, "@Surname", DbType.String, member.Surname);
                db.AddInParameter(cmd, "@OtherNames", DbType.String, member.OtherNames);
                db.AddInParameter(cmd, "@DateOfBirth", DbType.DateTime2, member.DateOfBirth);
                db.AddInParameter(cmd, "@Gender", DbType.AnsiString, member.Gender);
                db.AddInParameter(cmd, "@Telephone", DbType.String, member.Telephone);
                db.AddInParameter(cmd, "@DateJoined", DbType.Date, member.DateJoined);
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, member.CustomerId);
                db.AddInParameter(cmd, "@CurrentAccountId", DbType.Int32, member.CurrentAccountId);
                db.AddInParameter(cmd, "@LoanAccountId", DbType.Int32, member.LoanAccountId);
                db.AddInParameter(cmd, "@InvestmentAccountId", DbType.Int32, member.InvestmentAccountId);
                db.AddInParameter(cmd, "@Status", DbType.AnsiString, member.Status);
                db.AddInParameter(cmd, "@DateActivated", DbType.DateTime2, member.DateActivated);
                db.AddInParameter(cmd, "@RefferedBy", DbType.Int32, member.RefferedBy);
                db.AddInParameter(cmd, "@InformBy", DbType.String, member.InformBy);
                db.AddInParameter(cmd, "@Photo", DbType.String, member.Photo);
                db.AddInParameter(cmd, "@MemberId", DbType.Int32, member.MemberId);
                db.AddInParameter(cmd, "@MaxRecordsToDisplay", DbType.Int32, member.MaxRecordsToDisplay);

                db.ExecuteNonQuery(cmd);
            }
        }








    }
}
