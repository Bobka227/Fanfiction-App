using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ChapterModel
    {
        public int Id { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
    }

}
