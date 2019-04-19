using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.Model;

namespace TechnicalValidation.Data
{
    public class DBContext:DbContext//非线程安全的
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {

        }
        public DbSet<Student> students { get; set; }
    }
}
