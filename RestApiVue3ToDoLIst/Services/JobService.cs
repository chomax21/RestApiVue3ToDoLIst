using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiVue3ToDoLIst.Controllers;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Requests;
using RestApiVue3ToDoLIst.Data.Models.Entities;
using System.Net;

namespace RestApiVue3ToDoLIst.Services
{
    public class JobService : IJobRepository<Job, JobRequest>
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

        public async Task<bool> AddAsync(JobRequest jobRequest)
        {
            try
            {                               
                var createdBy = await _userService.CheckExtistAsync(new User() { Login = jobRequest.CreatedBy });
                var assignedTo = await _userService.CheckExtistAsync(new User() { Login = jobRequest.AssignedTo });
                var status = await GetStatus(jobRequest.Status);

                var newJob = new Job() {                     
                    Status = status,
                    Description = jobRequest.Description,
                    Title = jobRequest.Title,
                    AssignedTo = assignedTo,
                    CreatedBy = createdBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
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

        public async Task<bool> DropAsync(JobRequest jobRequest)
        {
            try
            {
                var job = _context.Jobs.FirstOrDefault(x => x.Id == jobRequest.Id);
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

        public async Task<Job> GetAsync(JobRequest jobRequest)
        {
            try
            {
                var job = await _context.Jobs
                                        .Include(x => x.AssignedTo)
                                        .Include(x => x.CreatedBy)
                                        .Include(x => x.Status)
                                        .FirstOrDefaultAsync(x => x.Id == jobRequest.Id);
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
                var jobs = await _context.Jobs
                                            .Include(x=>x.AssignedTo)
                                            .Include(x => x.CreatedBy)
                                            .Include(x => x.Status)
                                            .ToListAsync();
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

        public async Task<Job> UpdateAsync(JobRequest jobRequest)
        {
            try
            {
                var job = _context.Jobs
                                    .Include(x => x.AssignedTo)
                                    .Include(x => x.CreatedBy)
                                    .Include(x => x.Status).FirstOrDefault(x=>x.Id == jobRequest.Id);

                var createdBy = await _userService.CheckExtistAsync(new User() { Login = jobRequest.CreatedBy });
                var assignedTo = await _userService.CheckExtistAsync(new User() { Login = jobRequest.AssignedTo });
                var status = await GetStatus(jobRequest.Status);

                if(job != null)
                {
                    job.Status = status;
                    job.AssignedTo = assignedTo;
                    job.CreatedBy = createdBy;
                    job.Title = jobRequest.Title;
                    job.Description = jobRequest.Description;
                    job.CreatedAt = jobRequest.CreatedAt;
                    job.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return job;
                }
                else
                {
                    return null!;
                }
                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }                        
        }

        public async Task<Status> GetStatus(int? id)
        {
            try
            {
                var jobStatus = await _context.Statuses.FindAsync(id);
                if (jobStatus == null)
                    return null!;
                return jobStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
        }
    }
}
