using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiVue3ToDoLIst.Controllers;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Services
{
    public class JobService : IJobRepository<Job>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<JobController> _logger;
        private readonly IUserRepository<User> _userService;

        public JobService(ApplicationContext context, ILogger<JobController> logger, IUserRepository<User> userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
        }

        public async Task<bool> AddAsync(Job job)
        {
            try
            {
                var createdBy = await _userService.CheckExtistAsync(job.CreatedBy);
                var assignedTo = await _userService.CheckExtistAsync(job.AssignedTo);

                var newJob = new Job() { 
                    Id = job.Id,
                    Status = job.Status,
                    Description = job.Description,
                    Title = job.Title,
                    AssignedTo = assignedTo,
                    CreatedBy = createdBy,
                    CreatedAt = job.CreatedAt,
                    UpdatedAt = job.UpdatedAt
                };

                await _context.Jobs.AddAsync(newJob);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            

        }

        public async Task<bool> DropAsync(int? id)
        {
            try
            {
                var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id!.Value);
                if (job != null)
                {
                    _context.Jobs.Remove(job);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else                
                    return false;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }                                   
        }

        public async Task<Job> GetAsync(int? id)
        {
            try
            {
                var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id!.Value);
                if (job != null)
                    return job!;
                else
                    return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }            
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            try
            {
                var jobs = await _context.Jobs.ToListAsync();
                if (jobs != null)
                    return jobs;
                return null!;
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            try
            {
                _context.Jobs.Attach(job);
                _context.Entry(job).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return job;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
            

            
        }
    }
}
