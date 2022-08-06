using lngCollector.Model;
using lngCollector.Services.UserAuth;
using lngCollector.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace lngCollector.Pages.Users
{
    [Authorize]
    public class UserAccountModel : PageModel
    {
        private readonly IUserAuthRepo _repo;

        public UserAccountModel(IUserAuthRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                UserA = _repo.GetById(id);
            }
        }

        public void OnPost()
        {

        }

        public IActionResult OnPostChangeEmail()
        {
            

            // todo
            // сохранить новый адрес.
            // сгенерировать новый код подтверждения
            // поле что емаил подтвержден перевести в false

            //Console.WriteLine($"email : {UserA.email}");

            //ModelState.Remove("Message");


            if(string.IsNullOrEmpty(UserA.email))
            {
                ModelState.AddModelError("email", "Email should not be an empty string");
                return Page();
            }

            if(!Regex.IsMatch(UserA.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                ModelState.AddModelError("email", "The email does not fit to email format");
                return Page();
            }



            Random random = new Random();
            string code = random.Next(100000, 1000000).ToString();

            UserA.email_verified = false;
            UserA.email_verification_code = code;

            _repo.UpdateFields(UserA, new string[] { nameof(UserA.email), nameof(UserA.email_verification_code), nameof(UserA.email_verified) });

            //Console.WriteLine($"email has been changed for id={UserA.id}");

            //Message = "email has changed";

            //else
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var item in new string[] { nameof(UserA.email), nameof(UserA.email_verification_code), nameof(UserA.email_verified) })
            //    {
            //        Console.WriteLine(item);
            //    }

            //    foreach (var item in ModelState)
            //    {
            //        sb.AppendLine($"([{item.Key}] : [{item.Value.AttemptedValue}]) ");
            //    }

            //    Error = sb.ToString();

            //    return Page();
            //}

            return RedirectToPage("/users/useraccount");
        }

        public async Task<IActionResult> OnPostSendCode()
        {
            //int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            //UserA = _repo.GetById(id);

            Message = "The code has been successfully sent";

            Random random = new Random();

            UserA.email_verification_code = random.Next(100000, 1000000).ToString();

            _repo.UpdateFields(UserA, new string[] { nameof(UserA.email_verification_code) });

            EmailService eml = new EmailService();

            await eml.SendEmailAsync(UserA.email, "Email confirmation", $"You confirmation code is {UserA.email_verification_code}");

            return RedirectToPage("/users/useraccount");
        }

        public IActionResult OnPostEnterCode()
        {
            if (UserA.email_verification_code.Equals(confirmation_code))
            {
                UserA.email_verified = true;
                _repo.UpdateFields(UserA, new string[] { nameof(UserA.email_verified) });
            }

            return RedirectToPage("/users/useraccount");
        }

        [BindProperty]
        public User UserA { get; set; }
        [BindProperty]
        public string confirmation_code { get; set; }
        [BindProperty]
        public string Message { get; set; }
    }
}
