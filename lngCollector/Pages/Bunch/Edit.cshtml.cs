using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Services.UserDt;
using lngCollector.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace lngCollector.Pages.Bunch
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMatrixRepo _db;
        private readonly ILngRepo lngrepo;
        private readonly IUserValuesRepo valuesRepo;

        public EditModel(IMatrixRepo db, ILngRepo lngrepo, IUserValuesRepo valuesRepo)
        {
            _db = db;
            this.lngrepo = lngrepo;
            this.valuesRepo = valuesRepo;
        }

        public void OnGetEdit(int id)
        {
            operation = $"editing {id}...";
            Matrix = _db.Get(id);

            LngOptions = lngrepo.All().Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }).ToList();
        }

        public void OnGetCreate(int id)
        {
            operation = $"creating matrix";
            LngOptions = lngrepo.All().Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }).ToList();

            int uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var currLng = valuesRepo.LoadInt("current_lng");

            Matrix = new Model.Matrix { date = DateTime.Now, user_id = uid, lng_id = currLng ?? 0 };
        }

        // Just save an object
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Matrix.name))
            {
                Error = "name must not be empty";
                return Page();
            }

            Matrix.content_short = Matrix.content.TruncateLongString(50);

            if (Matrix.id == 0)
                _db.Create(Matrix);
            else
                _db.Save(Matrix);

            return RedirectToPage("/matrix/detail", new { id = Matrix.id });
        }

        public IActionResult OnPostDelete()
        {
            _db.Delete(Matrix);

            return RedirectToPage("/matrix/all");
        }

        [BindProperty]
        public Model.Matrix? Matrix { get; set; }
        public string operation { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<SelectListItem> LngOptions { get; set; }
    }
}
