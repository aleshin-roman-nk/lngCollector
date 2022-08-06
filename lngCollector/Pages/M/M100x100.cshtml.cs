using lngCollector.Model;
using lngCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lngCollector.Pages.M
{
    public class M100x100Model : PageModel
    {
        IEWordRepo _db;

        public M100x100Model(IEWordRepo db)
        {
            _db = db;
        }

        public void OnGet(/*int matrixid*/)// the matrix id goes here
        {
            var wcollection = _db.GetAll().ToArray();

            WordCount = wcollection.Length;

            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColsCount; col++)
                {
                    // индекс в wcollection
                    int indx = colsCount * row + col;

                    if(indx < wcollection.Length)
                    {
                        //Random rnd = new Random();
                        //mi[row, col] = rnd.Next(6) + 1;
                        mi[row, col] = wcollection[indx];
                    }
                    else mi[row, col] = null;
                }
            }
        }

        private const int rowsCount = 10;
        private const int colsCount = 40;

        public EWord[,] mi { get; set; } = new EWord[rowsCount, colsCount];

        public int RowsCount = rowsCount;
        public int ColsCount = colsCount;

        public int WordCount { get; set; }
    }
}
