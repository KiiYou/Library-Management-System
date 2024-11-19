using Library_Management_System.Data;
using Library_Management_System.Models;

namespace Library_Management_System.Repositories
{
    public class MemberRepository : IRepository<Member>
    {
        private readonly LibraryContext _context;

        public MemberRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAll() => _context.Members.ToList();
        public Member GetById(int id) => _context.Members.Find(id);
        public void Insert(Member member) { _context.Members.Add(member); }
        public void Update(Member member) { _context.Members.Update(member); }
        public void Delete(int id) { var member = _context.Members.Find(id); if (member != null) { _context.Members.Remove(member); } }
        public void Save() { _context.SaveChanges(); }

        public void CheckPenalty()
        {
            throw new NotImplementedException();
        }

        public void Delete(Checkout checkout)
        {
            throw new NotImplementedException();
        }
    }

}
