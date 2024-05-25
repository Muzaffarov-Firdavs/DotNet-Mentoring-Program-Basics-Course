using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    internal class ResponseModelStub : IResponseModel
    {
        private readonly IDictionary<string, string> _data = new Dictionary<string, string>();

        public void AddAttribute(string key, string value)
        {
            _data.Add(key, value);
        }

        public string GetAttribute(string key)
        {
            return _data[key];
        }

        public string GetActionResult()
        {
            if (_data.ContainsKey("action_result"))
                return _data["action_result"];

            return null;
        }
    }
}