using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {

        //Storefronts:
        //StoreFront CreateStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();

        Order PlaceOrder(StoreFront storeOrderedFrom, Order order);

        void UpdateStock(StoreFront storeToUpdate, List<LineItem> orderedProduct);

        int UpdateStock(Inventory inventoryToUpdate, int amountToAdd, int storeId);//method overloading
        

        //Users:
        User MakeUserManager(User newManager);
        List<User> GetAllUsers();
        User AddUser(User user);
        List<User> SearchUser(string search);
        //Products:
        List<Product> GetAllProducts();
        Product ProductByID(int id);

        // List<Product> ProductByStoreID();
        StoreFront StoreById(int Id);

        //inventory
        List<Inventory> GetAllInventory();

        //Orders
        // void ClearBadOrder(int orderId);
        Order NewOrder(int userId);
        List <Order> OrderByUserId(int UserId);
        Order OrderInfoById(int id);
    }
}