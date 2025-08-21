using WebChat.DTOs;
using WebChat.Models;

namespace WebChat.Repositories;

public interface IUserRepo
{
    Task<List<User>> GetUserListAsync(int userid);
    Task<User?> GetUserByIdAsync(int id);
}
