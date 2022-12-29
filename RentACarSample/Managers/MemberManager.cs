using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using RentACarSample.Entities;
using RentACarSample.Models;

namespace RentACarSample.Managers
{
    public interface IMemberManager
    {
        void AddMember(RegisterViewModel model);
        Member Authenticate(LoginViewModel model);
    }

    public class MemberManager : IMemberManager
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
            member.Password = model.Password.MD5();

            _databaseContext.Members.Add(member);
            _databaseContext.SaveChanges();
        }

        public Member Authenticate(LoginViewModel model)
        {
            model.Password = model.Password.MD5();

            return _databaseContext.Members.Include(x => x.Roles).FirstOrDefault(
                x => x.Username == model.Username && x.Password == model.Password);
        }
    }
}
