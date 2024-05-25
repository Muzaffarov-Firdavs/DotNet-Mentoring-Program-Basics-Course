using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    internal class UserDaoStub : IUserDao
    {
        private readonly IDictionary<int, IUser> _data = new Dictionary<int, IUser>
        {
            { 1, new UserStab() }
        };

        public IUser GetUser(int id)
        {
            if (_data.ContainsKey(id))
                return _data[id];
            return null;
        }
    }
}