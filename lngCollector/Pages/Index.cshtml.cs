using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            Nme = User.Identity.Name;

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/lngActions");
            }
            else return Page();
        }

        [BindProperty]
        public string Nme { get; set; }
    }
}