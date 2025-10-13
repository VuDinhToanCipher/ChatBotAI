namespace ChatBotAI.Domain.Users
{
    public interface IUserRespository
    {
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
