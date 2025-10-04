using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeberConnect.Pages.Staff
{
    public class ReviewODModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewODModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ODRequestView> Requests { get; set; }

        public void OnGet()
        {
            Requests = _context.ODRequests
                .Select(r => new ODRequestView
                {
                    Id = r.Id,
                    StudentId = r.StudentId,
                    StudentNumber = _context.Users
                                      .Where(u => u.Id == r.StudentId)
                                      .Select(u => u.UserName) // or StudentNumber column if exists
                                      .FirstOrDefault(),
                    Date = r.Date,
                    RequestType = r.RequestType,
                    Hour = r.Hour,
                    Reason = r.Reason,
                    Status = r.Status
                }).ToList();
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var req = await _context.ODRequests.FindAsync(id);
            if (req != null)
            {
                req.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var req = await _context.ODRequests.FindAsync(id);
            if (req != null)
            {
                req.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public class ODRequestView : ODRequest
        {
            public string StudentNumber { get; set; }
        }
    }
}
