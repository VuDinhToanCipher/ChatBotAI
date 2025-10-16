using ChatBotAI.Application.DTOs.UserDTO;
using ChatBotAI.Domain.Response;
using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ResponseModel> AddUserAsync(UserDTO user);
        Task<ResponseModel> LoginAsync(LoginDto loginDto);
        Task<User> UpdateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
