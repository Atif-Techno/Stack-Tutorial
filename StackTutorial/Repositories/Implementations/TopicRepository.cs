using Microsoft.EntityFrameworkCore;
using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Data;

namespace StackTutorial.Repositories.Implementations
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public TopicRepository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<TopicModel>> GetAllAsync()
        {
            return await _DbContext.Topics.Include(t => t.Category).ToListAsync();
        }

        public async Task<IEnumerable<TopicModel>> GetByCategoryIdAsync(long categoryId)
        {
            return await _DbContext.Topics.Where(t => t.CategoryID == categoryId).Include(t => t.Category).ToListAsync();
        }

        public async Task<TopicModel> GetByIdAsync(long id)
        {
            return await _DbContext.Topics.Include(t => t.Category).FirstOrDefaultAsync(t => t.TopicID == id);
        }

        public async Task<TopicModel> AddAsync(TopicModel topic)
        {
            _DbContext.Topics.Add(topic);
            await _DbContext.SaveChangesAsync();
            return topic;
        }

        public async Task<TopicModel> UpdateAsync(TopicModel topic)
        {
            _DbContext.Entry(topic).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return topic;
        }

        public async Task DeleteAsync(long id)
        {
            var topic = await _DbContext.Topics.FindAsync(id);
            if (topic != null)
            {
                _DbContext.Topics.Remove(topic);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}