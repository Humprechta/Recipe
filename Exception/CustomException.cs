namespace Recipe
{
    /// <summary>
    /// Exception for pre-specified user friendly message
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
