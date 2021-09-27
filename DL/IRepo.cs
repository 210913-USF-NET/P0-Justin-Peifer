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

        int UpdateStock(StoreFront storeToUpdate, LineItem orderedProduct);

        int UpdateStock(Inventory inventoryToUpdate, int amountToAdd);//method overloading


        //Users:
        // int UserEmailSearch();
        List<User> GetAllUsers();
        User AddUser(User user);
        
        //Products:
        List<Product> GetAllProducts();
        Product ProductByID(int id);

        // List<Product> ProductByStoreID();
        StoreFront StoreById(int Id);

        //inventory
        List<Inventory> GetAllInventory();

        //Orders
        Order NewOrder(int userId);
        List <Order> OrderByUserId(int UserId);
        Order OrderInfoById(int id);
    }
}