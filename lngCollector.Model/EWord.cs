using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    public class EWord
    {
        public int id { get; set; }
        public int? bunch_id { get; set; }// это поле не нужно. по логице 
        public int? user_id { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
        public int? lng_id { get; set; }
        // Number of sentences, made for this word.
        public int weight { get; set; }
        public DateTime? date { get; set; }
        public string? expressions { get; set; }
        public string? meaning { get; set; }
        public string? meaning_noun { get; set; }
        public string? meaning_verb { get; set; }
        public string? examples { get; set; }
    }
}
