using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        //default empty constructor
        public StoreFront() {}

        //Constructor chaining
        public StoreFront(string state, int zipcode)
        {
            this.Zipcode = zipcode;
            this.State = state;
            
        }

        //this is type member
        //this is an example of property
        public string State { get; set; }

        public int? Zipcode { get; set; }
        public int Id { get; set; }

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
            return $"State: {this.State} Zipcode: {this.Zipcode} ID Number :{this.Id}";
        }
    }
}