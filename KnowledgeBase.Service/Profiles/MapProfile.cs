using AutoMapper;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.AuthorDTOs;
using KnowledgeBase.Service.DTOs.CountryDTOs;
using KnowledgeBase.Service.DTOs.DocumentTypeDTOs;
using KnowledgeBase.Service.DTOs.OrganizationDTOs;
using KnowledgeBase.Service.DTOs.PublicationDTOs;
using KnowledgeBase.Service.DTOs.PublicationTagDTOs;
using KnowledgeBase.Service.DTOs.TagDTOs;
using KnowledgeBase.Service.DTOs.TopicDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<AuthorPostDto, Author>();
            CreateMap<Author, AuthorGetDto>();

            CreateMap<CountryPostDto, Country>();
            CreateMap<Country, CountryGetDto>();

            CreateMap<DocumentTypePostDto, DocumentType>();
            CreateMap<DocumentType, DocumentTypeGetDto>();

            CreateMap<OrganizationPostDto, Organization>();
            CreateMap<Organization, OrganizationGetDto>();

            CreateMap<TagPostDto, Tag>();
            CreateMap<Tag, TagGetDto>();

            CreateMap<TopicPostDto, Topic>();
            CreateMap<Topic, TopicGetDto>();

            CreateMap<PublicationTagPostDto, PublicationToTag>();
            CreateMap<PublicationToTag, PublicationTagGetDto>();

            CreateMap<PublicationPostDto, Publication>();//.ForMember(dest=> dest.FileName, src=>src.MapFrom(x=>x.file.FileName))
            CreateMap<Publication, PublicationGetDto>();

        }

    }
}
