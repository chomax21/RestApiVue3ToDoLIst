using System.ComponentModel.DataAnnotations;

namespace RestApiVue3ToDoLIst.Data.Models.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public string? StatusName { get; set; }
    }
}
