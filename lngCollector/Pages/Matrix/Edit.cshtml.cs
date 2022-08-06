using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace lngCollector.Pages.Matrix
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public EditModel(IMatrixRepo db)
        {
            _db = db;
        }

        public void OnGetEdit(int id)
        {
            operation = $"editing {id}...";
            Matrix = _db.Get(id);

            //LoggerObj.Write(Matrix);
        }

        public void OnGetCreate(int id)
        {
            operation = $"creating matrix";

            int uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Matrix = new Model.Matrix { date = DateTime.Now, user_id = uid };
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
    }
}
