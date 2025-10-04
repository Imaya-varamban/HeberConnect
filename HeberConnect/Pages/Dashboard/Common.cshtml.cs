using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeberConnect.Pages.Dashboard
{
    [Authorize] // All logged-in users can access
    public class CommonModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
