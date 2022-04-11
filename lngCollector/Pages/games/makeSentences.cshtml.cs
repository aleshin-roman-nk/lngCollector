using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.games
{
    public class makeSentencesModel : PageModel
    {
        private readonly IEWordRepo _db;

        public makeSentencesModel(IEWordRepo db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Word = _db.Get(id);

            Sentences = _db.GetSentences(Word.id);

            return Page();
        }

        public IActionResult OnGetRemoveSentence(int id, int id2)
        {
            Sentences = _db.DelSentence(new Sentence { Id = id2, WordId = id });
            Word = _db.Get(id);

            return Page();
        }

        public IActionResult OnPostCommitSentence()
        {
            Sentences = _db.AddSentence(NewSentence, Word.id);
            Word = _db.Get(Word.id);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public EWord Word { get; set; }

        public IEnumerable<Sentence> Sentences { get; set; }

        [BindProperty]
        public string NewSentence { get; set; }
    }
}
