using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HeberConnect.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class AnnouncementsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Announcement> Announcements { get; set; } = new();

        public async Task OnGetAsync()
        {
            Announcements = await _context.Announcements
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }
    }
}
