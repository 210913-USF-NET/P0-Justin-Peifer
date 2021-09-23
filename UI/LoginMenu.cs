using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;

namespace UI
{
    public class LoginMenu : IMenu
    {
        
        private UBL _bl;

        public LoginMenu(UBL bl)
        {
            _bl = bl;
        }

        public void Start(){
            

            loginstart:
            System.Console.WriteLine("Email:");
            string loginEmail = System.Console.ReadLine();
            System.Console.WriteLine("Password:");
            string loginPassword = System.Console.ReadLine();
            List<User> allUsers = _bl.GetAllUsers();
            if(allUsers.Count == 0)
            {
                Console.WriteLine("There are no accounts registered yet, please create a new account.");
            }
            }
        }
}