using KnowledgeBase.Core.Entities;
using KnowledgeBase.Core.IRepositories;
using KnowledgeBase.Data.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class TopicRepository:Repository<Topic>, ITopicRepository
    {
        public TopicRepository(KnowledgeDbContext context):base(context)
        {
        }
    }
}
