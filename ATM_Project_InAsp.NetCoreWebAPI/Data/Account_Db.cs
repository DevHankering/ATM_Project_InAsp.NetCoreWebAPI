using ATM_Project_InAsp.NetCoreWebAPI.DTO;
using ATM_Project_InAsp.NetCoreWebAPI.Enum;
using ATM_Project_InAsp.NetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATM_Project_InAsp.NetCoreWebAPI.Data
{
    public class Account_Db : DbContext
    {
        public Account_Db(DbContextOptions<Account_Db> options) : base(options) 
        {
                
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<BalanceModel> Balance { get; set; }
        public DbSet<BalanceSheet> BalanceSheet { get; set; }
        public DbSet<ExcelData> ExcelData { get; set; }
        public DbSet<CsvDataModel> CsvData { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()   // we are doing this to capture local time zone;
            .Property(u => u.CreatedAtLocalTimeZone)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<BalanceSheet>()   // we are doing this to capture local time zone;
           .Property(u => u.UpdatedAtLocalTimeZone)
           .HasColumnType("timestamp without time zone");



            modelBuilder.Entity<UserModel>()
               .HasOne(s => s.Document)
               .WithOne()
               .HasForeignKey<UserModel>(s => s.DocumentId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
               .HasOne(s => s.Balance)
               .WithOne()
               .HasForeignKey<UserModel>(s => s.BalanceId)  
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BalanceSheet>()
                .HasOne(s => s.User)
                .WithMany(b => b.BalanceSheet)
                .HasForeignKey(a => a.Customer_ID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BalanceDto>()
                .Property(b => b.CD)
                .HasConversion<string>();

        }
    }
}

