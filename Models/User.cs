using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Serilog;

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
        
        private string _name;
        public string Name{
            get
            {
                return _name;
            }
            set
            {
                Regex pattern = new Regex ("^[A-Za-z ]+$");
                if(value.Length == 0)
                {
                    InvalidUserNameException e = new InvalidUserNameException("User name can't be empty");
                    Log.Warning(e.Message);
                    throw e;
                }
                else if(!pattern.IsMatch(value))
                {
                    throw new InvalidUserNameException("User name can only have alphanumeric characters and spaces");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private string _email;

        public string Email
                {
                    get
                    {
                        return _email;
                    } 
                    set
                    {
                        if (!value.Contains("@") ||value.StartsWith('@') || value.EndsWith('@'))
                        {
                            throw new EmailVerificationException("Emails must have an \"@\" sign, and cannot start or end with it.");
                        }
                        else{
                            _email = value;
                        }
                    }
                }
        // this will be a unique identifier for the user, but I'm using an Id for an identifier with less personal info

        public string Password{get; set;}

        public bool? Access{get;set;}//is this a customer, manager, or VIP?

        // public List<Order> Orders 
        // {
        //     get; set;
        // }
    }
}
