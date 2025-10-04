using HeberConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HeberConnect.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int PendingComplaints { get; set; }

        public async Task OnGetAsync()
        {
            PendingComplaints = await _context.Complaints
                .CountAsync(c => c.Status == "Pending");
        }
    }
}
