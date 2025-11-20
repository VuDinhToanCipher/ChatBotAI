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
        public async Task<ResponseModel> AddUserAsync(RegisterDTO user)
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

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
           return await _userRespository.DeleteUserAsync(userId);
        }

        public async Task<List<UserDTO>> DisplayUserAsync()
        {
            var users =  await _userRespository.DisplayUserAsync();
            return users.Select(x => new UserDTO
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Name = x.Name,
                Email = x.Email,
                IsAdmin = x.IsAdmin

            }).ToList();
        }

        public async Task<UserDTO> GetUserAsync(Guid userId)
        {
            var result = await  _userRespository.GetUserAsync(userId);
            if (result == null) return null;
            return new UserDTO
            {
                UserId = result.UserId,
                UserName = result.UserName,
                Email = result.Email,
                IsAdmin = result.IsAdmin,
                Name = result.Name,
            };
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

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            var UserUpdate = new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Name = user.Name,
            };
            var updated = await _userRespository.UpdateUserAsync(UserUpdate);
            if (updated == null) return null;
            return new UserDTO
            {
                UserId = updated.UserId,
                Name = updated.Name,
                UserName = updated.UserName,
                Email = updated.Email,
                IsAdmin = updated.IsAdmin
            };
        }
    }
}
