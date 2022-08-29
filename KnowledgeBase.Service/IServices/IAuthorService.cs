using KnowledgeBase.Service.DTOs.AuthorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.IServices
{
    public interface IAuthorService:IService<AuthorGetDto, AuthorPostDto>
    {    
        
    }
}
