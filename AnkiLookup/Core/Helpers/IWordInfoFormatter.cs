using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Helpers
{
    public interface IWordInfoFormatter
    {
        string Render(CambridgeWordInfo wordInfo);
    }
}