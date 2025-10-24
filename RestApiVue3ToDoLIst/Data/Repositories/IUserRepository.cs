namespace RestApiVue3ToDoLIst.Data.Interfaces
{
    public interface IUserRepository<T> where T: class
    {
        public Task<T> GetAsync(string? login, string? password);
        public Task<IEnumerable<T>> GetAllAsync(string? login, string? password);
        public Task<T> CheckExtistAsync(T? user);
        public Task<bool> AddAsync(T? user);
        public Task<bool> DropAsync(int? id, string? password);
        public Task<T> UpdateAsync(T? user);
    }
}
