using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {

        //Storefronts:
        //StoreFront CreateStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        
        


        //Users:
        // int UserEmailSearch();
        List<User> GetAllUsers();
        //StoreFront UpdateStoreFront(StoreFront storeToUpdate);

        //Products:
        List<Product> GetAllProducts();
        Product ProductByID(int id);

        // List<Product> ProductByStoreID();
        StoreFront StoreById(int Id);

        //inventory
        List<Inventory> GetAllInventory();
    }
}