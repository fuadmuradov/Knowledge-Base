using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.App.Models.ViewModel
{
    public class PublicationTagVM
    {
        [Required]
        public int PublicationId { get; set; }

        [Required]
        public List<int> TagIds { get; set; }
    }
}
