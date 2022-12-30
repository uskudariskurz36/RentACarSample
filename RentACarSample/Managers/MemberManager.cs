using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using RentACarSample.Entities;
using RentACarSample.Models;

namespace RentACarSample.Managers
{
    public interface IMemberManager
    {
        Member AddMember(RegisterViewModel model);
        Member Authenticate(LoginViewModel model);
        int? GetIdByUsername(string username);
        List<Member> List();
    }

    public class MemberManager : IMemberManager
    {
        private DatabaseContext _databaseContext;

        public MemberManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Member AddMember(RegisterViewModel model)
        {
            if (_databaseContext.Members.Any(x => x.Username.ToLower() == model.Username.ToLower()))
            {
                throw new Exception("Kullanıcı adı sistem de mevcut.");
            }

            Member member = new Member();
            member.Username = model.Username;
            member.Password = model.Password.MD5();

            _databaseContext.Members.Add(member);
            _databaseContext.SaveChanges();

            return member;
        }

        public int? GetIdByUsername(string username)
        {
            Member member = _databaseContext.Members.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
            return member.Id;
        }

        public Member Authenticate(LoginViewModel model)
        {
            model.Password = model.Password.MD5();

            return _databaseContext.Members.Include(x => x.Roles).FirstOrDefault(
                x => x.Username == model.Username && x.Password == model.Password);
        }

        public List<Member> List()
        {
            return _databaseContext.Members.ToList();
        }
    }
}
