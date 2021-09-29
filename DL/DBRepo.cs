using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DL
{
    public class DBRepo : IRepo
    {
        private Entity.PeiferP0Context _context;

        public DBRepo(Entity.PeiferP0Context context){
            _context = context;
        }

        /// <summary>
        /// Starts a new order for the customer by grabbing a new Order number from the database
        /// </summary>
        /// <param name="UserId"> userID to be linked to the newly created order</param>
        /// <returns>An empty order that has the new OrderId with the UserId contained as well</returns>
                public Model.Order NewOrder(int UserId)
        {
            Entity.Order orderAdded = new Entity.Order(){
                UserId = UserId,
                DateOrdered = DateTime.Now,
            };
            
            orderAdded = _context.Add(orderAdded).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Model.Order()
            {
                Id = orderAdded.Id,
                DateOrdered = orderAdded.DateOrdered,
                UserId = orderAdded.UserId
            };
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="user">The collected info from the NewUserMenu</param>
        /// <returns>returns an updated version of the submitted User, complete with UserId given by the database.</returns>
        public Model.User AddUser(Model.User user)
        {
            Entity.User userToAdd = new Entity.User(){
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ManagerAccess = user.Access,
                Age = user.Age
            };
            
            userToAdd = _context.Add(userToAdd).Entity;

            //the "changes" don't get executed until you call the SaveChanges method
            _context.SaveChanges();

            //this clears the changetracker so it returns to a clean slate
            _context.ChangeTracker.Clear();

            return new Model.User()
            {
                Id = userToAdd.Id,
                Name = userToAdd.Name,
                Password = userToAdd.Password,
                Age = userToAdd.Age,
                Access = userToAdd.ManagerAccess,
                Email = userToAdd.Email
            };
        }


        /// <summary>
        /// Gets a list of all users stored in the database
        /// </summary>
        /// <returns>List of all Users.</returns>
        public List<Model.User> GetAllUsers(){

            return _context.Users.AsNoTracking().Select(
                User => new Model.User() {
                    Id = User.Id,
                    Email = User.Email,
                    Password = User.Password,
                    Name = User.Name,
                    Age = User.Age,
                    Access = User.ManagerAccess
                    }
            ).ToList();
        }

        /// <summary>
        /// Searchest for a user based on their name
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A list of all users that fit under the search</returns>
        public List<Model.User> SearchUser(string search)
        {
            return _context.Users.Where(
                user => user.Name.Contains(search)
            ).AsNoTracking().Select(
                u => new Model.User() {
                    Id = u.Id,
                    Email = u.Email,
                    Password = u.Password,
                    Name = u.Name,
                    Age = u.Age,
                    Access = u.ManagerAccess
                    }
            ).ToList();
        }

        /// <summary>
        /// Changes a customer account into a manager account
        ///  by changing the ".ManagerAccess" boolean to "True"
        /// </summary>
        /// <param name="newManager">the user to change from customer to manager</param>
        /// <returns>The updated user object</returns>
        public Model.User MakeUserManager(Model.User newManager)
        {

            Entities.User userToChange = (from u in _context.Users
                    where u.Id == newManager.Id select u).SingleOrDefault();
            userToChange.ManagerAccess = true;

            userToChange = _context.Users.Update(userToChange).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Model.User() {
                Id = userToChange.Id,
                Email = userToChange.Email,
                Password = userToChange.Password,
                Name = userToChange.Name,
                Age = userToChange.Age,
                Access = userToChange.ManagerAccess
            };
        }
        
        /// <summary>
        /// Gets more detailed order information, including LineItems, based on OrderId
        /// </summary>
        /// <param name="id">The OrderId for the order we are searching</param>
        /// <returns>The Order's information</returns>
        public Model.Order OrderInfoById(int id){
            Entity.Order orderById = 
                _context.Orders
                .Include(l => l.LineItems)
                .FirstOrDefault(l => l.Id == id);

            return new Model.Order() {
                    Id = orderById.Id,
                    DateOrdered = orderById.DateOrdered,
                    UserId = orderById.UserId,
                LineItems = orderById.LineItems.Select(l => new Model.LineItem(){
                    OrderId = l.OrderId,
                    StoreId = l.StoreId,
                    Quantity = l.Quantity
                    
                }).ToList()
            };
        }

        /// <summary>
        /// Gets the order history of a user based on his/her UserId
        /// </summary>
        /// <param name="UserId">The User's ID</param>
        /// <returns>A list of every order that the User has made</returns>
        public List<Model.Order> OrderByUserId(int UserId){
            return _context.Orders.Where(
                order => order.UserId.Equals(UserId)
            ).AsNoTracking().Select(
                Order => new Model.Order() {
                    Id = Order.Id,
                    DateOrdered = Order.DateOrdered,
                    UserId = Order.UserId}
            ).ToList();
        }


        /// <summary>
        /// Gets a list of every order in the database
        /// </summary>
        /// <returns>The list of every order</returns>
        public List<Model.Order> GetAllOrders(){
            return _context.Orders.AsNoTracking().Select(
                Order => new Model.Order() {
                    Id = Order.Id,
                    DateOrdered = Order.DateOrdered,
                    UserId = Order.UserId
                    }
            ).ToList();
        }

        // public void ClearBadOrder(int orderId){
        // var order = _context.Orders.Where(order => order.UserId.Equals(orderId)).FirstOrDefault();
        //     _context.Orders.DeleteObject(order);
        // }
        

        /// <summary>
        /// Updates the order that was opened at the start of the OrderMenu to add bought LineItems
        /// </summary>
        /// <param name="storeOrderedFrom"> The Store that the order was placed at</param>
        /// <param name="order">The Order, should include LineItems</param>
        /// <returns>Returns the placed order</returns>
        public Model.Order PlaceOrder(Model.StoreFront storeOrderedFrom, Model.Order order){

            foreach (Model.LineItem item in order.LineItems){
                Entity.LineItem addedItem = new Entity.LineItem() {
                    OrderId = order.Id,
                    StoreId = storeOrderedFrom.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                addedItem = _context.Add(addedItem).Entity;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }
            return order;
        }

        /// <summary>
        /// Updates a store's inventory quantity based off of attributes used for placing an order
        /// </summary>
        /// <param name="storeToUpdate">The store that needs to be updated</param>
        /// <param name="orderedProduct"> A list of LineItems that were bought. This needs to be subtracted from the store's inventory</param>
        public void UpdateStock(Model.StoreFront storeToUpdate, List<Model.LineItem> orderedProduct){
            foreach (Model.LineItem item in orderedProduct){
            Entities.Inventory updatedInventory = (from i in _context.Inventories where i.ProductId == item.ProductId && i.StoreId == storeToUpdate.Id
            select i).SingleOrDefault();

            updatedInventory.Quantity = updatedInventory.Quantity - item.Quantity;

            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryToUpdate"></param>
        /// <param name="amountToAdd"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public int UpdateStock(Model.Inventory inventoryToUpdate, int amountToAdd, int storeId){
            Entity.Inventory inventoryToEdit = new Entity.Inventory() {
                Id = inventoryToUpdate.Id,
                StoreId = storeId,
                ProductId = inventoryToUpdate.ProductId,
                Quantity = inventoryToUpdate.Quantity+amountToAdd
                                
            };

            

            inventoryToEdit = _context.Inventories.Update(inventoryToEdit).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return Convert.ToInt32(inventoryToEdit.Quantity);

        }

        public List<Model.StoreFront> GetAllStoreFronts(){

            //same as select * from StoreFront in sql query
            return _context.StoreFronts.AsNoTracking().Select(
                Storefront => new Model.StoreFront() {
                    Id = Storefront.Id,
                    State = Storefront.State,
                    Zipcode = Storefront.ZipCode
                    }
            ).ToList();
            
        }

        public Model.StoreFront StoreById(int id)
        {
            Entity.StoreFront storeById = 
                _context.StoreFronts
                .AsNoTracking()
                .Include(i => i.Inventories)
                .FirstOrDefault(i => i.Id == id);

            return new Model.StoreFront() {
                    Id = storeById.Id,
                    Zipcode = storeById.ZipCode,
                    State = storeById.State,
                Inventory = storeById.Inventories.Select(i => new Model.Inventory(){
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    
                }).ToList()
            };
        }

        //products
        public List<Model.Product> GetAllProducts(){

                    return _context.Products.AsNoTracking().Select(
                        Product => new Model.Product() {
                            Id = Product.Id,
                            Name = Product.Name,
                            Description = Product.Description,
                            Price = Product.Price,
                            Alcohol = Product.Alcohol
                            }
                    ).ToList();
                }
                
        public Model.Product ProductByID(int id)
        {
            Entity.Product productByID = 
                _context.Products
                .AsNoTracking()
                .FirstOrDefault(i => i.Id == id);

            return new Model.Product() {
                    Id = productByID.Id,
                    Name = productByID.Name,
                    Description = productByID.Description,
                    Price = productByID.Price,
                    Alcohol = productByID.Alcohol,
                Inventory = productByID.Inventories.Select(i => new Model.Inventory(){
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    
                }).ToList()
            };
        }

        //inventory

        public List<Model.Inventory> GetAllInventory(){
            //same as select * from Inventory in sql query
            return _context.Inventories.AsNoTracking().Select(
                Inventory => new Model.Inventory() {
                    Id = Inventory.Id,
                    StoreId = Inventory.StoreId,
                    ProductId = Inventory.ProductId,
                    Quantity = Inventory.Quantity
                    }
            ).ToList();
        }
        
        
    }
}