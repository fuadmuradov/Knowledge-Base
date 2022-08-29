using KnowledgeBase.Service.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.IServices
{
    public interface ITagService:IService<TagGetDto, TagPostDto>
    {
    }
}
