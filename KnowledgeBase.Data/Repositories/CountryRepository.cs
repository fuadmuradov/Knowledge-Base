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
    public class CountryRepository:Repository<Country>, ICountryRepository
    {
        public CountryRepository(KnowledgeDbContext context):base(context)
        {
        }
    }
}
