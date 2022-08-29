using KnowledgeBase.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }
        ICountryRepository CountryRepository { get; }
        IDocumentTypeRepository DocumentTypeRepository { get; } 
        IOrganizationRepository OrganizationRepository { get; } 
        IPublicationRepository PublicationRepository { get; }
        IPublicationTagRepository PublicationTagRepository { get; }
        ITagRepository TagRepository { get; }
        ITopicRepository TopicRepository { get; }        
    }
}
