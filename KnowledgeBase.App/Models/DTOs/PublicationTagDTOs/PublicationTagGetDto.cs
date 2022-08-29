

using KnowledgeBase.App.Models.DTOs.PublicationDTOs;
using KnowledgeBase.App.Models.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.PublicationTagDTOs
{
    public class PublicationTagGetDto
    {
        public int Id { get; set; }
        public PublicationGetDto Publication { get; set; }
        public TagGetDto Tag { get; set; }
    }
}
