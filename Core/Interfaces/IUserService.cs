using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<string> GetRoleName(string userId);
        Task<List<UserToReturnDto>> GetUsersWithSearchingAndPaging(QueryParameters queryParameters, string email);
        Task<int> GetCountForUsers();
        Task<AppUser> FindUserByIdAsync(string userId);
        Task LockUser(string id);
        Task UnlockUser(string id);


    }
}