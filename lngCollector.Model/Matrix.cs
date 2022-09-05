using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    public class Matrix
    {
        public Matrix()
        {
            EWords = new List<EWord>();
        }

        public int id { get; set; }
        public int parentId { get; set; }
        public int user_id { get; set; }
        public int lng_id { get; set; }
        public string? name { get; set; }
        public string? content { get; set; }
        public string? content_short { get; set; }
        public ICollection<EWord> EWords { get; set; }// На самом деле матрица содержик коллекцию не слов, а узлов упражнений.
        // каждый узел 1-1 ссылается на слово. Вся информация о предложениях содержится в этом узле, а не слове.
        public DateTime? date { get; set; }
        public bool isdeleted { get; set; }

        [NotMapped]
        public int word_count { get; set; }
        [NotMapped]
        public int snt_count_target { get; set; }
        [NotMapped]
        public int snt_count_actually { get; set; }
    }
}
