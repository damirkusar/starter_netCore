using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Interface.TransferObjects;

namespace Identity.Interface.Services
{
    public interface ILoadUser
    {
        Task<IList<User>> LoadAsync();
        Task<User> LoadAsync(Guid userId);
    }
}