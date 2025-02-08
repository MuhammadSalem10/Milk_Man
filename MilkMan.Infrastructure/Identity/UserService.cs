using Microsoft.AspNetCore.Identity;
using MilkMan.Application.Interfaces.Identity;
using MilkMan.Domain.Entities;
using System.Data;


namespace MilkMan.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser?> FindByIdAsync(string userId)
        {
            var user =  await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<ApplicationUser> RegisterUserAsync(string email, string password, string phoneNumber, string role)
        {

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber,
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return user;
            }
            else
            {
                throw new ApplicationException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
