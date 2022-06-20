using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.WG
{
    public class MatrixEditModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public MatrixEditModel(IMatrixRepo db)
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

            Matrix = new Matrix { date = DateTime.Now };
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

            return RedirectToPage("/wg/matrixdetail", new { id = Matrix.id });
        }

        public IActionResult OnPostDelete()
        {
            _db.Delete(Matrix);

            return RedirectToPage("/wg/matrixesview");
        }

        [BindProperty]
        public Matrix? Matrix { get; set; }
        public string operation { get; set; }
        public string Error { get; set; }
    }
}
