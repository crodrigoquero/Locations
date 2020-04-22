using Locations.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Repository.iRepository
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUserAsync(int userId);

        ICollection<User> GetUsersFromCity(string city);

        Task<ICollection<User>> GetUsersFromLondon();

        bool UserExist(string name);
        bool UserExist(int id);

        // crud
        bool CreateUser(Trail trail);
        bool UpdateUser(Trail trail);
        bool DeleteUser(Trail trail);



        bool Save();

    }
}
