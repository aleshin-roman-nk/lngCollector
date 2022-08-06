using lngCollector.Model;
using lngCollector.Services.UserAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace lngCollector.Pages.Users
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserAuthRepo _db;

        public ChangePasswordModel(IUserAuthRepo db)
        {
            _db = db;
        }

        public void OnGet()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            //    UserA = _db.GetById(id);
            //}
        }

        public IActionResult OnPost()
        {
            // Можно прям на форме добавить поле ввода существующуго пароля.
            // Но пока не все так серьезно )))

            //if (User.Identity.IsAuthenticated)
            //{
            //    int id = ;

            //    User UserA = _db.GetById(id);
            //}

            if(!ModelState.IsValid) return Page();

            var u = _db.GetById(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            u.password = password;

            _db.UpdateField(u, () => u.password);

            return RedirectToPage("/users/useraccount");
        }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [BindProperty]
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string confirm_password { get; set; }
    }

}
