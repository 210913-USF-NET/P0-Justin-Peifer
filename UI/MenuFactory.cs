using DL;
using SBL;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            //this is an example of dependency injection
            //I'm "injecting" an instance of business logic layer to restaurant menu, and an implementation of 
            //IRepo to business logic
            // IRepo dataLayer = new FileRepo();
            // IBL businessLogic = new BL(dataLayer);
            // IMenu restaurantMenu = new RestaurantMenu(businessLogic);

            // restaurantMenu.Start();
            switch (menuString.ToLower())
            {
                case "manager":
                    return new ManagerMenu();
                case "storefront":
                    return new FranchiseMenu(new BL(new StoreFrontRepo()));
                case "login":
                    //return new LoginMenu(new BL(new UserRepo()));
                // case "newuser":
                //     return new AddUser(new BL(new UserRepo()));
                default:
                    return null;
            }
        }
    }
}