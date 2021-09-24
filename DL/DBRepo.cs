using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DL
{
    public class DBRepo : IRepo
    {
        //dbcontext
        private Entity.PeiferP0Context _context;

        public DBRepo(Entity.PeiferP0Context context){
            _context = context;
        }

        // public Model.User AddUser(Model.User user){

        // }

        // public Model.User findUserById(int Id){
        //     List<Model.User> allUsers = GetAllUsers();
        //     foreach (Model.User user in allUsers)
        //     {
        //         if (user.Id == Id){
        //             return user.Id;
        //             break;
        //         }
        //         else{
        //             throw InputInvalidException("User not found.");
        //             goto loginstart;
                    
        //         }
                
        //     }

        // }

        // public int UserEmailSearch(string loginEmail){
        //     List<Model.User> allUsers = GetAllUsers();
        //     foreach (User user in allUsers)
        //     {
        //         if (user.Email == loginEmail){
        //             return user.Id;
        //             break;
        //         }
        //         else{
        //             System.Console.WriteLine("User not found.");
        //                 return -1;
        //         }
                
        //     }

        // }

        public List<Model.User> GetAllUsers(){

            return _context.Users.Select(
                User => new Model.User() {
                    Id = User.Id,
                    Email = User.Email,
                    Password = User.Password,
                    Name = User.Name,
                    Age = User.Age,
                    Access = User.ManagerAccess
                    }
            ).ToList();

        }

        // public User UpdateUser(User userToUpdate){

        // }

        public List<Model.StoreFront> GetAllStoreFronts(){

            //same as select * from StoreFront in sql query
            return _context.StoreFronts.Select(
                Storefront => new Model.StoreFront() {
                    Id = Storefront.Id,
                    State = Storefront.State,
                    Zipcode = Storefront.ZipCode
                    }
            ).ToList();
        }
    }
}