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

        public User AddUser(User user)
        {
            return _repo.AddUser(user);
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
            
        }

        public Product ProductByID(int id)
        {
            return _repo.ProductByID(id);
            
        }
        //products

        public List<Inventory> GetAllInventory()
        {
            return _repo.GetAllInventory();
        }

        public StoreFront StoreById(int id)
        {
            return _repo.StoreById(id);
            }
    }
}
