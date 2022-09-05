using lngCollector.Model;
using lngCollector.Services;
using lngCollector.Services.UserDt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.Users
{
    public class LngSettingsModel : PageModel
    {
        private readonly ILngRepo lngRepo;
        private readonly IUserValuesRepo valuesRepo;

        public LngSettingsModel(ILngRepo lngRepo, IUserValuesRepo valuesRepo)
        {
            this.lngRepo = lngRepo;
            this.valuesRepo = valuesRepo;
        }

        public void OnGet()
        {
            Lngs = new List<Lng> { new Lng { id = 0, name = "any" } };
            (Lngs as List<Lng>).AddRange(lngRepo.All());
        }

        public IActionResult OnGetSetLng(int lngid)
        {
            valuesRepo.SaveValue("current_lng", lngid.ToString());

            return RedirectToPage("/lngActions");
        }

        [BindProperty]
        public IEnumerable<Lng> Lngs { get; set; }
    }
}
