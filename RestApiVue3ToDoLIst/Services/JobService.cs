using Microsoft.EntityFrameworkCore;
using RestApiVue3ToDoLIst.Controllers;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Requests;
using RestApiVue3ToDoLIst.Data.Models.Entities;

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
            if(jobRequest == null)
                return false;

            var createdBy = await _userService.CheckExtistAsync(new User() { Login = jobRequest.CreatedBy });
            var assignedTo = await _userService.CheckExtistAsync(new User() { Login = jobRequest.AssignedTo });
            var status = await GetStatus(jobRequest.Status);

            var newJob = new Job()
            {
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

        public async Task<bool> DropAsync(JobRequest jobRequest)
        {
            if (jobRequest == null)
                return false;

            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobRequest.Id);
            if (job == null)
                return false;
            
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Job> GetAsync(JobRequest jobRequest)
        {                        
            var job = await _context.Jobs
                                    .Include(x => x.AssignedTo)
                                    .Include(x => x.CreatedBy)
                                    .Include(x => x.Status)
                                    .FirstOrDefaultAsync(x => x.Id == jobRequest.Id);                
            return job ?? null!;                       
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var jobs = await _context.Jobs
                                        .Include(x => x.AssignedTo)
                                        .Include(x => x.CreatedBy)
                                        .Include(x => x.Status)
                                        .ToListAsync();
            return jobs ?? null!;
        }

        public async Task<Job> UpdateAsync(JobRequest jobRequest)
        {
            if (jobRequest == null)
                return null;
             
            var job = _context.Jobs
                                .Include(x => x.AssignedTo)
                                .Include(x => x.CreatedBy)
                                .Include(x => x.Status).FirstOrDefault(x => x.Id == jobRequest.Id);

            var createdBy = await _userService.CheckExtistAsync(new User() { Login = jobRequest.CreatedBy });
            var assignedTo = await _userService.CheckExtistAsync(new User() { Login = jobRequest.AssignedTo });
            var status = await GetStatus(jobRequest.Status);

            if (job == null)
                return null!;
            
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

        public async Task<Status> GetStatus(int? id)
        {            
            var jobStatus = await _context.Statuses.FindAsync(id);
            return jobStatus ?? null!;            
        }
    }
}
