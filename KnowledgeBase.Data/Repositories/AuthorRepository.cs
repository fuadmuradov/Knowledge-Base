using KnowledgeBase.Core.Entities;
using KnowledgeBase.Core.IRepositories;
using KnowledgeBase.Data.DbAccess;
using KnowledgeBase.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class AuthorRepository:Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(KnowledgeDbContext context):base(context)
        {
      
        }
    }
}
