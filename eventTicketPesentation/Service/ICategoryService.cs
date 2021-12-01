using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}