using Microsoft.AspNetCore.Mvc;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Responces;
using RestApiVue3ToDoLIst.Data.Models.Entities;

namespace RestApiVue3ToDoLIst.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User> _userContext;
        private readonly ILogger<JobController> _logger;

        public UserController(IUserRepository<User> context, ILogger<JobController> logger)
        {
            _userContext = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("user")]
        public async Task<IResult> AddUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("AddUser.NoParams");
                    return Results.Json(new UserResponce { Result = false, Message = "AddUser - Нет параметров" }, statusCode: 400);
                }

                var result = await _userContext.GetAsync(user.Login, user.Password);

                if (result != null)
                {
                    _logger.LogWarning("Success.AddUser - Пользователь с таким логином уже существует! {Login} ", user.Login);
                    return Results.Json(new UserResponce { Result = false, Message = "Пользователь с таким логином уже существует!" }, statusCode: 409);
                }
                else
                {
                    await _userContext.AddAsync(user);
                    _logger.LogInformation("Success.AddUser - Пользователь {Login} успешно создан!", user.Login);
                    return Results.Json(new UserResponce { Result = true, Id = user.Id, Login = user.Login! }, statusCode: 200);
                }
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }


        [HttpDelete]
        [Route("user")]
        public async Task<IResult> DropUser([FromBody] int? id, string password)
        {
            try
            {
                if (!id.HasValue && password == null)
                {
                    _logger.LogError("BadRequest.DropUser.NoParams");
                    return Results.Json(new UserResponce { Result = false, Message = "DropUser - Нет параметров" }, statusCode: 400);
                }

                var result = await _userContext.DropAsync(id, password);
                if (result == false)
                {
                    _logger.LogError("BadRequest.DropUser - Запрос удаление пользователя");
                    return Results.Json(new UserResponce { Result = false, Message = "DropUser - Нет данных!" }, statusCode: 204);
                }

                _logger.LogInformation("Succes.DropUser - Запрос удаления пользователя");
                return Results.Json(new UserResponce { Result = true, Id = id!.Value }, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }


        [HttpPut]
        [Route("user")]
        public async Task<IResult> UpdateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("BadRequest.UpdateUser.NoParams");
                    return Results.Json(new UserResponce { Result = false, Message = "UpdateUser - Нет параметров" }, statusCode: 400);
                }

                var result = await _userContext.UpdateAsync(user);
                if (result == null)
                {
                    _logger.LogInformation("BadRequest.UpdateUser - Пользователь отсутсвует");
                    return Results.Json(new UserResponce { Result = false, Message = "UpdateUser - Нет данных!" }, statusCode: 404);
                }
                _logger.LogInformation("Success.UpdateUser - Обновление пользователя");
                return Results.Json(new UserResponce { Result = true, Message = "Success.UpdateUser - Обновление пользователя" }, statusCode: 200);
            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpGet]
        [Route("user")]
        public async Task<IResult> GetUser([FromQuery] string? login, string? password)
        {
            try
            {
                if (login == null && password == null)
                {
                    _logger.LogError("BadRequest.GetUser.NoParams");
                    return Results.Json(new UserResponce { Result = false, Message = "GetUser - Нет параметров" }, statusCode: 400);
                }

                var result = await _userContext.GetAsync(login, password!);
                if (result == null)
                {
                    _logger.LogInformation("Success.GetUser - Пользователь отсутсвует");
                    return Results.Json(new UserResponce { Result = false, Message = "GetUser - Нет данных!" }, statusCode: 204);
                }

                _logger.LogInformation("Success.GetUser - Запрос пользователя");
                return Results.Json(new UserResponce { Result = true, Login = result.Login!, Id = result.Id }, statusCode: 200);

            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        [HttpGet]
        [Route("users")]
        public async Task<IResult> GetUsersInfo([FromQuery] string? login, string? password)
        {
            try
            {
                if (login == null && password == null)
                {
                    _logger.LogError("BadRequest.GetUsersInfo.NoParams");
                    return Results.Json(new UserResponce { Result = false, Message = "GetUsersInfo - Нет параметров" }, statusCode: 400);
                }

                var result = await _userContext.GetAllAsync(login, password!);
                if (result == null)
                {
                    _logger.LogInformation("Success.GetUsersInfo - Пользователи отсутсвуют");
                    return Results.Json(new UserResponce { Result = false, Message = "GetUsersInfo - Нет данных!" }, statusCode: 204);
                }

                var usersDict = new Dictionary<string, string>();
                foreach (var user in result)
                {
                    usersDict.Add(user.Login!, user.SurName + " " + user.Name + " " + user.LastName);
                }

                _logger.LogInformation("Success.GetUser - Запрос пользователя");
                return Results.Json(new UserResponce { Result = true, UsersDict = usersDict }, statusCode: 200);

            }
            catch (Exception ex)
            {
                return InternalServerErrorHandler(ex.Message);
            }
        }

        private IResult InternalServerErrorHandler(string message)
        {
            _logger.LogError("Error.UserController - Произошла непредвиденная ошибка! {message}", message);
            return Results.Json(new UserResponce { Result = false, Message = message }, statusCode: 500);
        }
    }
}