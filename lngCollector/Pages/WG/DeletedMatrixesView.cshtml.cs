using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.WG
{
    public class DeletedMatrixesViewModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public DeletedMatrixesViewModel(IMatrixRepo r)
        {
            _db = r;
        }

        public void OnGet()
        {
            Matrixes = _db.AllDeleted();
        }

        public IActionResult OnGetKill(int id)
        {
            _db.Delete(id);
            Matrixes = _db.AllDeleted();

            return RedirectToPage("/wg/deletedmatrixesview");
        }

        public IActionResult OnGetRestore(int id)
        {
            _db.RestoreFromBinRecycle(id);
            Matrixes = _db.AllDeleted();

            return RedirectToPage("/wg/deletedmatrixesview");
        }

        [BindProperty]
        public IEnumerable<Matrix> Matrixes { get; set; }
    }
}