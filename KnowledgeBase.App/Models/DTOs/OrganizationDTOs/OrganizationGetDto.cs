
using KnowledgeBase.App.Models.DTOs.PublicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.OrganizationDTOs
{
    public class OrganizationGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Name { get; set; }
        public List<PublicationGetDto> Publications { get; set; }
    }
}
