using ChatBotAI.Application.DTOs.UserDTO;
using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDTO user);
        Task<User> UpdateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
