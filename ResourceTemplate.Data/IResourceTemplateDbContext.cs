using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceTemplate.Data.Models;

namespace ResourceTemplate.Data
{
    public interface IResourceTemplateDbContext
    {
        DbSet<Resource> Resources { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}