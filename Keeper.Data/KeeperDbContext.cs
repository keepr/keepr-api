using Keeper.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Data
{
    public class KeeperDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public KeeperDbContext(DbContextOptions<KeeperDbContext> options): base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            // Client
            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Client>()
                .HasOne(x => x.User)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.UserId);

            // Contact
            modelBuilder.Entity<Contact>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.ClientId);

            // Project
            modelBuilder.Entity<Project>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Project>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ClientId);

            // Task
            modelBuilder.Entity<ProjectTask>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ProjectTask>()
                .HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
