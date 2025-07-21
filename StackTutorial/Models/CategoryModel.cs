using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackTutorial.Models
{
    public class CategoryModel
    {
        [Key] // Explicitly mark as primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public long CategoryID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(120)]
        public string? Slug { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(160)]
        public string? MetaDescription { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }

        public ICollection<TopicModel> Topics { get; set; } = new List<TopicModel>();
    }
}