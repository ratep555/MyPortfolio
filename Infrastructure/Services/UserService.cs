using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly PortfolioContext _context;
        public UserService(PortfolioContext context)
        {
            _context = context;
        }
        public async Task<string> GetRoleName(string userId)
        {
            var roleName = await (from r in _context.Roles
                                  join ur in _context.UserRoles
                                  on r.Id equals ur.RoleId
                                  join u in _context.Users.Where(u => u.Id == userId)
                                  on ur.UserId equals u.Id
                                  select r.Name
                                 ).FirstOrDefaultAsync();

            return roleName;
        }

        public async Task<List<UserToReturnDto>> GetUsersWithSearchingAndPaging(
            QueryParameters queryParameters, string email)
        {
            IQueryable<UserToReturnDto> user = (from u in _context.Users.Where(u => u.Email != email)
                                               join a in _context.AppUsers on u.Id equals a.Id                            
                                               select new UserToReturnDto 
                                               {
                                                   DisplayName = a.DisplayName,
                                                   Email = u.Email,
                                                   UserId = u.Id,
                                                   LockoutEnd = u.LockoutEnd
                                            }).AsQueryable().OrderBy(x => x.DisplayName);
            
            if (queryParameters.HasQuery())
            {
                user = user
                .Where(t => t.Email.Contains(queryParameters.Query));
            }

            user = user.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await user.ToListAsync();       
        }

        public async Task<int> GetCountForUsers()
        {
            return await _context.AppUsers.CountAsync();
        }
       
        public async Task<AppUser> FindUserByIdAsync(string userId)
        {
            return await _context.AppUsers.Where(a => a.Id == userId).FirstOrDefaultAsync();
        }

        public async Task LockUser(string id)
        {
            var userFromDb = await _context.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _context.SaveChanges();
        }
        public async Task UnlockUser(string id)
        {
            var userFromDb = await _context.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            userFromDb.LockoutEnd = null;

            _context.SaveChanges();
        }

    }
}









