using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;

namespace UI
{
    public class LoginMenu : IMenu
    {
        
        private BL _bl;

        public LoginMenu(BL bl)
        {
            _bl = bl;
        }

        public void Start(){
            
            List<User> allUsers = _bl.GetAllUsers();
            
            if(allUsers.Count == 0)
            {//have this link to an account creation menu
                Console.WriteLine("There are no accounts registered yet, please create a new account.");
            }

            
            loginstart:
            User currentUser = null;
            System.Console.WriteLine("Email:");
            string loginEmail = System.Console.ReadLine();
            
            foreach (User user in allUsers)
            {
                if (user.Email == loginEmail){
                    currentUser = user;
                    System.Console.WriteLine($"Welcome, {currentUser.Name}!");
                    break;
                }
            }
            if (currentUser == null){
                System.Console.WriteLine("User not found. Press [1] to try again, or press [2] to create a new account.");
                    
                    switch (System.Console.ReadLine())
                {
                    case "1":
                        goto loginstart;
                        break;
                    case "2":
                        MenuFactory.GetMenu("newuser");
                        break;
                    default:
                        System.Console.WriteLine("Invalid input, please try again.");
                        goto loginstart;
                        break;
                }
                
            }
            
            
                // else {
                //     System.Console.WriteLine("User not found. Press [1] to try again, or press [2] to create a new account.");
                    
                //     switch (System.Console.ReadLine();)
                // {
                //     case "1":
                //         goto loginstart;
                //     case "2"
                //         MenuFactory("newuser")
                //     default:
                //         System.Console.WriteLine("Invalid input, please try again.");
                //         goto loginstart;
                // }
                // }

            System.Console.WriteLine("Password:");
            string loginPassword = System.Console.ReadLine();
            if (currentUser.Password == loginPassword){
                MenuFactory.GetMenu("order");
            }
            else {
                System.Console.WriteLine("Invalid password. Press [1] to try again, or press [2] to exit to the main menu.");
            }
            
            }

        }
}