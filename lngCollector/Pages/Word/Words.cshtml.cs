using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.W
{
    [Authorize]
    public class WordsModel : PageModel
    {
        private readonly IEWordRepo _db;

        public WordsModel(IEWordRepo r)
        {
            _db = r;
        }

        public IEnumerable<EWord> Words { get; private set; }

        public void OnGet()
        {
            Words = _db.GetAll();
        }
    }
}
