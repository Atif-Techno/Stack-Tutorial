using Microsoft.AspNetCore.Mvc;
using StackTutorial.Models;
using StackTutorial.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using UmbrellaCorp.Infrastructure.Repositories;

namespace StackTutorial.Controllers.Admin
{
    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<TopicController> _logger;

        public TopicController(
            ITopicService topicService,
            ICategoryService categoryService,
            ILogger<TopicController> logger)
        {
            _topicService = topicService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var topics = await _topicService.GetAllTopicsAsync();
                return View(topics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topics");
                TempData["ErrorMessage"] = "An error occurred while loading topics.";
                return View(Array.Empty<TopicModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                await PopulateCategoryDropdown();
                return View(new TopicModel { IsActive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading categories for topic creation");
                TempData["ErrorMessage"] = "An error occurred while loading categories.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TopicModel topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    topic.TopicID = UmbrellaLib.NEWGUID();
                    await _topicService.CreateTopicAsync(topic);
                    TempData["SuccessMessage"] = "Topic created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                await PopulateCategoryDropdown(topic.CategoryID);
                return View(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating topic");
                TempData["ErrorMessage"] = "An error occurred while creating the topic.";
                await PopulateCategoryDropdown(topic.CategoryID);
                return View(topic);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    _logger.LogWarning("Topic with ID {TopicId} not found", id);
                    return NotFound();
                }

                await PopulateCategoryDropdown(topic.CategoryID);
                return View(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topic for edit");
                TempData["ErrorMessage"] = "An error occurred while loading the topic.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, TopicModel topic)
        {
            try
            {
                if (id != topic.TopicID)
                {
                    _logger.LogWarning("ID mismatch in topic edit");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    // Log all validation errors
                    foreach (var error in ModelState)
                    {
                        if (error.Value.Errors.Count > 0)
                        {
                            _logger.LogWarning("Validation error for {Key}: {Errors}",
                                error.Key,
                                string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage)));
                        }
                    }

                    await PopulateCategoryDropdown(topic.CategoryID);
                    return View(topic);
                }

                if (ModelState.IsValid)
                {
                    await _topicService.UpdateTopicAsync(topic);
                    TempData["SuccessMessage"] = "Topic updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                await PopulateCategoryDropdown(topic.CategoryID);
                return View(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating topic");
                TempData["ErrorMessage"] = "An error occurred while updating the topic.";
                await PopulateCategoryDropdown(topic.CategoryID);
                return View(topic);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    _logger.LogWarning("Topic with ID {TopicId} not found for deletion", id);
                    return NotFound();
                }

                if (topic.Contents.Any())
                {
                    TempData["ErrorMessage"] = "Cannot delete topic because it contains contents. Delete the contents first.";
                    return RedirectToAction(nameof(Index));
                }

                return View(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topic for deletion");
                TempData["ErrorMessage"] = "An error occurred while loading the topic.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    _logger.LogWarning("Topic with ID {TopicId} not found for deletion", id);
                    return NotFound();
                }

                if (topic.Contents.Any())
                {
                    TempData["ErrorMessage"] = "Cannot delete topic because it contains contents. Delete the contents first.";
                    return RedirectToAction(nameof(Index));
                }

                await _topicService.DeleteTopicAsync(id);
                TempData["SuccessMessage"] = "Topic deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic");
                TempData["ErrorMessage"] = "An error occurred while deleting the topic.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        private async Task PopulateCategoryDropdown(long? selectedCategoriesId = null)
        {
            try
            {
                var category = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Categories = new SelectList(category, "CategoryID", "Name", selectedCategoriesId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating Category dropdown");
                ViewBag.Categories = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}