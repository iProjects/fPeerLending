using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fPeerLending.Entities;
using fPeerLending.Data;

namespace fPeerLending.Business
{
    public class MemberSettingsComponent
    {
        public void Set(int member, string key)
        {
            MemberSettingDAC mdac = new MemberSettingDAC();
            MemberSetting setting = new MemberSetting();
            setting.Member = member;
            setting.SSKey = key;
            mdac.Create(setting);
        }

        public MemberSetting get(int member, string key)
        {
            MemberSettingDAC mdac = new MemberSettingDAC();
            return mdac.SelectById(member, key);
        }

        public List<MemberSetting> getByMemmber(int member)
        {
            MemberSettingDAC mdac = new MemberSettingDAC();
            return mdac.SelectByMember(member);
        }
    }
}
