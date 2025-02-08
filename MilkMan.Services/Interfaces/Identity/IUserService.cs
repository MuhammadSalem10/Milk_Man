using MilkMan.Domain.Entities;


namespace MilkMan.Application.Interfaces.Identity
{
    public interface IUserService
    {
        public Task<ApplicationUser> RegisterUserAsync(string email, string password, string phoneNumber, string role);
        public Task<ApplicationUser?> FindByIdAsync(string userId);
        public Task DeleteAsync(ApplicationUser user);
    }

    public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string>? Errors { get; set; } // Optional for informative messages
    }
}
