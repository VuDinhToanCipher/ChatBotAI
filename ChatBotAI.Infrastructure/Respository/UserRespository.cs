using ChatBotAI.Domain.Response;
using ChatBotAI.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Respository
{
    public class UserRespository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<ResponseModel> AddUserAsync(User user)
        {
           var valid = await _context.Users.FirstOrDefaultAsync(x=>x.Email == user.Email);
            if (valid != null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "email đã tồn tại"
                };
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Đăng kí thành công"
            };
        }


        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> LoginAsync(User user)
        {
            var valid = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (valid == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Email chưa được đăng kí"
                };
            }
           if(valid.Password != user.Password)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Sai mật khẩu"
                };
            }
            return new ResponseModel
            {
                IsSuccess = true,
                Message = Convert.ToString(valid.UserId) + "|" + valid.Name + "|" + valid.Email
            };
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
