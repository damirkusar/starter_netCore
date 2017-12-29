using System;
using System.Threading.Tasks;

namespace Identity.Interface.Services
{
    public interface IDeleteUser
    {
        Task DeleteAsync(Guid userId);
    }
}