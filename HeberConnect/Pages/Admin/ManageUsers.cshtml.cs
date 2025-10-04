using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeberConnect.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();
        public Dictionary<string, IList<string>> UserRoles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            foreach (var u in Users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                UserRoles[u.Id] = roles;
            }
        }
    }
}
