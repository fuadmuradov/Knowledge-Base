
using KnowledgeBase.Service.DTOs.AuthorDTOs;
using KnowledgeBase.Service.DTOs.CountryDTOs;
using KnowledgeBase.Service.DTOs.DocumentTypeDTOs;
using KnowledgeBase.Service.DTOs.OrganizationDTOs;
using KnowledgeBase.Service.DTOs.TopicDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.PublicationDTOs
{
    public class PublicationGetDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public AuthorGetDto Author { get; set; }
        public OrganizationGetDto Organization { get; set; }
        public CountryGetDto Country { get; set; }
        public DocumentTypeGetDto DocumentType { get; set; }
        public TopicGetDto Topic { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public DateTime ModifiedAt { get; set; }

    }
}
