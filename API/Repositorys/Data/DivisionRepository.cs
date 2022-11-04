using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class DivisionRepository : IRepository<Division, int>
    {
        private readonly MyContext _context;

        public DivisionRepository(MyContext context)
        {
            _context = context;
        }
        // GET aLL

        public IEnumerable<Division> Get()
        {
            return _context.Divisions.ToList();
        }

        public Division GetById(int id)
        {
            return _context.Divisions.Find(id);
        }
        /// -- CREATE --
        /// 

        public int Create(Division division)
        {
            _context.Divisions.Add(division);
            var result = _context.SaveChanges();
            return result;
        }
        /// -- UPDATE --
        public int Update(Division division)
        {
            _context.Entry(division).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;

        }

        /// -- DELETE --

        public int Delete(int id)
        {
            var data = _context.Divisions.Find(id);
            if (data != null)
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;

            }
            return 0;
        }


    }
}
