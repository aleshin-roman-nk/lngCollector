using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    public class Sentence
    {
        public int Id { get; set; }
        [Column("word_id")]
        public int WordId { get; set; }
        public string Text { get; set; }
        public DateTime? date { get; set; }
        public int? user_id { get; set; }
    }
}
