using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeberConnect.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class AdminOnlyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
