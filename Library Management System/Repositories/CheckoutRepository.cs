using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public class CheckoutRepository : IRepository<Checkout>
    {
        private readonly LibraryContext _context;

        public CheckoutRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts
                .Include(c => c.Book) 
                .Include(c => c.Member) 
                .ToList();              
        }

        public Checkout GetById(int id) => _context.Checkouts.Find(id);
        public void Insert(Checkout checkout) { _context.Checkouts.Add(checkout); }
        public void Update(Checkout checkout) { _context.Checkouts.Update(checkout); }
        public void Delete(int id) { var checkout = _context.Checkouts.Find(id); if (checkout != null) { _context.Checkouts.Remove(checkout); } }
        public void Save() { _context.SaveChanges(); }
        public void CheckPenalty()
        {
            var checkouts = _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Member)
                .ToList();

            var datenow = DateTime.Now;

            foreach (var checkout in checkouts)
            {
                if (checkout.ReturnDate == null)
                {
                    if (checkout.DueDate < datenow)
                    {
                        checkout.IsPenality = true;
                    }
                    else
                    {
                        checkout.IsPenality = false;
                    }
                }
                else
                {
                    if (checkout.ReturnDate > checkout.DueDate)
                    {
                        checkout.IsPenality = true;
                    }
                    else
                    {
                        checkout.IsPenality = false;
                    }
                }
            }

            _context.SaveChanges();
        }
        public void Delete(Checkout checkout)
        {
            _context.Checkouts.Remove(checkout);
        }

    }

}
