using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        void Commit();
    }
}
