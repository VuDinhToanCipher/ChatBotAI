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


        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserId == userId);
            if (user == null)
            {
                return false;
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> DisplayUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            return user;
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
                Message = Convert.ToString(valid.UserId) + "|" + valid.Name + "|" + valid.Email + "|" + valid.IsAdmin
            };
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x=> x.UserId == user.UserId);
            if (result == null) return null;
            result.UserName = user.UserName;
            result.IsAdmin = user.IsAdmin;
            result.Email = user.Email;
            result.Name = user.Name;
            await _context.SaveChangesAsync();
            return result;

        }
    }
}
