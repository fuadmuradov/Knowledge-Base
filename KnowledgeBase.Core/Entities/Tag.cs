using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core.Entities
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public List<PublicationToTag> PublicationToTags { get; set; }
    }
}
