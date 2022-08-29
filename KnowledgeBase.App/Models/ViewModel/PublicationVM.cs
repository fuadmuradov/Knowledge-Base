using KnowledgeBase.App.Models.DTOs.AuthorDTOs;
using KnowledgeBase.App.Models.DTOs.CountryDTOs;
using KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs;
using KnowledgeBase.App.Models.DTOs.OrganizationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationTagDTOs;
using KnowledgeBase.App.Models.DTOs.TagDTOs;
using KnowledgeBase.App.Models.DTOs.TopicDTOs;

namespace KnowledgeBase.App.Models.ViewModel
{
    public class PublicationVM
    {
        public List<PublicationGetDto> Publications { get; set; }
        public List<AuthorGetDto> Authors { get; set; }
        public List<CountryGetDto> Countries { get; set; }
        public List<OrganizationGetDto> Organizations { get; set; }
        public List<DocumentTypeGetDto> DocumentTypes { get; set; }
        public List<TopicGetDto> Topics { get; set; }
        public List<TagGetDto> Tags { get; set; }
        public List<PublicationTagGetDto> PublicationTags { get; set; }
        public PublicationPostVM PublicationPostVM { get; set; }
        

    }
}
