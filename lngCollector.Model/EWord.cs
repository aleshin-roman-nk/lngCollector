using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    public class EWord
    {
        public int id { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
        //public int lng_id { get; set; }
        public LngType lng_type { get; set; }
        // степень освоения слова. можно использовать для формирования автозаданий. или фильтрации тех слов, по которым меньше практики.
        public int weight { get; set; }
        // keeps produced sentences
        public string? performance { get; set; }
    }
}
