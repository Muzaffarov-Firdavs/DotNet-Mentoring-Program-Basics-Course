using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    internal class UserStab : IUser
    {
        public IList<UserTask> Tasks { get; } = new List<UserTask>
        {
            new UserTask("task1"),
            new UserTask("task2"),
            new UserTask("task3")
        };
    }
}