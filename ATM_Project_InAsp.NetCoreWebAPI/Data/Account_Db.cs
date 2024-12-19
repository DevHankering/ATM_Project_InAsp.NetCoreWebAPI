using ATM_Project_InAsp.NetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM_Project_InAsp.NetCoreWebAPI.Data
{
    public class Account_Db : DbContext
    {
        public Account_Db(DbContextOptions<Account_Db> options) : base(options) 
        {
                
        }

        public DbSet<UserDomainModel> Users { get; set; }
        public DbSet<DocumentDomainModel> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDomainModel>()   // we are doing this to capture local time zone;
            .Property(u => u.CreatedAtLocalTimeZone)
            .HasColumnType("timestamp without time zone");




            modelBuilder.Entity<UserDomainModel>()
                .HasOne(s => s.Documents)
                .WithOne()
                .HasForeignKey<UserDomainModel>(s => s.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
