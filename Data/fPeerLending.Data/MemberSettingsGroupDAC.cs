//====================================================================================================
// Base code generated with Momentum: DAC Gen (Build 2.5.5049.15162)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by francis.muraya at KPC0201M on 11/17/2014 14:47:55 
//====================================================================================================

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
    /// MemberSettingsGroups data access component. Manages CRUD operations for the MemberSettingsGroups table.
    /// </summary>
    public partial class MemberSettingsGroupDAC : DataAccessComponent
    {
        /// <summary>
        /// Inserts a new row in the MemberSettingsGroups table.
        /// </summary>
        /// <param name="memberSettingsGroup">A MemberSettingsGroup object.</param>
        /// <returns>An updated MemberSettingsGroup object.</returns>
        public MemberSettingsGroup Create(MemberSettingsGroup memberSettingsGroup)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.MemberSettingsGroups ([GroupName], [Parent]) " +
                "VALUES(@GroupName, @Parent); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@GroupName", DbType.AnsiString, memberSettingsGroup.GroupName);
                db.AddInParameter(cmd, "@Parent", DbType.Int32, memberSettingsGroup.Parent);

                // Get the primary key value.
                memberSettingsGroup.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return memberSettingsGroup;
        }

        /// <summary>
        /// Updates an existing row in the MemberSettingsGroups table.
        /// </summary>
        /// <param name="memberSettingsGroup">A MemberSettingsGroup entity object.</param>
        public void UpdateById(MemberSettingsGroup memberSettingsGroup)
        {
            const string SQL_STATEMENT =
                "UPDATE dbo.MemberSettingsGroups " +
                "SET " +
                    "[GroupName]=@GroupName, " +
                    "[Parent]=@Parent " +
                "WHERE [Id]=@Id ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@GroupName", DbType.AnsiString, memberSettingsGroup.GroupName);
                db.AddInParameter(cmd, "@Parent", DbType.Int32, memberSettingsGroup.Parent);
                db.AddInParameter(cmd, "@Id", DbType.Int32, memberSettingsGroup.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Conditionally deletes one or more rows in the MemberSettingsGroups table.
        /// </summary>
        /// <param name="id">A id value.</param>
        public void DeleteById(int id)
        {
            const string SQL_STATEMENT = "DELETE dbo.MemberSettingsGroups " +
                                         "WHERE [Id]=@Id ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);


                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Returns a row from the MemberSettingsGroups table.
        /// </summary>
        /// <param name="id">A Id value.</param>
        /// <returns>A MemberSettingsGroup object with data populated from the database.</returns>
        public MemberSettingsGroup SelectById(int id)
        {
            const string SQL_STATEMENT =
                "SELECT [Id], [GroupName], [Parent] " +
                "FROM dbo.MemberSettingsGroups  " +
                "WHERE [Id]=@Id ";

            MemberSettingsGroup memberSettingsGroup = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new MemberSettingsGroup
                        memberSettingsGroup = new MemberSettingsGroup();

                        // Read values.
                        memberSettingsGroup.Id = base.GetDataValue<int>(dr, "Id");
                        memberSettingsGroup.GroupName = base.GetDataValue<string>(dr, "GroupName");
                        memberSettingsGroup.Parent = base.GetDataValue<int>(dr, "Parent");
                    }
                }
            }

            return memberSettingsGroup;
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the MemberSettingsGroups table.
        /// </summary>
        /// <returns>A collection of MemberSettingsGroup objects.</returns>		
        public List<MemberSettingsGroup> Select()
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                "SELECT [Id], [GroupName], [Parent] " +
                "FROM dbo.MemberSettingsGroups ";

            List<MemberSettingsGroup> result = new List<MemberSettingsGroup>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new MemberSettingsGroup
                        MemberSettingsGroup memberSettingsGroup = new MemberSettingsGroup();

                        // Read values.
                        memberSettingsGroup.Id = base.GetDataValue<int>(dr, "Id");
                        memberSettingsGroup.GroupName = base.GetDataValue<string>(dr, "GroupName");
                        memberSettingsGroup.Parent = base.GetDataValue<int>(dr, "Parent");

                        // Add to List.
                        result.Add(memberSettingsGroup);
                    }
                }
            }

            return result;
        }
    }
}

