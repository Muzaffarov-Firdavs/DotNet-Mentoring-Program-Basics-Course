namespace Task3.DoNotChange
{
    public interface IResponseModel
    {
        void AddAttribute(string key, string value);

        string GetAttribute(string key);
    }
}