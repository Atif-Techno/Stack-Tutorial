using Microsoft.EntityFrameworkCore;
using StackTutorial.Models;
using StackTutorial.Repositories.Interfaces;
using StackTutorial.Data;
using static StackTutorial.Models.CommonClass;

namespace StackTutorial.Repositories.Implementations
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public ContentRepository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<ContentModel>> GetAllAsync()
        {
            return await _DbContext.Contents.Include(c => c.Topic).ThenInclude(t => t.Category).OrderBy(c => c.Order).ToListAsync();
        }

        public async Task<IEnumerable<ContentModel>> GetByTopicIdAsync(long topicId)
        {
            return await _DbContext.Contents.Where(c => c.TopicID == topicId).Include(c => c.Topic).OrderBy(c => c.Order).ToListAsync();
        }

        public async Task<ContentModel> GetByIdAsync(long id)
        {
            return await _DbContext.Contents.Include(c => c.Topic).ThenInclude(t => t.Category).FirstOrDefaultAsync(c => c.ContentID == id);
        }

        public async Task<ContentModel> AddAsync(ContentModel content)
        {
            _DbContext.Contents.Add(content);
            await _DbContext.SaveChangesAsync();
            return content;
        }

        public async Task<ContentModel> UpdateAsync(ContentModel content)
        {
            _DbContext.Entry(content).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return content;
        }

        public async Task DeleteAsync(long id)
        {
            var content = await _DbContext.Contents.FindAsync(id);
            if (content != null)
            {
                _DbContext.Contents.Remove(content);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<ContentModel> GetPreviousContentAsync(long currentContentId, long topicId)
        {
            var currentContent = await _DbContext.Contents.FindAsync(currentContentId);
            if (currentContent == null) return null;

            return await _DbContext.Contents.Where(c => c.TopicID == topicId && c.Order < currentContent.Order).OrderByDescending(c => c.Order).FirstOrDefaultAsync();
        }

        public async Task<ContentModel> GetNextContentAsync(long currentContentId, long topicId)
        {
            var currentContent = await _DbContext.Contents.FindAsync(currentContentId);
            if (currentContent == null) return null;

            return await _DbContext.Contents.Where(c => c.TopicID == topicId && c.Order > currentContent.Order).OrderBy(c => c.Order).FirstOrDefaultAsync();
        }
        public async Task<List<CommonModel>> CountContentCategoriesWise()
        {
            return await _DbContext.Contents.Include(t => t.Topic).ThenInclude(c => c.Category)
                .GroupBy(t => new { t.Topic.Category.CategoryID }).Select(g => new CommonModel
                {
                    CategoryID = g.Key.CategoryID,
                    ContentCount = g.Count()
                }).AsNoTracking() // Improves performance for read-only
                  .ToListAsync();
        }
    }
}