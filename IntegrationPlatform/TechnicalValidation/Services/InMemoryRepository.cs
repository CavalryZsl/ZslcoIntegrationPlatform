using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalValidation.IServices;
using TechnicalValidation.Model;

namespace TechnicalValidation.Services
{
    public class InMemoryRepository : IRepository<Student>
    {
        private  IEnumerable<Student> _students;
        private static Object lockobj=new object();
        public InMemoryRepository()
        {
            _students= new List<Student> {
                new Student{ Id=1,FirstName="Jim",LastName="Green",Birthday=new DateTime(1989,2,23),gender=Gender.女},
                new Student{ Id=2,FirstName="Tom",LastName="Carter",Birthday=new DateTime(1992,4,21),gender=Gender.男},
                new Student{ Id=3,FirstName="Jack",LastName="Green" ,Birthday=new DateTime(1987,2,13),gender=Gender.女},
            };
            
        }

        public Student Add(Student student)
        {
            lock (lockobj)
            {
                var maxid = _students.Max(a => a.Id);
                maxid++;
                student.Id = maxid;
                var query= _students.ToList();
                query.Add(student);
                _students = query;
            }
            return student;
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(a => a.Id == id);

        }
    }
}
