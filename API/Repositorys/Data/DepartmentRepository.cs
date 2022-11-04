using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace API.Repository.Data
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private readonly MyContext _context;

        public DepartmentRepository(MyContext context)
        {
            _context = context;
        }
        public int Create(Department Entity)
        {
            _context.Departments.Add(Entity);
            var result = _context.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var data = _context.Departments.Find(id);
            if (data != null)
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;

            }
            return 0;
        }

        public IEnumerable<Department> Get()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments.Find(id);
        }

        public int Update(Department Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }
    }
}
