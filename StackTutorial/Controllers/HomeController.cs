using Microsoft.AspNetCore.Mvc;
using StackTutorial.Models;
using StackTutorial.Services.Interfaces;
using System.Threading.Tasks;

namespace StackTutorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ITopicService _topicService;
        private readonly IContentService _contentService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ICategoryService categoryService,
            ITopicService topicService,
            IContentService contentService,
            ILogger<HomeController> logger)
        {
            _categoryService = categoryService;
            _topicService = topicService;
            _contentService = contentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var ContentCounts = await _contentService.CountContentCategoriesWise();
                ViewBag.ContentCounts = ContentCounts.ToDictionary(x => x.CategoryID, x => x.ContentCount);
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading categories for homepage");
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpGet("explore/{id:long}")]
        public async Task<IActionResult> Explore(long id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category {CategoryId} not found", id);
                    return NotFound();
                }

                var topics = await _topicService.GetTopicsByCategoryIdAsync(id);
                var firstTopic = topics.MinBy(t => t.Order);

                if (firstTopic == null)
                {
                    _logger.LogInformation("No topics found for category {CategoryId}", id);
                    return RedirectToAction("Category", "Tutorial", new { id });
                }

                var contents = await _contentService.GetContentsByTopicIdAsync(firstTopic.TopicID);
                var firstContent = contents.MinBy(c => c.Order);

                return firstContent != null
                    ? RedirectToAction("Content", "Tutorial", new { id = firstContent.ContentID })
                    : RedirectToAction("Topic", "Tutorial", new { id = firstTopic.TopicID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Explore for category {CategoryId}", id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier
            });
        }
    }
}