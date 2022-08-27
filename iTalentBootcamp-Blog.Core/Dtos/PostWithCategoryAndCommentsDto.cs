using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class PostWithCategoryAndCommentsDto : PostDto
    {
        public CategoryDto Category { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
