﻿using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Services
{
    public interface ICommentService : IService<Comment>
    {
        Task<CustomResponseDto<List<CommentDto>>> GetCommentsByPostId(int postId);
        Task DeleteCommentById(int commentId);
    }
}
