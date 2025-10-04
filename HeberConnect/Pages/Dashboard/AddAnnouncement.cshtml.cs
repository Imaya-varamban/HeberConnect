using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeberConnect.Pages.Dashboard
{
    [Authorize(Roles = "Staff,Admin")]
    public class AddAnnouncementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddAnnouncementModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Announcement Announcement { get; set; } = new Announcement();

        public string StatusMessage { get; set; } = string.Empty;
        public List<Announcement> MyAnnouncements { get; set; } = new List<Announcement>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            MyAnnouncements = _context.Announcements
                .Where(a => a.CreatedBy == user.Email)
                .OrderByDescending(a => a.CreatedOn)
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid input!";
                await OnGetAsync();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Announcement.CreatedBy = user?.Email ?? "Unknown";
            Announcement.CreatedOn = DateTime.Now;

            _context.Announcements.Add(Announcement);
            await _context.SaveChangesAsync();

            StatusMessage = "Announcement posted successfully!";
            await OnGetAsync(); // Refresh list
            return Page();
        }
    }
}
