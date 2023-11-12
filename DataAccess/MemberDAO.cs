using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        //-----------------------------------------------------------------
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                {
                    lock (instanceLock)
                        if (instance == null)
                        {
                            instance = new MemberDAO();
                        }
                    return instance;
                }
            }
        }
        //-----------------------------------------------------------------
        public IEnumerable<Member> GetMemberList()
        {
            var members = new List<Member>();
            try
            {
                using var context = new EstoreContext();
                members = context.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }
        //-----------------------------------------------------------------
        public Member GetMemberByID(int memberID)
        {
            Member member = null;
            try
            {
                using var context = new EstoreContext();
                member = context.Members.SingleOrDefault(m => m.MemberId == memberID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }
        //-----------------------------------------------------------------
        //Add new a car
        public void AddNew(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member == null)
                {
                    using var context = new EstoreContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Update a car
        public void Update(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member != null)
                {
                    using var context = new EstoreContext();
                    context.Members.Update(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Remove a car
        public void Remove(int memberID)
        {
            try
            {
                Member member = GetMemberByID(memberID);
                if (member != null)
                {
                    using var context = new EstoreContext();
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
