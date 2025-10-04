using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using HeberConnect.Models;

namespace HeberConnect.Pages.Dashboard
{
    [Authorize(Roles = "Student")]
    public class StudentOnlyModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        //  Constructor name must match the class name
        public StudentOnlyModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Announcement> Announcements { get; set; }

        public void OnGet()
        {
            Announcements = _context.Announcements
                                   .OrderByDescending(a => a.CreatedOn)
                                   .Take(5)
                                   .ToList();
        }
    }
}
