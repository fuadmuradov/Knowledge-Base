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
    public class DocumentTypeRepository:Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(KnowledgeDbContext context):base(context)
        {

        }
    }
}
