using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace lngCollector.Pages.Word
{
    [Authorize]
    public class DetailModel : PageModel
    {
        IEWordRepo repo;

        public DetailModel(IEWordRepo r)
        {
            repo = r;
        }

        public IActionResult OnGet(int id)
        {
//            Console.WriteLine($"DetailModel : GET METHOD ID={id}");

            if (id == 0)
            {
                return RedirectToPage("Words");
            }
                

            Word = repo.Get(id);

            if (Word == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public EWord Word { get; set; }
    }
}
