using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class PostsWithPageCount
    {
        public List<PostWithCategoryDto> Posts { get; set; }
        public int pageCount { get; set; }
    }
}
