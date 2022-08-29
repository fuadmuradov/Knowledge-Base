using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core.Entities
{
    public class DocumentType:BaseEntity
    {
        public string TypeName { get; set; }
        public List<Publication> Publications { get; set; }
    }
}
