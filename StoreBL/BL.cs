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
        public Order NewOrder(int userId)
        {
            return _repo.NewOrder(userId);
        }


        public Order OrderInfoById(int id){
            return _repo.OrderInfoById(id);
        }
        public List <Order> OrderByUserId(int UserId){
            return _repo.OrderByUserId(UserId);
        }
        public Order PlaceOrder(StoreFront store, Order order){
            return _repo.PlaceOrder(store, order);
        }
        
        public int UpdateStock(StoreFront storeToUpdate, LineItem orderedProduct){
            return _repo.UpdateStock(storeToUpdate, orderedProduct);
        }

        public int UpdateStock(Inventory inventoryToUpdate, int amountToAdd){
                    return _repo.UpdateStock(inventoryToUpdate, amountToAdd);
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
