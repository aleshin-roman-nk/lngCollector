using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.Matrix
{
    [Authorize]
    public class DetailModel : PageModel
    {

        private readonly IMatrixRepo _db;

        public DetailModel(IMatrixRepo db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            if(id == 0)
                return RedirectToPage("/matrix/all");

            Matrix = _db.Get(id);

            return Page();
        }

        public IActionResult OnPostDelete()
        {

            //_db.Delete(Matrix);

            _db.SendToBinRecycle(Matrix);

            return RedirectToPage("/matrix/all");
        }

        [BindProperty]
        public Model.Matrix? Matrix { get; set; }
    }
}
