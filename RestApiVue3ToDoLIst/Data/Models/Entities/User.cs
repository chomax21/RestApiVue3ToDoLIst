using System.ComponentModel.DataAnnotations;

namespace RestApiVue3ToDoLIst.Data.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? SurName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Position { get; set; }

        [Required]
        [MinLength(8)]
        public string? Password { get; set; }


    }
}
