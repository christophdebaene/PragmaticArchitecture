using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Domain.Users;
using MyApp.Site.Infrastructure;

namespace MyApp.Site.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public IEnumerable<User> Data { get; private set; }
        public IndexModel(IUserRepository userRepository) => _userRepository = userRepository;
        public void OnGet() => Data = _userRepository.GetUsers();
        public IActionResult OnPostSelect(string name)
        {
            var user = _userRepository.GetUserByName(name);
            HttpContext.Session.Set("CurrentUser", user);
            return RedirectToPage("/Tasks/Dashboard");
        }
    }
}
