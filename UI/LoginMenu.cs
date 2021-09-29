using System;
using Models;
using SBL;
using DL;
using Serilog;
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
            User loginUser = null;
            System.Console.WriteLine("Email:");
            string loginEmail = System.Console.ReadLine();
            
            foreach (User user in allUsers)
            {
                if (user.Email == loginEmail){
                    loginUser = user;
                    System.Console.WriteLine($"Welcome, {loginUser.Name}!");
                    break;
                }
            }
            if (loginUser == null){
                System.Console.WriteLine("User not found. Press [1] to try again, or press [2] to create a new account.");
                    
                    switch (System.Console.ReadLine())
                {
                    case "1":
                        goto loginstart;
                        
                    case "2":
                        new StartMenu().Start();
                        break;
                    default:
                        System.Console.WriteLine("Invalid input, please try again.");
                        goto loginstart;
                }
                
            }
            else{

                passwordinput:
                System.Console.WriteLine("Password:");
                string loginPassword = System.Console.ReadLine();
                if (loginUser.Password == loginPassword){
                    MenuFactory.currentUser = loginUser;
                    Log.Information($"User number {loginUser.Id} ({loginUser.Name}) successfully logged in.");
                    if (loginUser.Access== true){
                        MenuFactory.GetMenu("manager").Start();
                    }

                    else{
                        System.Console.WriteLine("Would you like to start an order or view your order history?");
                        System.Console.WriteLine("[0] New Order\n[1] View completed orders.");
                        string input = System.Console.ReadLine();
                        if (input == "0")
                        {
                        MenuFactory.GetMenu("order").Start();
                        }
                        if (input == "1")
                        {
                        MenuFactory.GetMenu("vieworders").Start();
                        }
                    }
                }
                else{
                    System.Console.WriteLine("Invalid password.");
                    invalidpassword:
                    System.Console.WriteLine("Press [1] to try again, or press [2] to exit to the main menu.");
                    string input = System.Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                goto passwordinput;
                                
                            case "2":
                                break;
                            default:
                                System.Console.WriteLine("Invalid input, please try again.");
                                goto invalidpassword;
                    }

                }
            }
            
        }
        
    }
}