using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, 
        ClaimsPrincipal user)
        {
             var email = user.FindFirstValue(ClaimTypes.Email);

             return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        } 

        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user) 
        {
             return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}