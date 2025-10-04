using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HeberConnect.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Announcement> Announcements { get; set; } = new List<Announcement>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();

        public async Task OnGetAsync()
        {
            // Day order and date are already handled in view via DateTime.Now
            var user = await _userManager.GetUserAsync(User);
            string userEmail = user?.Email ?? "";

            // Load only announcements created by this staff
            Announcements = await _context.Announcements
                .Where(a => a.CreatedBy == userEmail)
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();

            // Load only assignments created by this staff
            Assignments = await _context.Assignments
                .Where(a => a.CreatedBy == userEmail)
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }
    }
}
