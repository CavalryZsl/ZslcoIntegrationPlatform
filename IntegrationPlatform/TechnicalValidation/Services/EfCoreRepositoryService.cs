using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.Data;
using TechnicalValidation.IServices;
using TechnicalValidation.Model;

namespace TechnicalValidation.Services
{
    public class EfCoreRepositoryService : IRepository<Student>
    {
        private readonly DBContext dBContext;
        public EfCoreRepositoryService(DBContext dbcontext)
        {
            dBContext = dbcontext;
        }
        public Student Add(Student t)
        {
            dBContext.students.Add(t);
            dBContext.SaveChanges();
            return dBContext.students.OrderByDescending(a => a.Id).First();
        }

        public IEnumerable<Student> GetAll()
        {
            return dBContext.students.ToList();
        }

        public Student GetById(int id)
        {
            return dBContext.students.Find(id);
        }
    }
}
