using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class CategoryWithPostsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PostDto> Posts { get; set; }
    }
}
