using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Services.UserDt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.Bunch
{
    [Authorize]
    public class allModel : PageModel
    {
        private readonly IMatrixRepo _db;
        private readonly IUserValuesRepo valuesRepo;

        public allModel(IMatrixRepo db, IUserValuesRepo valuesRepo)
        {
            _db = db;
            this.valuesRepo = valuesRepo;
        }

        public void OnGet()
        {
            var lngid = valuesRepo.LoadInt("current_lng");

            Matrices = _db.GetAll(lngid ?? 0); 
        }

        [BindProperty]
        public IEnumerable<Model.Matrix> Matrices { get; set; }
    }
}
