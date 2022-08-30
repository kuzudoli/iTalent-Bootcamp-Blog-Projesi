using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class PostPopularDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public string CreatedAtShortString => CreatedAt.ToShortDateString();
        public DateTime CreatedAt { get; set; }
    }
}
