namespace WordChallenge.Extensions
{
    using System;
    using System.Text;

    public static class ExceptionExtensions
    {
        public static string FullString(this Exception exc)
        {
            if (exc == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder(exc.Message);
            while (exc.InnerException != null)
            {
                exc = exc.InnerException;
                sb.Append($" / {exc.Message}");
            }

            return sb.ToString();
        }

    }
}
