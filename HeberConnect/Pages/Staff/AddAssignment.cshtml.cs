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

namespace HeberConnect.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class AddAssignmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddAssignmentModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = new Assignment();

        public string StatusMessage { get; set; } = string.Empty;
        public List<Assignment> MyAssignments { get; set; } = new List<Assignment>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            MyAssignments = _context.Assignments
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
            Assignment.CreatedBy = user?.Email ?? "Unknown";
            Assignment.CreatedOn = DateTime.Now;

            _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();

            StatusMessage = "Assignment added successfully!";
            await OnGetAsync(); // Refresh history
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
                StatusMessage = "Assignment deleted successfully!";
            }
            await OnGetAsync(); // refresh the list
            return Page();
        }

    }
}
