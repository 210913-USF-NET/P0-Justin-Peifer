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

        public User MakeUserManager(User newManager)
        {
            return _repo.MakeUserManager(newManager);
        }

        public List<User> SearchUser(string search){
            return _repo.SearchUser(search);
        }

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

        // public void ClearBadOrder(int orderId){
        //     return ClearBadOrder (orderId);
        // }
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
        
        public void UpdateStock(StoreFront storeToUpdate, List<LineItem> orderedProduct){
            _repo.UpdateStock(storeToUpdate, orderedProduct);
        }

        public int UpdateStock(Inventory inventoryToUpdate, int amountToAdd, int storeId){
                    return _repo.UpdateStock(inventoryToUpdate, amountToAdd, storeId);
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
