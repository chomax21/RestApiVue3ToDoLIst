namespace RestApiVue3ToDoLIst.Data.Models.DTO.Responces
{
    public class UserResponce : BaseResponce
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public Dictionary<string, string> UsersDict { get; set; }
    }
}
 