using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.IO;
using lngCollector.Services;

namespace lngCollector.Pages.M
{
    public class cosmosModel : PageModel
    {
        private readonly ICosmosRepo _repo;
        private readonly IDbConfig dbc;
        int w = 400;
        int h = 400;

        public cosmosModel(ICosmosRepo repo, IDbConfig dbc)
        {
            _repo = repo;
            this.dbc = dbc;
        }

        public void OnGet()
        {
            //string fname = @"wwwroot\img\gme\cosmos.svg";

            //string path = dbc.path.Replace("\\", "/");
            //string fname = $"{path}/generated/cosmos.svg";

            //if (!Directory.Exists($"{dbc.path}\\generated"))
            //    Directory.CreateDirectory($"{dbc.path}\\generated");

            //cosmos_svg = fname;

            StringBuilder svgOut = new StringBuilder();

            svgOut.AppendLine($"<svg width=\"{w}\" height=\"{h}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\">");
            svgOut.AppendLine($"<rect x=\"0\" y=\"0\" width=\"{w}\" height=\"{h}\" stroke=\"black\" fill=\"#124B6B\" stroke-width=\"2\"/>");

            svgOut.AppendLine("<filter id=\"blurMe\"><feGaussianBlur in= \"SourceGraphic\" stdDeviation = \"1\" /></filter>");
            svgOut.AppendLine("<filter id=\"blurMe1\"><feGaussianBlur in= \"SourceGraphic\" stdDeviation = \"5\" /></filter>");

            // На заднем плане планета
            //svgOut.AppendLine("<circle cx = \"130\" cy = \"130\" r = \"40\" fill = \"#F4D03F\" filter=\"url(#blurMe1)\"/>");

            Random rnd = new Random();

            for (int i = 0; i < _repo.SentensesCount(); i++)
            {
                int x = rnd.Next(w - 4) + 4;
                int y = rnd.Next(h - 4) + 4;
                svgOut.AppendLine(drawPoint(x, y));
            }

            svgOut.AppendLine("</svg>");

            //System.IO.File.WriteAllText(fname, svgOut.ToString(), Encoding.UTF8);

            cosmos_svg = svgOut.ToString();
        }

        string drawPoint(int x, int y)
        {
            //return $"<rect x = \"{x}\" y = \"{y}\" width = \"5\" height = \"5\" stroke = \"yellow\" stroke-width = \"2\" fill = \"transparent\" filter=\"url(#blurMe)\"/>";
            return $"<circle cx = \"{x}\" cy = \"{y}\" r = \"2\" fill = \"#58D68D\" filter=\"url(#blurMe)\"/>";
        }

        public string cosmos_svg { get; set; }
    }
}
