using lngCollector.Model;
using lngCollector.Services.UserAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace lngCollector.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly IUserAuthRepo _repo;

        public LoginModel(IUserAuthRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            Usr = new Usr();
        }

        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("Error");

            if (ModelState.IsValid)
            {
                User user = _repo.Get(Usr.Email, Usr.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToPage("/Index");// or to user contenxt page
                }
                Error = "Некорректные логин и(или) пароль";
            }

            return Page();
        }

        private async Task Authenticate(User usr)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                //new Claim(ClaimsIdentity.DefaultNameClaimType, userName)

                new Claim(ClaimTypes.Name, usr.name),
                new Claim(ClaimTypes.NameIdentifier, usr.id.ToString()),
                new Claim(ClaimTypes.Email, usr.email)

                //new Claim("Name", usr.name),
                //new Claim("Id", usr.id.ToString())

            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, AppCookieSettings.Name);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(AppCookieSettings.Name, new ClaimsPrincipal(id));
        }

        [BindProperty]
        public string Error { get; set; }

        [BindProperty]
        public Usr Usr { get; set; }
    }

    public class Usr
    {
        [Required(ErrorMessage ="Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
