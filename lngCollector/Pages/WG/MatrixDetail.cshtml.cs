using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.WG
{
    public class MatrixDetailModel : PageModel
    {

        private readonly IMatrixRepo _db;

        public MatrixDetailModel(IMatrixRepo db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            if(id == 0)
                return RedirectToPage("/wg/matrixesview");

            Matrix = _db.Get(id);

            return Page();
        }

        public IActionResult OnPostDelete()
        {

            //_db.Delete(Matrix);

            _db.SendToBinRecycle(Matrix);

            return RedirectToPage("/wg/matrixesview");
        }

        [BindProperty]
        public Matrix? Matrix { get; set; }
    }
}
