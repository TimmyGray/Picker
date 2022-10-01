using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Picker.Contexts;

namespace Picker.Classes
{
    internal class ConfigurationDb
    {
        ConfigurationBuilder builder;
        DbContextOptionsBuilder<DateContext> optionsBuilder;
        public DbContextOptions<DateContext> options;
        public string connectionstring;

        public ConfigurationDb()
        {
            builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var conf = builder.Build();
            connectionstring = conf.GetConnectionString("DefaultConnection");
            optionsBuilder = new DbContextOptionsBuilder<DateContext>();
            options = optionsBuilder
                .UseMySql(connectionstring, new MySqlServerVersion(new Version(5, 5, 39)))
                .Options;

        }

    }
}
