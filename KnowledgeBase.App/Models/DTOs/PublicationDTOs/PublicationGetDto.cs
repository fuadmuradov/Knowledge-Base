

using KnowledgeBase.App.Models.DTOs.AuthorDTOs;
using KnowledgeBase.App.Models.DTOs.CountryDTOs;
using KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs;
using KnowledgeBase.App.Models.DTOs.OrganizationDTOs;
using KnowledgeBase.App.Models.DTOs.TopicDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.PublicationDTOs
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
