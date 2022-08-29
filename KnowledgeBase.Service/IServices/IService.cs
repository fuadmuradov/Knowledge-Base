using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.IServices
{
    public interface IService<TEntityGet, TEntityPost>
    {
        Task<TEntityGet> CreateAsync(TEntityPost postDto);
        Task<TEntityGet> UpdateAsync(int id, TEntityPost postDto);
        Task DeleteAsync(int id);
        Task<TEntityGet> GetAsync(int id);
        Task<List<TEntityGet>> GetAllAsync();
    }
}
