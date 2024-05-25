using System;
using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3
{
    public class UserTaskController
    {
        private const string actionResult = "action_result";
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                var task = new UserTask(description);
                _taskService.AddTaskForUser(userId, task);
            }
            catch (Exception ex) when (ex is InvalidInputException || ex is NotFoundException || ex is AlreadyExistException)
            {
                model.AddAttribute(actionResult, ex.Message);
                return false;
            }
            catch (Exception)
            {
                model.AddAttribute(actionResult, "Internal server error");
                return false;
            }

            return true;
        }

    }
}