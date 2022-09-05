using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    public class Story
    {
        public int id { get; set; }
        public int bunchId { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public string text { get; set; }
    }
}
