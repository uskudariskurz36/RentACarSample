using RentACarSample.Entities;
using RentACarSample.Models;

namespace RentACarSample.Managers
{
    public interface IMemberManager
    {
        void AddMember(RegisterViewModel model);
    }

    public class MemberManager: IMemberManager
    {
        private DatabaseContext _databaseContext;

        public MemberManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddMember(RegisterViewModel model)
        {
            Member member = new Member();
            member.Username = model.Username;
            member.Password = model.Password;

            _databaseContext.Members.Add(member);
            _databaseContext.SaveChanges();
        }
    }
}
