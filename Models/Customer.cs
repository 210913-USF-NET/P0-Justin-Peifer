using System;

namespace Models
{
    public class Customer
    {
        public Customer(){}

        public Customer(string name) {
            this.Name = name;
        }

        public string Name {get; set;} //property, belongs in a type

        //constructor chaining
        public Customer(string name, int age) :this(name)
        {
            this.Age = age
        }

        public int Age{get; set;}
    }
}
