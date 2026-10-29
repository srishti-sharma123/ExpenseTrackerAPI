using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}