using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackTutorial.Models
{
    public class TopicModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TopicID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [StringLength(120, ErrorMessage = "Slug cannot exceed 120 characters")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug can only contain lowercase letters, numbers and hyphens")]
        public string? Slug { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [StringLength(160, ErrorMessage = "Meta description cannot exceed 160 characters")]
        public string? MetaDescription { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Order must be 0 or greater")]
        public int Order { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid category")]
        public long CategoryID { get; set; }

        public CategoryModel? Category { get; set; }

        public long? CreatedBy { get; set; }

        public ICollection<ContentModel> Contents { get; set; } = new List<ContentModel>();
    }
}