using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<string> GetRoleName(string userId);

    }
}