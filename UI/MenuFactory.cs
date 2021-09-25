using DL;
using SBL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace UI
{
    public class MenuFactory
    {
        
        public static Models.User currentUser;
        public static IMenu GetMenu(string menuString)
        { 
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<PeiferP0Context> options = new DbContextOptionsBuilder<PeiferP0Context>()
            .UseSqlServer(connectionString).Options;
            PeiferP0Context context = new PeiferP0Context(options);
            //this is an example of dependency injection
            //I'm "injecting" an instance of business logic layer to restaurant menu, and an implementation of 
            //IRepo to business logic
            // IRepo dataLayer = new FileRepo();
            // IBL businessLogic = new BL(dataLayer);
            // IMenu restaurantMenu = new RestaurantMenu(businessLogic);

            // restaurantMenu.Start();
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            //for formatting
            switch (menuString.ToLower())
            {
                case "manager":
                System.Console.WriteLine("");
                    return new ManagerMenu(new BL(new DBRepo(context)));
                case "storefront":
                    return new FranchiseMenu(new BL(new DBRepo(context)));
                case "login":
                    return new LoginMenu(new BL(new DBRepo(context))); 
                case "newuser":
                    System.Console.WriteLine("This will bring you to the new user portal in the future.");
                    return null;
                //     return new NewUserMenu(new BL(new DBRepo(context)));
                case "order":
                    return new OrderMenu(new BL(new DBRepo(context)));
                //case "exit":
                    //I want to make a function that will close all instances open, so you don't have a weird going back in each instance.
                default:
                    return null;
            }
        }
    }
}