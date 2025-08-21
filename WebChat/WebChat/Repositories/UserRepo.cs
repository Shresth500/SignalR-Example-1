using Microsoft.EntityFrameworkCore;
using WebChat.Data;
using WebChat.Models;

namespace WebChat.Repositories;

public class UserRepo(ApplicationDbContext context) : IUserRepo
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        var data = await context.User.FirstOrDefaultAsync(x => x.Id == id);
        return data;
    }

    public async Task<List<User>> GetUserListAsync(int userid)
    {
        var data = await context.User.Where(x => x.Id!=userid).ToListAsync();
        return data;
    }
}
