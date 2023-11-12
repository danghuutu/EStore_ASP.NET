using BusinessObject;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMember();
        Member GetMemberByID(int memberID);
        void InsertMember(Member member);
        void DeleteMember(int memberID);
        void UpdateMember(Member member);
    }
}
