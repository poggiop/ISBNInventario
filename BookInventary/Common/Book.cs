using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? PageCount { get; set; }
        public string PreviewLink { get; set; }
        public int? PrintedPageCount { get; set; }
        public string PrintType { get; set; }
        public string PublishedDate { get; set; }
        public string Subtitle { get; set; }
        public string Publisher { get; set; }
    }
}
