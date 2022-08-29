using KnowledgeBase.Service.DTOs.AuthorDTOs;
using KnowledgeBase.Service.DTOs.CountryDTOs;
using KnowledgeBase.Service.DTOs.PublicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.IServices
{
    public interface IPublicationService:IService<PublicationGetDto, PublicationPostDto>
    {
      
    }
}
