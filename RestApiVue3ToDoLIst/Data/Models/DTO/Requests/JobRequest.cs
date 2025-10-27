using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Data.Models.DTO.Requests
{
    public class JobRequest
    {
        public int? Id { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? AssignedTo { get; set; }        
        public string? Title { get; set; }        
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
