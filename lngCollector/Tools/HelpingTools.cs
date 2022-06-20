using System.Text;

namespace lngCollector.Tools
{
    public static class HelpingTools
    {
        public static string? TruncateLongString(this string str, int maxLength) =>
            str?[0..Math.Min(str.Length, maxLength)];

        public static string GetAllInners(this Exception ex)
        {
            var nxt = ex;
            StringBuilder sb = new StringBuilder();
            while (nxt != null)
            {
                sb.Append($"{nxt.Message}\n");
                nxt = nxt.InnerException;
            }
            return sb.ToString();
        }
    }
}
