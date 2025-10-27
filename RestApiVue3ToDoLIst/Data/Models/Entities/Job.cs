using System.ComponentModel.DataAnnotations;

namespace RestApiVue3ToDoLIst.Data.Models.Entities
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public Status? Status { get; set; }

        [Required]
        public User? CreatedBy { get; set; }

        [Required]
        public User? AssignedTo { get; set; }
        
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
