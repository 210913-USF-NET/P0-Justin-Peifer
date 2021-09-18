using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        //default empty constructor
        public StoreFront() {}

        //constructor overloading
        public StoreFront(string name)
        {
            this.Name = name;
        }

        //Constructor chaining
        public StoreFront(string name, string city, string state) : this(name)
        {
            this.City = city;
            this.State = state;
        }

        //this is type member
        //this is an example of property
        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        // //this is field
        // private string _name;

        // //this is a wrapper for the field above
        // public string GetName()
        // {
        //     return _name;
        // }

        // public void SetName(string value)
        // {
        //     _name = value;
        // }

        public override string ToString()
        {
            return $"Name: {this.Name}, City: {this.City}, State: {this.State}";
        }
    }
}