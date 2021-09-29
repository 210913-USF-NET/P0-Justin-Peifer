using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBL;
using Models;
using Serilog;

namespace UI
{
    public class NewUserMenu : IMenu
    {
        private BL _bl;

        public NewUserMenu(BL bl)
        {
            _bl = bl;
        }
        public void Start()
        { 
            System.Console.WriteLine("Welcome! We're glad you decided to shop with us.");

            userbeginning:
            System.Console.WriteLine("Please type your name:");
            string newUserName = Console.ReadLine();

            System.Console.WriteLine("Please enter your age:");
            string newUserAgeString = System.Console.ReadLine();
            try{
            int testConversion = Int32.Parse(newUserAgeString);
            }
            catch(System.FormatException){
                Log.Information("Caught a FormatException. Age must be a numerical value.");
                System.Console.WriteLine("Please enter a numerical value for your name.");
                System.Console.WriteLine("Starting again...");
                goto userbeginning;
                
            }
            int newUserAge = Int32.Parse(newUserAgeString);

            System.Console.WriteLine("Please type your email:");
            string newUserEmail = Console.ReadLine();

            System.Console.WriteLine("Finally, type a password:");
            string newUserPass = Console.ReadLine();
            
            
            try{
            User newUser = new User (newUserName, newUserAge, newUserEmail, newUserPass);
            MenuFactory.currentUser = _bl.AddUser(newUser);
            }
            catch(EmailVerificationException){
                Log.Information("Caught an EmailVerificationException.");
                System.Console.WriteLine("Emails must have an \"@\" sign in them.");
                System.Console.WriteLine("Please try starting again.");
                goto userbeginning;
            }
            catch(InvalidUserNameException){
                Log.Information("Caught an InvalidUserNameException.");
                System.Console.WriteLine("User name can only have alphabetic characters and spaces.");
                System.Console.WriteLine("Please try starting again.");
                goto userbeginning;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            Log.Information("Created a new user!");
            MenuFactory.GetMenu("order").Start();

        }
    }
}