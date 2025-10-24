using Microsoft.AspNetCore.Mvc;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Responces;
using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Controllers
{
    [ApiController]
    [Route("api")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository<Job> _jobContext;
        private readonly ILogger<JobController> _logger;

        public JobController(IJobRepository<Job> context, ILogger<JobController> logger)
        {
            _jobContext = context;
            _logger = logger;
        }


        [HttpPost]
        [Route("job")]
        public async Task<IResult> CreateJob([FromBody] Job job)
        {
            try
            {
                if (job == null)
                {
                    _logger.LogError("BadRequest.CreateJob - ��� ����������!");
                    return Results.Json(new UserResponce { Result = false, Message = "CreateJob - ��� ����������" }, statusCode: 400);
                }

                var result = await _jobContext.AddAsync(job);

                if (result)
                {
                    _logger.LogInformation("Success.CreateJob - ������ ������� ��������!");
                    return Results.Json("������ ������� ��������!", statusCode: 200);
                }
                else
                {
                    _logger.LogError("BadRequest.CreateJob - ������ ��� ���������� ������!");
                    return Results.BadRequest("BadRequest.CreateJob - ������ ��� ���������� ������!");
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
                    _logger.LogError("BadRequest.GetJobs - ��� ������!");
                    return Results.BadRequest("��� ������!");
                }

                _logger.LogInformation("Success.GetJobs - ������ ������� ����������!");
                return Results.Json(result, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpGet]
        [Route("job")]
        public async Task<IResult> GetJob([FromQuery] int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    _logger.LogError("BadRequest.GetJob - ��� ����������!");
                    return Results.Json(new UserResponce { Result = false, Message = "GetJob - ��� ����������" }, statusCode: 400);
                }

                var result = await _jobContext.GetAsync(id);

                if (result == null)
                {
                    _logger.LogError("BadRequest.GetJob - ��� ������!");
                    return Results.BadRequest("��� ������!");
                }

                _logger.LogInformation("Success.GetJob - ������ ������� ����������!");
                return Results.Json(result, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }


        [HttpPut]
        [Route("job")]
        public async Task<IResult> UpdateJob([FromBody] Job job)
        {
            try
            {
                if (job == null)
                {
                    _logger.LogError("BadRequest.UpdateJob - ��� ����������!");
                    return Results.Json(new UserResponce { Result = false, Message = "UpdateJob - ��� ����������" }, statusCode: 400);
                }

                var result = await _jobContext.UpdateAsync(job);

                if (result != null)
                {
                    _logger.LogInformation("Success.GetJob - ������ ������� ���������!");
                    return Results.Json(result, statusCode: 200);
                }

                _logger.LogError("BadRequest.UpdateJob - ��� ������!");
                return Results.BadRequest("��� ������!");
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpDelete]
        [Route("job")]
        public async Task<IResult> DropJob([FromBody] int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    _logger.LogError("BadRequest.DropJob - ��� ����������!");
                    return Results.Json(new UserResponce { Result = false, Message = "DropJob - ��� ����������" }, statusCode: 400);
                }

                var result = await _jobContext.DropAsync(id);
                if (result)
                {
                    _logger.LogInformation("Success.DropJob - ������ ������� �������!");
                    return Results.Json(new UserResponce { Result = true, Message = "Success.DropJob" }, statusCode: 200);
                }

                _logger.LogError("BadRequest.DropJob - ������!");
                return Results.Json(new UserResponce { Result = false, Message = "������!" }, statusCode: 500);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        private IResult InternalServerErrorHandler(string message)
        {
            _logger.LogError("Error.JobController - ��������� �������������� ������! {message}", message);
            return Results.Json(new UserResponce { Result = false, Message = message }, statusCode: 500);
        }
    }
}