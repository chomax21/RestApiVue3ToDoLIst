namespace RestApiVue3ToDoLIst.Data.Interfaces
{
    public interface IJobRepository<T> where T : class
    {
        public Task<T> GetAsync(int? id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> AddAsync(T job);
        public Task<bool> DropAsync(int? id);
        public Task<T> UpdateAsync(T job);
    }
}
