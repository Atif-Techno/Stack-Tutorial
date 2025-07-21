using System.ComponentModel.DataAnnotations;

namespace StackTutorial.Models
{
    public class Content
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Foreign key
        public int TopicId { get; set; }

        // Navigation property
        public Topic Topic { get; set; }
    }
}