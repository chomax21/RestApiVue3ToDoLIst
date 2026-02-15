using Microsoft.EntityFrameworkCore;
using RestApiVue3ToDoLIst.Controllers;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Services
{
    public class UserService : IUserRepository<User>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<JobController> _logger;

        public UserService(ApplicationContext context, ILogger<JobController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> AddAsync(User? user)
        {
            if (user == null)            
                return false;                                             
            
            await _context.Users.AddAsync(user!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DropAsync(int? id, string? password)
        {                        
           var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Password == password);
           if (user == null)
              return false;

            _context.Users.Remove(user!);
            await _context.SaveChangesAsync();
            return true;            
        }

        public async Task<User> GetAsync(string? login = "", string? password = "")
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            return user ?? null!;            
        }

        public async Task<User> CheckExtistAsync(User? user)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);               
            return existUser ?? null!;
        }

        public async Task<User> UpdateAsync(User? user)
        {
            _context.Users.Attach(user!);
            _context.Entry(user!).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user!;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? login)
        {                      
            var checkUser = await CheckExtistAsync(new User { Login = login });                
            if (checkUser == null)
                return null!;
            
            var users = await _context.Users.ToListAsync();
            return users ?? new List<User>(0);                       
        }
    }
}
