using System.Threading.Tasks;
using Identity.Interface.TransferObjects;

namespace Identity.Interface.Services
{
    public interface IUpdateClient
    {
        Task UpdateAsync(Client client);
    }
}