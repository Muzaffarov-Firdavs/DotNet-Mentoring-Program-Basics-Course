namespace FizzBuzzs
{
    public interface IFizzBuzz
    {
        string GetFizzBuzz(int number);
        IEnumerable<string> GetFizzBuzzRange(int start, int end);
    }
}
