using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Repository.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteCommentById(int commentId)
        {
            var commentForDelete = await _context.Comments.FindAsync(commentId);
            _context.Comments.Remove(commentForDelete);
        }

        public async Task<List<Comment>> GetCommentsByPostId(int postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
