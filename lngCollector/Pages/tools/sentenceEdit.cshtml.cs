using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.tools
{
    [Authorize]
    public class sentenceEditModel : PageModel
    {
        private readonly IEWordRepo db;

        public sentenceEditModel(IEWordRepo r)
        {
            this.db = r;
        }

        public void OnGet(int id)
        {
            Sentence = db.GetSentence(id);
        }

        public IActionResult OnPostCommit()
        {
            if (!string.IsNullOrEmpty(Sentence.Text))
            {
                db.SaveSentence(Sentence);
                return RedirectToPage("/games/makeSentences", new { id = Sentence.WordId });
            }
            else
            {
                Error = "A sentence must not be an empty string";
                Sentence = db.GetSentence(Sentence.Id);
                return Page();
            }
        }

        public IActionResult OnPostDelete()
        {
            db.DelSentence(Sentence);

            return RedirectToPage("/games/makeSentences", new { id = Sentence.WordId });
        }

        [BindProperty]
        public Sentence? Sentence { get; set; }

        public string Error { get; set; }
    }
}
