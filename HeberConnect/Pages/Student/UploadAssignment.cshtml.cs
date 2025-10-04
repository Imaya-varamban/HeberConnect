using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace HeberConnect.Pages.Student
{
    public class UploadAssignmentModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public UploadAssignmentModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile AssignmentFile { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (AssignmentFile == null)
            {
                Message = "Please select a file to upload.";
                return Page();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolder, AssignmentFile.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await AssignmentFile.CopyToAsync(fileStream);
            }

            Message = $"File '{AssignmentFile.FileName}' uploaded successfully!";
            return Page();
        }
    }
}
