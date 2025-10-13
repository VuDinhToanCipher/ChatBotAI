using ChatBotAI.Domain.Users;

namespace ChatBotAI.Infrastructure.Respository
{
    public class UserRespository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public Task<User> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
