using lngCollector.Services;
using lngCollector.Services.UserDt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages
{
    public class lngActionsModel : PageModel
    {
        private readonly IUserValuesRepo valuesRepo;
        private readonly ILngRepo lngRepo;

        public lngActionsModel(IUserValuesRepo valuesRepo, ILngRepo lngRepo)
        {
            this.valuesRepo = valuesRepo;
            this.lngRepo = lngRepo;
        }

        public void OnGet()
        {
            var currlngID = valuesRepo.LoadValue("current_lng");

            if (currlngID == null) CurrentLngName = "***";
            else
            {
                var currlng = lngRepo.Get(int.Parse(currlngID));
                CurrentLngName = currlng?.name ?? "***";
            }
        }

        public string CurrentLngName { get; set; }
    }
}
