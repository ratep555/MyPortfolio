using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<string> GetRoleName(string userId);
        Task<IQueryable<UserToReturnDto>> GetUsersWithSearching(QueryParameters queryParameters, string email);
        Task<IQueryable<UserToReturnDto>> GetUsersWithPaging(QueryParameters queryParameters, string email);
        Task<AppUser> FindUserByIdAsync(string userId);
        Task LockUser(string id);
        Task UnlockUser(string id);


    }
}