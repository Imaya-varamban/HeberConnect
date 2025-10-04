using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace HeberConnect.Pages.Staff
{
    public class AssignmentsListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Assignment> Assignments { get; set; }

        public void OnGet()
        {
            Assignments = _context.Assignments
                .OrderBy(a => a.Deadline)
                .ToList();
        }
    }
}
