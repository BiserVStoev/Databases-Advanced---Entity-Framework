using BookShopSystem.Models;

namespace BookShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BookShopContext>());
        }

        public IDbSet<Author> Authors { get; set; }
        
        public IDbSet<Category> Categories { get; set; }
        
        public IDbSet<Book> Books { get; set; } 
    }
    
}