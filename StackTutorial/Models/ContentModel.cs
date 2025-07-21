using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackTutorial.Models
{
    public class ContentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContentID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [StringLength(120, ErrorMessage = "Slug cannot exceed 120 characters")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug can only contain lowercase letters, numbers and hyphens")]
        public string? Slug { get; set; }

        [Required(ErrorMessage = "Content body is required")]
        public string Body { get; set; }

        [StringLength(160, ErrorMessage = "Meta description cannot exceed 160 characters")]
        public string? MetaDescription { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Order must be 0 or greater")]
        public int Order { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("Topic")]
        [Required(ErrorMessage = "Please select a topic")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid topic")]
        public long TopicID { get; set; }

        public TopicModel? Topic { get; set; }

        public long? CreatedBy { get; set; }
    }
}