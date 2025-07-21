using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackTutorial.Models;
using StackTutorial.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UmbrellaCorp.Infrastructure.Repositories;

namespace StackTutorial.Controllers.Admin
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly ITopicService _topicService;
        private readonly ILogger<ContentController> _logger;

        public ContentController(
            IContentService contentService,
            ITopicService topicService,
            ILogger<ContentController> logger)
        {
            _contentService = contentService;
            _topicService = topicService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var contents = await _contentService.GetAllContentsAsync();
                return View(contents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contents");
                TempData["ErrorMessage"] = "An error occurred while loading contents.";
                return View(Array.Empty<ContentModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                await PopulateTopicDropdown();
                return View(new ContentModel
                {
                    Order = 0,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading topics for content creation");
                TempData["ErrorMessage"] = "An error occurred while preparing the form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentModel content)
        {
            try
            {
                content.Body = Request.Form["Body"]; // Capture TinyMCE content
                content.CreatedDate = DateTime.UtcNow;

                if (ModelState.IsValid)
                {
                    content.ContentID = UmbrellaLib.NEWGUID();
                    await _contentService.CreateContentAsync(content);
                    TempData["SuccessMessage"] = "Content created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                await PopulateTopicDropdown(content.TopicID);
                return View(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating content");
                TempData["ErrorMessage"] = "An error occurred while creating the content.";
                await PopulateTopicDropdown(content?.TopicID ?? 0);
                return View(content);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var content = await _contentService.GetContentByIdAsync(id);
                if (content == null)
                {
                    _logger.LogWarning("Content with ID {ContentId} not found", id);
                    return NotFound();
                }

                await PopulateTopicDropdown(content.TopicID);
                return View(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving content for edit");
                TempData["ErrorMessage"] = "An error occurred while loading the content.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ContentModel content)
        {
            try
            {
                if (id != content.ContentID)
                {
                    _logger.LogWarning("ID mismatch in content edit");
                    return NotFound();
                }

                // Ensure TinyMCE content is properly bound
                content.Body = Request.Form["Body"];
                content.UpdatedDate = DateTime.UtcNow;

                if (ModelState.IsValid)
                {
                    await _contentService.UpdateContentAsync(content);
                    TempData["SuccessMessage"] = "Content updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogWarning("Model validation failed for content ID {ContentId}", id);
                await PopulateTopicDropdown(content.TopicID);
                return View(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating content ID {ContentId}", id);
                TempData["ErrorMessage"] = "An error occurred while updating the content.";
                await PopulateTopicDropdown(content.TopicID);
                return View(content);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var content = await _contentService.GetContentByIdAsync(id);
                if (content == null)
                {
                    _logger.LogWarning("Content with ID {ContentId} not found for deletion", id);
                    return NotFound();
                }

                return View(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving content for deletion");
                TempData["ErrorMessage"] = "An error occurred while loading the content.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _contentService.DeleteContentAsync(id);
                TempData["SuccessMessage"] = "Content deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting content");
                TempData["ErrorMessage"] = "An error occurred while deleting the content.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        private async Task PopulateTopicDropdown(long? selectedTopicId = null)
        {
            try
            {
                var topics = await _topicService.GetAllTopicsAsync();
                ViewBag.Topics = new SelectList(topics, "TopicID", "Title", selectedTopicId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating topic dropdown");
                ViewBag.Topics = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}