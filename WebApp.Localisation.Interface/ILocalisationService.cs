using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebApp.Localisation.Interface
{
    public interface ILocalisationService
    {
        Task<JObject> GetJsonLocalisationsAsync(string languageIsoAlpha2);
    }
}