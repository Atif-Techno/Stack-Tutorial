using Microsoft.AspNetCore.Mvc;
using StackTutorial.Models;
using StackTutorial.Services.Interfaces;
using System.Threading.Tasks;

namespace StackTutorial.Controllers
{
    public class TutorialController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITopicService _topicService;
        private readonly IContentService _contentService;
        //private readonly ISearchService _searchService;

        public TutorialController(
            ICategoryService categoryService,
            ITopicService topicService,
            IContentService contentService
            //ISearchService searchService
            )
        {
            _categoryService = categoryService;
            _topicService = topicService;
            _contentService = contentService;
        }

        [HttpGet("category/{id:long}")]
        public async Task<IActionResult> Category(long id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            var topics = await _topicService.GetTopicsByCategoryIdAsync(id);

            ViewBag.Category = category;
            ViewBag.ActiveCategoryId = id;
            return View(topics);
        }

        [HttpGet("topic/{id:long}")]
        public async Task<IActionResult> Topic(long id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null) return NotFound();

            var contents = await _contentService.GetContentsByTopicIdAsync(id);

            ViewBag.Topic = topic;
            ViewBag.ActiveCategoryId = topic.CategoryID;
            ViewBag.ActiveTopicId = id;
            return View(contents);
        }

        [HttpGet("content/{id:long}")]
        public async Task<IActionResult> Content(long id)
        {
            var content = await _contentService.GetContentByIdAsync(id);
            if (content == null) return NotFound();

            var (previous, next) = await GetContentNeighbors(id, content.TopicID);

            ViewBag.PreviousContent = previous;
            ViewBag.NextContent = next;
            ViewBag.Topic = content.Topic;
            ViewBag.ActiveContentId = content.ContentID;
            ViewBag.ActiveTopicId = content.TopicID;
            ViewBag.ActiveCategoryId = content.Topic.CategoryID;
            return View(content);
        }

        private async Task<(ContentModel Previous, ContentModel Next)> GetContentNeighbors(long currentId, long topicId)
        {
            var previous = await _contentService.GetPreviousContentAsync(currentId, topicId);
            var next = await _contentService.GetNextContentAsync(currentId, topicId);
            return (previous, next);
        }

        //[HttpGet("search")]
        //[HttpPost("search")]
        //public async Task<IActionResult> Search(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var results = await _searchService.SearchAsync(query);

        //    ViewBag.SearchTerm = query;
        //    return View(results);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Search(string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var categories = await _categoryService.GetAllCategoriesAsync();
        //    var topics = await _topicService.GetAllTopicsAsync();
        //    var contents = await _contentService.GetAllContentsAsync();

        //    ViewBag.SearchTerm = searchTerm;
        //    return View((Categories: categories, Topics: topics, Contents: contents));
        //}


        //private async Task<(ContentModel Previous, ContentModel Next)> GetContentNeighbors(long currentId, long topicId)
        //{
        //    var previous = await _contentService.GetPreviousContentAsync(currentId, topicId);
        //    var next = await _contentService.GetNextContentAsync(currentId, topicId);
        //    return (previous, next);
        //}
    }
}