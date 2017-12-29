using System.Threading.Tasks;
using Identity.Interface.TransferObjects;

namespace Identity.Interface.Services
{
    public interface IClientService
    {
        Task<Client> LoadAsync(string clientId);
        Task RegisterAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(string clientId);
    }
}