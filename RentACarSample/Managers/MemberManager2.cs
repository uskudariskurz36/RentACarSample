using RentACarSample.Entities;
using RentACarSample.Models;

namespace RentACarSample.Managers
{
    public class MemberManager2 : IMemberManager
    {
        private DatabaseContext _databaseContext;

        public MemberManager2(DatabaseContext databaseContext)
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

        public Member Authenticate(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
