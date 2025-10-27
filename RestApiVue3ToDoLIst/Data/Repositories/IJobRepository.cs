using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Data.Interfaces
{
    public interface IJobRepository<T, K> where T : class
                                          where K : class
    {
        public Task<T> GetAsync(K jobRequest);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> AddAsync(K jobRequest);
        public Task<bool> DropAsync(K jobRequest);
        public Task<T> UpdateAsync(K job);
        public Task<Status> GetStatus(int? id);
    }
}
