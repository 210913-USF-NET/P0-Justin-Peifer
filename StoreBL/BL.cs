using System;
using Models;
using System.Collections.Generic;
using DL;

namespace SBL
{
    public class BL : IBL
    {
        private IRepo _repo;
        
        //IRepo repo is the dependency of Business logic, that is being passed in aka "injected"
        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }

        // public StoreFront CreateStoreFront(StoreFront store)
        // {
        //     return _repo.CreateStoreFront(store);
        // }

        // public StoreFront UpdateStoreFront(StoreFront storeToUpdate)
        // {
        //     //add logic to update StoreFront
        //     return _repo.UpdateStoreFront(storeToUpdate);
        // }
        
        // public User AddUser(User user)
        // {
        //     return _repo.AddUser(user);
        // }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        // public User UpdateUser(User userToUpdate)
        // {
        //     return _repo.UpdateUser(userToUpdate);
        // } 
    }
}
