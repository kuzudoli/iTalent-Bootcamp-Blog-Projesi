using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Dtos
{
    public class PostCreateWithImageDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
    }
}
