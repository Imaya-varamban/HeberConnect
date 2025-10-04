using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeberConnect.Pages.Dashboard
{
    [Authorize(Roles = "Staff")]
    public class StaffOnlyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
