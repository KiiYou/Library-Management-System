using Library_Management_System.Data;
using Library_Management_System.Models;
using Library_Management_System.Repositories;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<Checkout> _checkoutRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly LibraryContext _context;

        public CheckoutController(IBookService bookService, IRepository<Member> memberRepository, IRepository<Checkout> checkoutRepository,LibraryContext libraryContext,IRepository<Book> bookRepository)
        {
            _bookService = bookService;
            _memberRepository = memberRepository;
            _checkoutRepository = checkoutRepository;
            _bookRepository = bookRepository;
            _context = libraryContext;
        }

        public IActionResult Create()
        {
            ViewBag.Books = _bookService.GetAllBooks();
            ViewBag.Members = _memberRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Checkout checkout)
        {
            if (checkout.MemberId!=null&&checkout.BookId!=null)
            {
                if (checkout.DueDate <= checkout.CheckoutDate)
                {
                    ModelState.AddModelError("DueDate", "Due date must be after the checkout date.");
                    return View(checkout);
                }
                _checkoutRepository.Insert(checkout);
                int b = checkout.BookId;
                var boo=_bookRepository.GetById(b);
                boo.IsAvailable = false;
                _checkoutRepository.Save();
                _bookRepository.Update(boo);
                _bookRepository.Save();
                return RedirectToAction("Index");
            }

            return View(checkout);
        }


            public IActionResult Index()
            {
                _checkoutRepository.CheckPenalty(); 

                var checkouts = _checkoutRepository.GetAll(); 
                return View(checkouts); 
            }

            [HttpPost]
            public IActionResult Return(int id)
            {
                var checkout = _checkoutRepository.GetById(id);
                if (checkout == null)
                {
                    return NotFound();
                }

                checkout.ReturnDate = DateTime.Now;
                checkout.IsReturned = true;
                int b = checkout.BookId;
                var boo = _bookRepository.GetById(b);
                boo.IsAvailable = true;
                _checkoutRepository.Update(checkout);
                _checkoutRepository.Save();
                _bookRepository.Update(boo);
                _bookRepository.Save();

            return RedirectToAction("Index");
            }

            public async Task<IActionResult> Penalty(int checkoutId)
            {
                if (checkoutId == 0)
                {
                    return BadRequest("Invalid Checkout ID.");
                }

                var checkout = await _context.Checkouts
                    .Include(c => c.Book)
                    .FirstOrDefaultAsync(c => c.CheckoutId == checkoutId);

                if (checkout == null || checkout.ReturnDate == null)
                {
                    return NotFound("Checkout not found or book not returned.");
                }

                if (checkout.IsOverdue)
                {
                    int overdueDays = (checkout.ReturnDate.Value - checkout.DueDate).Days;
                    decimal penaltyAmount = overdueDays * 5.25m; 

                    var penalty = new Penalty
                    {
                        CheckoutId = checkout.CheckoutId,
                        Amount = penaltyAmount,
                        PenaltyDate = DateTime.Now
                    };

                    _context.Penalties.Add(penalty);
                    await _context.SaveChangesAsync();

                    ViewData["Message"] = $"Penalty calculated: {penaltyAmount} for {overdueDays} overdue days.";
                }
                else
                {
                    ViewData["Message"] = "No penalty. Book was returned on time.";
                }

                return View(checkout);
            }
        [HttpPost]
        public IActionResult ClearAllCheckouts()
        {
            var checkouts = _checkoutRepository.GetAll();
            if (checkouts.Any())
            {
                foreach (var checkout in checkouts)
                {
                    _checkoutRepository.Delete(checkout);
                }

                _checkoutRepository.Save();
                TempData["Message"] = "All checkout records have been cleared.";
            }
            else
            {
                TempData["Message"] = "No checkout records to clear.";
            }

            return RedirectToAction("Index");
        }



    }

}
