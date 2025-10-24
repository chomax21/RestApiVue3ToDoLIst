using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Data.Models.DTO.Requests
{
    public class JobRequest
    {
        public string? JobId { get; set; }
        public string? CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public Job? Job { get;}
    }
}
