using Library_Management_System.Models;
using Library_Management_System.Repositories;

namespace Library_Management_System.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();
        public Book GetBookById(int id) => _bookRepository.GetById(id);
        public void AddBook(Book book) { _bookRepository.Insert(book); _bookRepository.Save(); }
        public void UpdateBook(Book book) { _bookRepository.Update(book); _bookRepository.Save(); }
        public void DeleteBook(int id) { _bookRepository.Delete(id); _bookRepository.Save(); }
    }

}
