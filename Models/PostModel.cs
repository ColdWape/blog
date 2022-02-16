using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public string MainPhoto { get; set; }

    }
}
