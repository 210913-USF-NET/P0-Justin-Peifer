using Models;
using System.Collections.Generic;
namespace DL
{
    public interface IUserRepo
    {
        User AddUser(User user);
        List<User> GetAllUsers();
        User UpdateUser(User userToUpdate);
    
    }
}