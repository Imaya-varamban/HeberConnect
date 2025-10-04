using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeberConnect.Pages.Student
{
    public class SubmitComplaintModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SubmitComplaintModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Complaint Complaint { get; set; }

        public List<Complaint> MyComplaints { get; set; } = new List<Complaint>();

        public void OnGet()
        {
            var studentId = _userManager.GetUserId(User);
            MyComplaints = _context.Complaints.Where(c => c.StudentId == studentId).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var studentId = _userManager.GetUserId(User);
            Complaint.StudentId = studentId;
            Complaint.Date = System.DateTime.Now;

            _context.Complaints.Add(Complaint);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
