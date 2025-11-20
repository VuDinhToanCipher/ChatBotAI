using ChatBotAI.Application.DTOs.UserDTO;
using ChatBotAI.Domain.Response;
using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ResponseModel> AddUserAsync(RegisterDTO user);
        Task<ResponseModel> LoginAsync(LoginDto loginDto);
        Task<UserDTO> UpdateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<List<UserDTO>> DisplayUserAsync();
        Task<UserDTO> GetUserAsync(Guid userId);
    }
}
