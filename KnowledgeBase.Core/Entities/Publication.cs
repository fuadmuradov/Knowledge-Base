using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core.Entities
{
    public class Publication:BaseEntity
    {
        public int Year { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
        public List<PublicationToTag> PublicationToTags { get; set; }
    }
}
