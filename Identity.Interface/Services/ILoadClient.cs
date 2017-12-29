using System.Threading.Tasks;
using Identity.Interface.TransferObjects;

namespace Identity.Interface.Services
{
    public interface ILoadClient
    {
        Task<Client> LoadAsync(string clientId);
    }
}