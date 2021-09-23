using System;
using System.Collections.Generic;
namespace Models
{
    public class User
    {
        public User(){}

        public User(string name, int age, string email, string password)
        {
            this.Age = age;
            this.Email = email;
            this.Password = password;
            this.Access = false;
        }

        public int Age{get; set;}

        public string Email{get; set;}// this will be a unique identifier for the user

        public string Password{get; set;}

        public bool Access{get;set;}//is this a customer, manager, or VIP?

        // public List<Order> Orders 
        // {
        //     get; set;
        // }
    }
}
