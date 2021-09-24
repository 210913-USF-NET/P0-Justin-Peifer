using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Models
{
    public class User
    {
        public User(){}

        public User(string name, int age, string email, string password)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
            this.Password = password;
            this.Access = false;
        }

        public int Id{get; set;}

        public int? Age{get; set;}
        
        public string Name{get; set;}

        public string Email{get; set;}// this will be a unique identifier for the user, but I'm using an Id for an identifier with less personal info

        public string Password{get; set;}

        public bool? Access{get;set;}//is this a customer, manager, or VIP?

        // public List<Order> Orders 
        // {
        //     get; set;
        // }
    }
}
