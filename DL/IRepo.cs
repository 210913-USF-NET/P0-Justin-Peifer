using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        //StoreFront CreateStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        
        List<User> GetAllUsers();
        //StoreFront UpdateStoreFront(StoreFront storeToUpdate);
    }
}