using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class MembersController : Controller
    {
        private readonly LibraryContext _context;

        public MembersController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.ToListAsync();
            return View(members);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Name,Email,MembershipDate")] Member member)
        {
            if (member.Name!=null&&member.Email!=null)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Name,Email,MembershipDate")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (member.Name != null && member.Email != null)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            return View(member);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
        [HttpGet]
        public JsonResult SearchMembers(string term)
        {
            var members = _context.Members
                           .Where(m => m.Name.Contains(term))
                           .Select(m => new { m.MemberId, m.Name })
                           .ToList();

            return Json(members);
        }
        public JsonResult GetMemberNames(string term)
        {
            var members = _context.Members
                .Where(m => m.Name.Contains(term))
                .Select(m => new {
                    label = m.Name,
                    value = m.MemberId
                }).ToList();
            return Json(members);
        }
    }
}
