using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core.Entities
{
    public class PublicationToTag
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
