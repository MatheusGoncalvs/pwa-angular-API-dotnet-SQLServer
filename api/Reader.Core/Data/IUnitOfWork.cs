using System.Threading.Tasks;

namespace Reader.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}