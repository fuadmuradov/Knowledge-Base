using KnowledgeBase.Core;
using KnowledgeBase.Core.IRepositories;
using KnowledgeBase.Data.DbAccess;
using KnowledgeBase.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly KnowledgeDbContext context;
        IAuthorRepository authorRepository;
        ICountryRepository countryRepository;
        IDocumentTypeRepository documentTypeRepository;
        IOrganizationRepository organizationRepository;
        IPublicationRepository publicationRepository;
        IPublicationTagRepository publicationTagRepository;
        ITagRepository tagRepository;
        ITopicRepository topicRepository;

        public UnitOfWork(KnowledgeDbContext context)
        {
            this.context = context;
        }
        public IAuthorRepository AuthorRepository => authorRepository ?? new AuthorRepository(context);

        public ICountryRepository CountryRepository => countryRepository ?? new CountryRepository(context);

        public IDocumentTypeRepository DocumentTypeRepository => documentTypeRepository ?? new DocumentTypeRepository(context);

        public IOrganizationRepository OrganizationRepository => organizationRepository ?? new OrganizationRepository(context);

        public IPublicationRepository PublicationRepository => publicationRepository ?? new PublicationRepository(context); 

        public IPublicationTagRepository PublicationTagRepository => publicationTagRepository ?? new PublicationTagRepository(context); 

        public ITagRepository TagRepository => tagRepository ?? new TagRepository(context);

        public ITopicRepository TopicRepository => topicRepository ?? new TopicRepository(context);
    }
}
