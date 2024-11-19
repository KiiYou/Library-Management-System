using Library_Management_System.Data;
using Library_Management_System.Models;

namespace Library_Management_System.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll() => _context.Books.ToList();
        public Book GetById(int id) => _context.Books.Find(id);
        public void Insert(Book book) { _context.Books.Add(book); }
        public void Update(Book book) { _context.Books.Update(book); }
        public void Delete(int id) { var book = _context.Books.Find(id); if (book != null) { _context.Books.Remove(book); } }
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
