using System.Collections.Generic;

namespace Task3.DoNotChange
{
    public interface IUser
    {
        IList<UserTask> Tasks { get; }
    }
}