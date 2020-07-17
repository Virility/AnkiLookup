using AnkiLookup.Core.Models;
using System.Threading.Tasks;

namespace AnkiLookup.Core.Providers
{
    public interface IDictionaryResolver
    {
        Task<Word> GetWord(string wordText);
    }
}