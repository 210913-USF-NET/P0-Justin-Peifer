using System;
using System.Collections.Generic;
namespace Models
{
    public class User
    {
        public User(){}

        public User(string name) {
            this.Name = name;
        }

        public string Name {get; set;} //property, belongs in a type

        //constructor chaining
        public User(string name, int age) :this(name)
        {
            this.Age = age;
        }

        public User(string name, int age, string email, string password, string access) :this(name)
        {
            this.Age = age;
            this.Email = email;
            this.Password = password;
            this.Access = access;
        }

        public int Age{get; set;}

        public string Email{get; set;}// this will be a unique identifier for the user

        public string Password{get; set;}

        public string Access{get;set;}//is this a customer, manager, or VIP?

        // public List<Order> Orders 
        // {
        //     get; set;
        // }
    }
}
