using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeberConnect.Pages.Admin
{
    public class ReviewComplaintModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewComplaintModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Complaint> Complaints { get; set; } = new List<Complaint>();

        public void OnGet()
        {
            Complaints = _context.Complaints.ToList();
        }

        public async Task<IActionResult> OnPostResolveAsync(int id)
        {
            var c = _context.Complaints.Find(id);
            if (c != null) { c.Status = "Resolved"; await _context.SaveChangesAsync(); }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var c = _context.Complaints.Find(id);
            if (c != null) { c.Status = "Rejected"; await _context.SaveChangesAsync(); }
            return RedirectToPage();
        }
    }
}
