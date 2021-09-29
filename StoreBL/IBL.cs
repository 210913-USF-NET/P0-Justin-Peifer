using Models;
using System.Collections.Generic;
using DL;

namespace SBL
{
    //this is an interface for all business logics i may or may not create
    //interface is a contract, all classes that implements an interface
    //must have the methods listed in the interface
    public interface IBL
    {
        List<StoreFront> GetAllStoreFronts();
        List<User> GetAllUsers();
        List<Product> GetAllProducts();
        List<Inventory> GetAllInventory();
        User AddUser(User user);

        User MakeUserManager(User newManager);
        List<User> SearchUser(string search);
        Order NewOrder(int userId);
        Order PlaceOrder(StoreFront store, Order order);
        // void ClearBadOrder(int orderId);
        List <Order> OrderByUserId(int UserId);
        void UpdateStock(StoreFront storeToUpdate, List<LineItem> orderedProduct);
        
        int UpdateStock(Inventory inventoryToUpdate, int amountToAdd, int storeId);

        StoreFront StoreById(int id);
        // StoreFront CreateStoreFront(StoreFront store);

        // StoreFront UpdateStoreFront(StoreFront StoreFrontToUpdate);
    }
}