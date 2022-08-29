using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Core.Models;
using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(
            IGenericRepository<Category> repository, 
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
