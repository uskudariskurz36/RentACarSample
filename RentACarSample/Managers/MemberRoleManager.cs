using RentACarSample.Common;
using RentACarSample.Entities;

namespace RentACarSample.Managers
{
    public interface IMemberRoleManager
    {
        void AddMemberRole(int memberId, string roleName);
        void RemoveMemberRole(int memberRoleId);
        void RemoveMemberRole(string roleName, int memberId);
        List<MemberRole> GetRolesByMemberId(int memberId);
        void RemoveAllMemberRoles(int memberId);
    }

    public class MemberRoleManager : IMemberRoleManager
    {
        private DatabaseContext _databaseContext;

        public MemberRoleManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddMemberRole(int memberId, string roleName)
        {
            if (_databaseContext.MemberRoles.Any(x => x.MemberId == memberId && x.Name == roleName))
                return;

            MemberRole memberRole = new MemberRole();
            memberRole.MemberId = memberId;
            memberRole.Name = roleName;

            _databaseContext.MemberRoles.Add(memberRole);
            _databaseContext.SaveChanges();
        }

        public void RemoveMemberRole(int memberRoleId)
        {
            MemberRole memberRole =
                _databaseContext.MemberRoles.Find(memberRoleId);

            if (memberRole == null) return;

            _databaseContext.MemberRoles.Remove(memberRole);
            _databaseContext.SaveChanges();
        }

        public void RemoveMemberRole(string roleName, int memberId)
        {
            MemberRole memberRole =
                _databaseContext.MemberRoles
                    .Where(x => x.MemberId == memberId && x.Name == roleName)
                    .FirstOrDefault();

            if (memberRole == null) return;

            _databaseContext.MemberRoles.Remove(memberRole);
            _databaseContext.SaveChanges();
        }

        public List<MemberRole> GetRolesByMemberId(int memberId)
        {
            return _databaseContext.MemberRoles.Where(x => x.MemberId == memberId).ToList();
        }

        public void RemoveAllMemberRoles(int memberId)
        {
            List<MemberRole> memberRoles = _databaseContext.MemberRoles.Where(x => x.MemberId == memberId).ToList();

            foreach (MemberRole role in memberRoles)
            {
                _databaseContext.MemberRoles.Remove(role);
            }

            _databaseContext.SaveChanges();
        }
    }
}
