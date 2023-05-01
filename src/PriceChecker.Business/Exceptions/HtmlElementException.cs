public class HtmlElementException : Exception
{
    public HtmlElementException()
    {
    }

    public HtmlElementException(string message)
        : base(message)
    {
    }

    public HtmlElementException(string message, Exception inner)
        : base(message, inner)
    {
    }
}