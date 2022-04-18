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

            NewSentence = "";

            return Page();
        }

        public IActionResult OnGetRemoveSentence(int id, int sentid)
        {
            Sentences = _db.DelSentence(new Sentence { Id = sentid, WordId = id });
            Word = _db.Get(id);

            return Page();
        }

        public IActionResult OnPostCommitSentence()
        {
            if (!string.IsNullOrEmpty(NewSentence))
                Sentences = _db.AddSentence(NewSentence, Word.id);
            else
            {
                Error = "A sentence must not be an empty string";
                Sentences = _db.GetSentences(Word.id);
            }

            Word = _db.Get(Word.id);

            NewSentence = "";

            return Page();
            //return RedirectToPage("/games/makeSentences");
        }

        [BindProperty(SupportsGet = true)]
        public EWord Word { get; set; }

        public IEnumerable<Sentence> Sentences { get; set; }

        [BindProperty]
        public string NewSentence { get; set; }
        public string Error { get; set; }
    }
}
