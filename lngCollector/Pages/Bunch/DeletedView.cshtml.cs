using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.Bunch
{
    public class DeletedViewModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public DeletedViewModel(IMatrixRepo r)
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

            return RedirectToPage("/bunch/deletedview");
        }

        public IActionResult OnGetRestore(int id)
        {
            _db.RestoreFromBinRecycle(id);
            Matrixes = _db.AllDeleted();

            return RedirectToPage("/bunch/deletedview");
        }

        [BindProperty]
        public IEnumerable<Model.Bunch> Matrixes { get; set; }
    }
}