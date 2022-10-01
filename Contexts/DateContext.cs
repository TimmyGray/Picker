using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Picker.Classes;

namespace Picker.Contexts
{
    internal class DateContext:DbContext
    {
        public DbSet<Date> Dates { get; set; }

        public DateContext(DbContextOptions<DateContext> options):base(options)
        {
            Database.EnsureCreated();
        }

    }
}
