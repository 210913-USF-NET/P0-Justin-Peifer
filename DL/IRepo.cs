using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        StoreFront AddStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        StoreFront UpdateStoreFront(StoreFront storeToUpdate);
    }
}