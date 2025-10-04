using HeberConnect.Data;
using HeberConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeberConnect.Pages.Student
{
    public class AssignmentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AssignmentsModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<Assignment> Assignments { get; set; }

        public void OnGet()
        {
            Assignments = _context.Assignments
                .OrderBy(a => a.Deadline)
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile File, int AssignmentId)
        {
            if (File != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, "uploads", File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }

                var submission = new StudentAssignment
                {
                    AssignmentId = AssignmentId,
                    StudentId = User.Identity?.Name,
                    FilePath = "/uploads/" + File.FileName,
                    SubmittedOn = DateTime.Now
                };

                _context.StudentAssignments.Add(submission);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
