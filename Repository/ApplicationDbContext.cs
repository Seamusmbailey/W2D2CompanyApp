using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyApp.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public ApplicationDbContext(){}

    public DbSet<Department> Departments {get;set;}
    public DbSet<Employee> Employees {get;set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            // Using the Builder Design Pattern
            // Explanation of this is in the Trainer code github.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                                        .AddJsonFile("appsettings.json")
                                                                        .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // LINQ
        // Line Integrated Query
        // Bascially Lambda Functions

        /*
            This is what is helping the AppDbContext know how to create the entities themselves
            This ensures the relationship between both entities are clear

            For this example, you o nly need to choose one entity instead of both as the modelbuilder on creation will explain the relationship of both entities
        */

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentID);

        // We can do this with both as seen below, but we only need one of them.
        /*
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentID);
        */
    }
}