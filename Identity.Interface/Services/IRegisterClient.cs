using System.Threading.Tasks;
using Identity.Interface.TransferObjects;

namespace Identity.Interface.Services
{
    public interface IRegisterClient
    {
        Task RegisterAsync(RegisterClient client);
    }
}