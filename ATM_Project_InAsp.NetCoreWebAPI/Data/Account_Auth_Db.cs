using ATM_Project_InAsp.NetCoreWebAPI.RTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ATM_Project_InAsp.NetCoreWebAPI.Data
{
    public class Account_Auth_Db : IdentityDbContext
    {
        public Account_Auth_Db(DbContextOptions<Account_Auth_Db> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleId = "6b9c63d2-bfed-4c58-9eda-33f45da21c86";
            var UserRoleId = "deeb5371-aa85-4e22-bfbc-9ca3002174ae";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                },
                new IdentityRole
                {
                    Id = UserRoleId,
                    ConcurrencyStamp = UserRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };


            builder.Entity<IdentityRole>().HasData(roles);


            //var adminUser = new List<UserResponseRto>
            //{
            //    new UserResponseRto
            //    {
            //        Id = Guid.Parse("ede13ae9-4956-4414-b914-557f24f83eac"),
            //        PinNo = "0346",
            //        Name = "Rahul",
            //        Roles = ["Admin"],
            //        AdhaarCardUrl = "...",
            //        PanCardUrl = "...",

            //    }
            //};

            //builder.Entity<UserResponseRto>().HasData(adminUser);
        }
    }
}
