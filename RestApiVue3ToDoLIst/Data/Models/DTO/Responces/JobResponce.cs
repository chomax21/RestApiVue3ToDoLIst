using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Data.Models.DTO.Responces
{
    public class JobResponce : BaseResponce
    {        
        public Job? Job { get; set; }
        public List<Job>? JobList { get; set; }
    }
}
