using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.Model;

namespace TechnicalValidation.IServices
{
    public interface IRepository<T> where T : class,new()
    {
         IEnumerable<T> GetAll();
         T GetById(int id);
         T Add(T t);
    }
}
