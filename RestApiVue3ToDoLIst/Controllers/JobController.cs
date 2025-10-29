using Microsoft.AspNetCore.Mvc;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Requests;
using RestApiVue3ToDoLIst.Data.Models.DTO.Responces;
using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Controllers
{
    [ApiController]
    [Route("api")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository<Job, JobRequest> _jobContext;
        private readonly ILogger<JobController> _logger;

        public JobController(IJobRepository<Job, JobRequest> context, ILogger<JobController> logger)
        {
            _jobContext = context;
            _logger = logger;
        }


        [HttpPost]
        [Route("job")]
        public async Task<IResult> CreateJob([FromBody] JobRequest jobRequest)
        {
            try
            {
                if (jobRequest == null)
                {
                    _logger.LogError("BadRequest.CreateJob - нет параметров!");
                    return Results.Json(new JobResponce { Result = false, Message = "CreateJob - Нет параметров" }, statusCode: 400);
                }

                var result = await _jobContext.AddAsync(jobRequest);

                if (result)
                {
                    _logger.LogInformation("Success.CreateJob - Задача успешно заведена!");
                    return Results.Json("Задача успешно заведена!", statusCode: 200);
                }
                else
                {
                    _logger.LogError("BadRequest.CreateJob - Ошибка при добавлении задачи!");                    
                    return Results.Json(new JobResponce { Result = false, Message = "CreateJob - Ошибка при добавлении задачи" }, statusCode: 400);
                }
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpGet]
        [Route("jobs")]
        public async Task<IResult> GetJobs()
        {
            try
            {
                var result = await _jobContext.GetAllAsync();
                if (result == null)
                {
                    _logger.LogError("BadRequest.GetJobs - Нет данных!");
                    return Results.BadRequest("Нет данных!");
                }

                _logger.LogInformation("Success.GetJobs - Задачи успешно отправлены!");
                var jobResponce = new JobResponce() { JobList = result.ToList() };
                return Results.Json(jobResponce, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpGet]
        [Route("job")]
        public async Task<IResult> GetJob([FromQuery] JobRequest jobRequest)
        {
            try
            {
                if (jobRequest == null)
                {
                    _logger.LogError("BadRequest.GetJob - нет параметров!");
                    return Results.Json(new JobResponce { Result = false, Message = "GetJob - Нет параметров" }, statusCode: 400);
                }

                var result = await _jobContext.GetAsync(jobRequest);

                if (result == null)
                {
                    _logger.LogError("BadRequest.GetJob - Нет данных!");
                    return Results.BadRequest("Нет данных!");
                }

                _logger.LogInformation("Success.GetJob - Задача успешно отправлена!");
                return Results.Json(result, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }


        [HttpPut]
        [Route("job")]
        public async Task<IResult> UpdateJob([FromBody] JobRequest jobRequest)
        {
            try
            {
                if (jobRequest == null)
                {
                    _logger.LogError("BadRequest.UpdateJob - нет параметров!");
                    return Results.Json(new JobResponce { Result = false, Message = "UpdateJob - Нет параметров" }, statusCode: 400);
                }

                var result = await _jobContext.UpdateAsync(jobRequest);

                if (result != null)
                {
                    _logger.LogInformation("Success.GetJob - Задача успешно обновлена!");
                    return Results.Json(new JobResponce { Result = true, Job = result }, statusCode: 200);
                }

                _logger.LogError("BadRequest.UpdateJob - Нет данных!");
                return Results.BadRequest("Нет данных!");
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpDelete]
        [Route("job")]
        public async Task<IResult> DropJob([FromBody] JobRequest jobRequest)
        {
            try
            {
                if (jobRequest == null)
                {
                    _logger.LogError("BadRequest.DropJob - нет параметров!");
                    return Results.Json(new JobResponce { Result = false, Message = "DropJob - Нет параметров" }, statusCode: 400);
                }

                var result = await _jobContext.DropAsync(jobRequest);
                if (result)
                {
                    _logger.LogInformation("Success.DropJob - Задача успешно удалена!");
                    return Results.Json(new JobResponce { Result = true, Message = "Success.DropJob" }, statusCode: 200);
                }

                _logger.LogError("BadRequest.DropJob - Ошибка!");
                return Results.Json(new JobResponce { Result = false, Message = "Ошибка!" }, statusCode: 500);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        private IResult InternalServerErrorHandler(string message)
        {
            _logger.LogError("Error.JobController - Произошла непредвиденная ошибка! {message}", message);
            return Results.Json(new JobResponce { Result = false, Message = message }, statusCode: 500);
        }
    }
}