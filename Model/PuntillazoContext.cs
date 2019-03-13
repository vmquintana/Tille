namespace TilleWPF.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PuntillazoContext : DbContext
    {
        public PuntillazoContext()
            : base("name=PuntillazoContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductMov> ProductMovs { get; set; }
        
    }
}
