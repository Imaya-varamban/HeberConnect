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
    public class ApplyODModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplyODModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ODRequest ODRequest { get; set; }

        public List<ODRequest> MyRequests { get; set; } = new List<ODRequest>();

        public void OnGet()
        {
            var studentId = _userManager.GetUserId(User);
            MyRequests = _context.ODRequests.Where(r => r.StudentId == studentId).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var studentId = _userManager.GetUserId(User);
            ODRequest.StudentId = studentId;

            _context.ODRequests.Add(ODRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
