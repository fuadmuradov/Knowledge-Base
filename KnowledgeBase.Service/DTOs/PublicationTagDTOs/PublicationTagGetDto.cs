
using KnowledgeBase.Service.DTOs.PublicationDTOs;
using KnowledgeBase.Service.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.PublicationTagDTOs
{
    public class PublicationTagGetDto
    {
        public int Id { get; set; }
        public PublicationGetDto Publication { get; set; }
        public TagGetDto Tag { get; set; }
    }
}
