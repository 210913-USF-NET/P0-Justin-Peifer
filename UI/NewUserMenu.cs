using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBL;
using Models;

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

            System.Console.WriteLine("Please type your name:");
            string newUserName = Console.ReadLine();

            System.Console.WriteLine("Please enter your age:");
            int newUserAge = Int32.Parse(Console.ReadLine());

            System.Console.WriteLine("Please type your email:");
            string newUserEmail = Console.ReadLine();

            System.Console.WriteLine("Finally, type a password:");
            string newUserPass = Console.ReadLine();
            //Testing password masking here
            // char[] psw = new char[100];
            // ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();
            // for (int i = 0; i < psw.Length; i++)
            // {
            //     keyinfo = Console.ReadKey(true);
            //     if (!keyinfo.Key.Equals(ConsoleKey.Enter))
            //     {
            //         psw[ i ] = keyinfo.KeyChar;
            //         Console.Write("*");
            //     }
            //     else
            //     {
            //         break;
            //     }
            // }
            // string newUserPass = new string(psw);
            User newUser = new User (newUserName, newUserAge, newUserEmail, newUserPass);
            _bl.AddUser(newUser);
            
        }
    }
}