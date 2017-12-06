using System.Threading.Tasks;

namespace Identity.Interface.Services
{
    public interface IDeleteClient
    {
        Task DeleteAsync(string clientId);
    }
}