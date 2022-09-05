using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lngCollector.Pages
{
    public class addlngModel : PageModel
    {
        private readonly ILngRepo lngRepo;

        public addlngModel(ILngRepo lngRepo)
        {
            this.lngRepo = lngRepo;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            lngRepo.Create(LngShortName, LngName);

            return RedirectToPage("/ssp32167");
        }

        [BindProperty]
        [Required(ErrorMessage = "Just name is required")]
        [DisplayName("full name")]
        public string LngName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Short name is required")]
        [DisplayName("short name")]
        public string LngShortName { get; set; }
    }
}
