using ChatBotAI.Domain.Response;
namespace ChatBotAI.Domain.Users
{
    public interface IUserRespository
    {
        Task<ResponseModel> AddUserAsync(User user);
        Task<ResponseModel> LoginAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<List<User>> DisplayUserAsync();
        Task<User> GetUserAsync(Guid userId);
    }
}
