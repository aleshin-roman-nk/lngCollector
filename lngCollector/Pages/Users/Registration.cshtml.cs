using lngCollector.Model;
using lngCollector.Services.UserAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lngCollector.Pages.Users
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserAuthRepo _db;

        public RegistrationModel(IUserAuthRepo db)
        {
            _db = db;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("Error");
            //ModelState.Remove("Usr.Name");

            if (ModelState.IsValid)
            {
                if(_db.EmailExists(Usr.Email))
                {
                    //Error = $"{Usr.Email} already exists. Type another.";
                    ModelState.AddModelError("", $"{Usr.Email} already exists. Type another.");
                    return Page();
                }

                Random random = new Random();
                string code = random.Next(100000, 1000000).ToString();

                User u = new User { email = Usr.Email, name = Usr.Name, password = Usr.Password, email_verification_code = code, email_verified = false };

                _db.Create(u);

                return RedirectToPage("/Index");// go to user page
            }
            //else
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var item in ModelState)
            //    {
            //        sb.AppendLine($"([{item.Key}] : [{item.Value.AttemptedValue}]) ");
            //    }

            //    Error = sb.ToString();
            //}

            //if (string.IsNullOrEmpty(Error)) Error = "Smth goes wrong";

            return Page();
        }
        //[BindProperty]
        //public string Error { get; set; }
        [BindProperty]
        public UsrReg Usr { get; set; }
    }

    public class UsrReg
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string ConfirmPassword { get; set; }
    }
}
