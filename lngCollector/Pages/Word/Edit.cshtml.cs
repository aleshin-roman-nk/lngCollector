using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace lngCollector.Pages.W
{
    [Authorize]
    public class EditModel : PageModel
    {
        IEWordRepo repo;
        public EditModel(IEWordRepo r)
        {
            repo = r;
        }

        public void OnGetCreate(int matrixid)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(id);
            Word = new EWord { MatrixId = matrixid, weight = 0, lng_id = 0, date = DateTime.Now, user_id = id };
        }

        public IActionResult OnGetEdit(int id)
        {
            Word = repo.Get(id);

            if (Word == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if(string.IsNullOrEmpty(Word.text))
            {
                Error = "Words text must not be empty";
                return Page();
            }

            if(Word.id == 0)
            {
                try
                {
                    Word = repo.Create(Word);
                    //Word.lng_type = LngType.eng;
                    return RedirectToPage("Detail", new { id = Word.id });
                }
                catch (Exception ex)
                {
                    Error = ex.GetAllInners();
                    return Page();
                }
            }

            try
            {
                //repo.Save(Word);
                repo.SaveTextpartOnly(Word);
            }
            catch (Exception ex)
            {
                Error = ex.GetAllInners();
                return Page();
            }

            return RedirectToPage("Detail", new { id = Word.id });

            //return Page();
        }

        [BindProperty]
        public EWord Word { get; set; }
        public string Error { get; set; }
    }
}
