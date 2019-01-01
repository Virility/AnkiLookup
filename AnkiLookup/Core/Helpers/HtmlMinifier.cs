using System.Text.RegularExpressions;

namespace AnkiLookup.Core.Helpers
{
    public static class HtmlMinifier
    {
        private static readonly Regex BetweenScriptTagsRegEx = new Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>", RegexOptions.Compiled);
        private static readonly Regex BetweenTagsRegex = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}", RegexOptions.Compiled);
        private static readonly Regex MatchBodyRegEx = new Regex(@"</body>", RegexOptions.Compiled);

        public static string Minify(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return html;

            var matches = BetweenScriptTagsRegEx.Matches(html);
            html = BetweenScriptTagsRegEx.Replace(html, string.Empty);
            html = BetweenTagsRegex.Replace(html, string.Empty);

            var body = string.Empty;
            foreach (Match match in matches)
                body += match.ToString();
            return MatchBodyRegEx.Replace(html, body + "</body>");
        }
    }
}