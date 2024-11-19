using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public JsonResult SearchBooks(string term)
        {
            var books = _context.Books
                        .Where(b => b.Title.Contains(term))
                        .Select(b => new { b.BookId, b.Title })
                        .ToList();

            return Json(books);
        }

        [HttpGet]
        public JsonResult GetBookTitles(string term)
        {
            var books = _context.Books
                .Where(b => b.Title.Contains(term))
                .Select(b => new {
                    label = b.Title,
                    value = b.BookId
                }).ToList();
            return Json(books);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var books = from b in _context.Books
                        where b.IsAvailable == true 
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) ||
                                         b.Author.Contains(searchString) ||
                                         b.Genre.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReturnBook(int checkoutId)
        {
            var checkout = await _context.Checkouts.FindAsync(checkoutId);

            if (checkout == null)
            {
                return NotFound();
            }

            checkout.ReturnDate = DateTime.Now;
            _context.Update(checkout);
            await _context.SaveChangesAsync();

            
            return RedirectToAction("CalculatePenalty", "Penalty", new { checkoutId = checkoutId });
        }


        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }

}

