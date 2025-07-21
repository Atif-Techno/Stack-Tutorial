using StackTutorial.Models;
using static StackTutorial.Models.CommonClass;

namespace StackTutorial.Services.Interfaces
{
    public interface IContentService
    {
        Task<IEnumerable<ContentModel>> GetAllContentsAsync();
        Task<IEnumerable<ContentModel>> GetContentsByTopicIdAsync(long topicId);
        Task<ContentModel> GetContentByIdAsync(long id);
        Task<ContentModel> CreateContentAsync(ContentModel content);
        Task<ContentModel> UpdateContentAsync(ContentModel content);
        Task DeleteContentAsync(long id);
        Task<ContentModel> GetPreviousContentAsync(long currentContentId, long topicId);
        Task<ContentModel> GetNextContentAsync(long currentContentId, long topicId);
        Task<List<CommonModel>> CountContentCategoriesWise();
    }
}