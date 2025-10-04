using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HeberConnect.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Assignment> Assignments { get; set; } = new();
        public bool HasNewAnnouncements { get; set; }

        public async Task OnGetAsync()
        {
            // Upcoming assignments
            Assignments = await _context.Assignments
                .OrderBy(a => a.Deadline)
                .Take(5)
                .ToListAsync();

            // "New" announcement logic → any created in last 2 days
            HasNewAnnouncements = await _context.Announcements
                .AnyAsync(a => a.CreatedOn >= DateTime.Now.AddDays(-2));
        }
    }
}
