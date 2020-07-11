using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Helpers.Formatters
{
    public interface IWordFormatter
    {
        string Render(Word word);
    }
}