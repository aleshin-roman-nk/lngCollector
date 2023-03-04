using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.W
{
    [Authorize]
    public class ConfirmDeleteModel : PageModel
    {
        IEWordRepo repo;

        public ConfirmDeleteModel(IEWordRepo r)
        {
            repo = r;
        }

        public void OnGet(int id)
        {
            Word = repo.Get(id);
        }

        public IActionResult OnPost(EWord word)
        {
            //Console.WriteLine($"DELETING '{word.text}'");

            repo.Delete(word);

            return RedirectToPage("/matrix/detail", new { id = word.bunch_id });
        }

        public EWord Word { get; set; }
    }
}
