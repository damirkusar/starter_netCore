using System.Threading.Tasks;
using Identity.Interface.Models;

namespace Identity.Interface.Services
{
    public interface IRegisterClientService
    {
        Task RegisterAsync(RegisterClient client);
    }
}