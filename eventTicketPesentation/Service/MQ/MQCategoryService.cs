using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service.MQ
{
    public class MQCategoryService  : MQService, ICategoryService
    {
        public MQCategoryService(IModel channel) : base(channel)
        {
        }

        public Task<Category> CreateCategoryAsync(Category category)
        {
            return SendAndConvertAsync<Category, Category>("createCategory", category);
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return SendAndConvertAsync<List<Category>>("getAllCategories");
        }
    }
}