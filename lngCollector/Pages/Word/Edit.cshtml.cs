using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Services.UserDt;
using lngCollector.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace lngCollector.Pages.Word
{
    [Authorize]
    public class EditModel : PageModel
    {
        IEWordRepo repo;
        private readonly ILngRepo lngrepo;
        private readonly IUserValuesRepo valuesRepo;

        public EditModel(IEWordRepo r, ILngRepo lngrepo, IUserValuesRepo valuesRepo)
        {
            repo = r;
            this.lngrepo = lngrepo;
            this.valuesRepo = valuesRepo;
        }

        public void OnGetCreate(int matrixid)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Options = lngrepo.All().Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }).ToList();

            var current_lng = int.Parse(valuesRepo.LoadValue("current_lng") ?? "0");

            Word = new EWord { bunch_id = matrixid, weight = 0, date = DateTime.Now, user_id = id, lng_id = current_lng };
        }

        public IActionResult OnGetEdit(int id)
        {
            Word = repo.Get(id);

            Options = lngrepo.All().Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }).ToList();

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

        [BindProperty]
        public List<SelectListItem> Options { get; set; }
    }
}
