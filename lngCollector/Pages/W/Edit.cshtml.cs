using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.W
{
    public class EditModel : PageModel
    {
        IEWordRepo repo;
        public EditModel(IEWordRepo r)
        {
            //Console.WriteLine("EditModel constructed");

            repo = r;
        }

        public IActionResult OnGet(int id)
        {
            //Console.WriteLine($"EditModel : GET METHOD ID={id}");

            if(id == 0)
            {
                //Console.WriteLine($"EditModel : GET METHOD => create a new object");
                Word = new EWord();

                return Page();
            }

            Word = repo.Get(id);

            if (Word == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost(EWord word)
        {
            //Console.WriteLine($"EditModel : POST METHOD ID={word.text}");

            if(string.IsNullOrEmpty(word.text))
            {
                Error = "Words text must not be empty";
                Word = word;
                return Page();
            }

            if(word.id == 0)
            {
                try
                {
                    Word = repo.Create(word);
                    Word.lng_type = LngType.eng;
                    return RedirectToPage("Detail", new { id = Word.id });
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    Word = word;
                    return Page();
                }
            }

            Word = word;

            if (Word == null) return RedirectToPage("/NotFound");

            try
            {
                //repo.Save(Word);
                repo.SaveTextDescriptionOnly(Word);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return Page();
            }

            return RedirectToPage("Detail", new { id = Word.id });

            //return Page();
        }

        public EWord Word { get; set; }
        public string Error { get; set; }
    }
}
