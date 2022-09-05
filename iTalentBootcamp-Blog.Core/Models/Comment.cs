using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
