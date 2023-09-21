using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fPeerLending.Data;
using fPeerLending.Entities;

namespace fPeerLending.Business
{
    public class MailingGroupsComponent
    {
        public MailingGroup CreateMailingGroup(MailingGroup mg)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.Create(mg);
        }
        public MailingGroup CreateMailingGroup(int member, string parent, string groupname)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            if (GroupExists(parent, member)) //parent exists
            {
                // create it
                //MailingGroup mg = default(MailingGroup);
                MailingGroup mg = new MailingGroup();
                mg.Creator = member;
                mg.CreatedOn = DateTime.Today;
                mg.LastModified = DateTime.Today;
                mg.ShortCode = groupname;
                mg.ParentGroupId = GetMailingGroup(parent, member).GroupId;
                return mDac.Create(mg);
            }
            else if (parent.Equals("ROOT"))
            {
                //Create group and attach to root
                //Create root
                //MailingGroup rt = default(MailingGroup);
                MailingGroup rt = new MailingGroup();
                rt.Creator = member;
                rt.CreatedOn = DateTime.Today;
                rt.LastModified = DateTime.Today;
                rt.ShortCode = "ROOT";
                rt.ParentGroupId = 0;
                MailingGroup root = mDac.Create(rt);

                //MailingGroup mg = default(MailingGroup);
                MailingGroup mg = new MailingGroup();
                mg.Creator = member;
                mg.CreatedOn = DateTime.Today;
                mg.LastModified = DateTime.Today;
                mg.ShortCode = groupname;
                mg.ParentGroupId = root.GroupId;
                return mDac.Create(mg);
            }
            else
            {
                // the parent does not exist also
                throw new ArgumentNullException("Parent", "Parent group does [" + parent + "] not exist");
            }
        }
        public void CreateRootMailingGroup(int member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();

            //Create root 
            MailingGroup rt = new MailingGroup();
            rt.Creator = member;
            rt.CreatedOn = DateTime.Today;
            rt.LastModified = DateTime.Today;
            rt.ShortCode = "ROOT";
            rt.ParentGroupId = 0;
            MailingGroup root = mDac.Create(rt);
        }
        public void UpdateMailingGroup(MailingGroup mg)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
             mDac.UpdateById(mg);
        }
        public MailingGroup GetMailingGroupById(int id)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.SelectById(id);
        }
        public MailingGroup GetOrCreateMailingGroup(int member, string parent, string groupname)
        {
            if (GroupExists(groupname, member)) 
                return GetMailingGroup(groupname, member);
            return CreateMailingGroup(member, parent, groupname);
        }
        
        public bool GroupExists(string groupname)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.CountByShortCode(groupname) > 0;
        }
        public bool GroupExists(string groupname, int memberId)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.CountByShortCodeMember(groupname,memberId) > 0;
        }
        public MailingGroup GetMailingGroup(string shortCode, int Member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.SelectByShortCodeMember(shortCode, Member);
        }
        public bool IsRoot(string shortCode, int member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            MailingGroup grp = mDac.SelectByShortCodeMember(shortCode, member);
            return grp.ParentGroupId == 0;
        }
        public MailingGroupMember CreateMailingGroupMember(MailingGroupMember mg)
        {
            MailingGroupMemberDAC mmDac = new MailingGroupMemberDAC();
            return mmDac.Create(mg);
        }

        public List<MailingGroup> GetMailingGroupsTreeView(int member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();

            List<MailingGroup> rootNodes = (from mg in mDac.Select()
                                           where mg.Creator == member
                                           select new MailingGroup()
                                             {
                                                 ShortCode = mg.ShortCode,
                                                 CreatedOn = mg.CreatedOn,
                                                 Creator = mg.Creator,
                                                 GroupId = mg.GroupId,
                                                 LastModified = mg.LastModified,
                                                 ParentGroupId = mg.ParentGroupId
                                             }).ToList();

            foreach (var rootNode in rootNodes)
            {
                BuildChildNode(rootNode);
            }

            return rootNodes;
        }

        public MailingGroup GetTreeViewList(int member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();

            MailingGroup rootNode = (from mg in mDac.Select()
                                     where mg.Creator == member
                                     where mg.ParentGroupId == 0
                                     select new MailingGroup()
                                     {
                                         ShortCode = mg.ShortCode,
                                         CreatedOn = mg.CreatedOn,
                                         Creator = mg.Creator,
                                         GroupId = mg.GroupId,
                                         LastModified = mg.LastModified,
                                         ParentGroupId = mg.ParentGroupId
                                     }).SingleOrDefault();
            BuildChildNode(rootNode);

            return rootNode;
        }

        private void BuildChildNode(MailingGroup rootNode)
        {
            if (rootNode != null)
            {
                MailingGroupDAC mDac = new MailingGroupDAC();

                List<MailingGroup> chidNode = (from mg in mDac.Select()
                                                 where mg.ParentGroupId == rootNode.GroupId
                                               select new MailingGroup()
                                                 { 
                                                     ShortCode = mg.ShortCode,
                                                     CreatedOn = mg.CreatedOn,
                                                     Creator = mg.Creator,
                                                     GroupId = mg.GroupId,
                                                     LastModified = mg.LastModified,
                                                     ParentGroupId = mg.ParentGroupId
                                                 }).ToList<MailingGroup>();
                if (chidNode.Count > 0)
                {
                    foreach (var childRootNode in chidNode)
                    {
                        BuildChildNode(childRootNode);
                        rootNode.ChildNode.Add(childRootNode);
                    }
                }

            }
        }
        public List<MailingGroup> GetMemberMailingGroups(int member)
        {
            MailingGroupDAC mDac = new MailingGroupDAC();
            return mDac.SelectByCreator(member);
        }




    }
}
