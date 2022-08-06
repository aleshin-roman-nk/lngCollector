using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.WordsNode
{
    [Authorize]
    public class allModel : PageModel
    {
        private readonly IMatrixRepo _db;

        public allModel(IMatrixRepo db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Matrices = _db.GetAll(); 
        }

        [BindProperty]
        public IEnumerable<Model.Matrix> Matrices { get; set; }
    }
}
