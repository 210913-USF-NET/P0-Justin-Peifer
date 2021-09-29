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



            System.Console.WriteLine("");
            //for formatting
            switch (menuString.ToLower())
            {
                case "manager":
                    return new ManagerMenu(new BL(new DBRepo(context)));
                case "login":
                    return new LoginMenu(new BL(new DBRepo(context))); 
                case "newuser":
                    return new NewUserMenu(new BL(new DBRepo(context)));
                case "order":
                    return new OrderMenu(new BL(new DBRepo(context)));
                case "inventory":
                    return new InventoryMenu(new BL(new DBRepo(context)));
                case "vieworders":
                    return new OrderViewer(new BL(new DBRepo(context)));
                case "customermanagement":
                    return new CustomerManagementMenu(new BL(new DBRepo(context)));
                default:
                    return null;
            }
        }
    }
}