using KnowledgeBase.Service.DTOs.PublicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.CountryDTOs
{
    public class CountryGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Name { get; set; }
        public List<PublicationGetDto> Publications { get; set; }
    }
}
