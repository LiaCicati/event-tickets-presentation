using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface ICategoryService
    {
        Task<Category> createCategoryAsync(Category category);
        Task<List<Category>> getAllCategoriesAsync();
    }
}