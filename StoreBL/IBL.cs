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

        StoreFront StoreById(int id);
        // StoreFront CreateStoreFront(StoreFront store);

        // StoreFront UpdateStoreFront(StoreFront StoreFrontToUpdate);
    }
}