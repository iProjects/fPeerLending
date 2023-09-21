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
    public partial class MailingGroupMemberDAC
    {
        public List<MailingGroupMember> SelectFlattenedGroupMembers(string ShortCode)
        {
                 // Using recursive CTE to get offers made to a group
            const string SQL_STATEMENT =
               //Tree flattening query - Recursive CTE
               //Anchor part
                " WITH FlattenedGroups AS(" +
                " SELECT	*" +
                " FROM	MailingGroups" +
                " WHERE	ShortCode = @ShortCode" +
                " UNION ALL" +
                //Recursive part
                    " SELECT	t.*" +
                    " FROM	MailingGroups t INNER JOIN" +
                    " 	FlattenedGroups r ON t.ParentGroupId = r.GroupId" +
                    " )" +
                " SELECT mgm.* " +
                " FROM dbo.MailingGroupMembers mgm " +
                " INNER JOIN FlattenedGroups g on mgm.GroupId = g.GroupId";

            List<MailingGroupMember> result = new List<MailingGroupMember>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@ShortCode", DbType.String, ShortCode);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new MailingGroupMember
                        MailingGroupMember mailingGroupMember = new MailingGroupMember();

                        // Read values.
                        mailingGroupMember.Id = base.GetDataValue<int>(dr, "Id");
                        mailingGroupMember.GroupId = base.GetDataValue<int>(dr, "GroupId");
                        mailingGroupMember.Member = base.GetDataValue<int>(dr, "Member");

                        // Add to List.
                        result.Add(mailingGroupMember);
                    }
                }
            }

            return result;
        }
    }
}
