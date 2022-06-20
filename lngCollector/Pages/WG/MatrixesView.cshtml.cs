using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.WG
{
    public class MatrixesViewModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public MatrixesViewModel(IMatrixRepo db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Matrices = _db.GetAll();
        }

        [BindProperty]
        public IEnumerable<Matrix> Matrices { get; set; }
    }
}
