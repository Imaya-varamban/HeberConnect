using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeberConnect.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // If not logged in ? show guest landing page
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
                return Page();
            }

            // If logged in ? redirect to role-based dashboards
            if (User.IsInRole("Admin")) return RedirectToPage("/Admin/Index");
            if (User.IsInRole("Staff")) return RedirectToPage("/Staff/Index");
            if (User.IsInRole("Student")) return RedirectToPage("/Student/Index");

            // fallback ? guest page
            return Page();
        }
    }
}
