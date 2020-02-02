using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Extensions;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class DataInitialiser
    {
        public DataContext DataContext { get; set; }
        public UserManager<User> UserManager { get; set; }
        public IConfiguration Configuration { get; set; }

        public Task InitialiseData(IServiceProvider serviceProvider)
        {
            UserManager = serviceProvider.GetService(typeof(UserManager<User>)) as UserManager<User>;
            DataContext = serviceProvider.GetService(typeof(DataContext)) as DataContext;
            Configuration = serviceProvider.GetService(typeof(IConfiguration)) as IConfiguration;

            // TODO: Remove
            Console.WriteLine(DataContext.Database.GetDbConnection().ConnectionString);
            
            SeedUsers();
            //SeedSystemSettings();
            SeedSystemSettings();
            return Task.CompletedTask;
        }

        private void SeedSystemSettings()
        {
            var systemSettings = new SystemSettings
            {
                EmailFromAddress = "noreply@ecomsoftware.com",
                CompanyName = "Ecom",
                ProductName = "Ecom Framework Core"
            };

            DataContext.AddOrUpdate(systemSettings);
            DataContext.SaveChanges();
        }

        //private void SeedSystemSettings()
        //{
        //    var settings = new List<Setting>
        //    {
        //        new Setting("EmailFromAddress", "noreply@ecomsoftware.com", "Emails sent from system will be sent from this address."),
        //        new Setting("CompanyName", "Ecom", "Name of the organisation"),
        //        new Setting("ProductName", "Ecom Framework v2", "Name of the web application"),
        //    };

        //    foreach (var setting in settings)
        //    {
        //        DataContext.AddOrUpdate(setting);
        //    }
        //}

        private void SeedUsers()
        {
            var password = Configuration["DefaultPassword"];
            CreateNewUser("ecomadmin", password, "Ecom", "SuperAdmin", "ecomadmin@ecomsoftware.com", isEcomAccount: true);
            CreateNewUser("admin", password, "Ecom", "Admin", "admin@ecomsoftware.com");
        }

        private void CreateNewUser(string username, string password, string forename, string surname,
            string email = null, bool isEcomAccount = false)
        {
            if (DataContext.Users.FirstOrDefault(x => x.UserName == username) != null) return;

            var user = new User
            {
                UserName = username,
                Email = email,
                Forenames = forename,
                Surname = surname,
                CreatedDate = DateTime.Now,
                PasswordValidToDate = isEcomAccount ? DateTime.Today.AddYears(1) : DateTime.Now.AddDays(30),
                IsEcomAccount = isEcomAccount
            };

            var result = UserManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                // Log success
            }

        }
    }
}