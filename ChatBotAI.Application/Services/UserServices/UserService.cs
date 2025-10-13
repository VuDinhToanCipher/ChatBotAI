using ChatBotAI.Application.DTOs.UserDTO;
using ChatBotAI.Domain.Users;

namespace ChatBotAI.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _userRespository;
        public UserService(IUserRespository _userRespository)
        {
            this._userRespository = _userRespository;
        }
        public Task<User> AddUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
