using ChatBotAI.Application.DTOs.UserDTO;
using ChatBotAI.Domain.Response;
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
        public async Task<ResponseModel> AddUserAsync(UserDTO user)
        {
            var NewUser = new User
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,     
            };
            return await _userRespository.AddUserAsync(NewUser);
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> LoginAsync(LoginDto loginDto)
        {
            var NewLogin = new User
            {
                Email = loginDto.Email,
                Password = loginDto.Password,
            };
           return await _userRespository.LoginAsync(NewLogin);
        }

        public Task<User> UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
