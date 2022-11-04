using API.Context;
using API.Handlers;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Repository.Data
{
    public class AccountRepository
    {
        private MyContext _context;

        public AccountRepository(MyContext context)
        {
            _context = context;
        }

        public ResponseLogin Login(string Email, string Password)
        {

            var data = _context.Users
                  .Include(x => x.Employee)
                  .Include(x => x.Role)
                  .SingleOrDefault(x => x.Employee.Email.Equals(Email));
            if (data != null)
            {
                
                if (Hashing.ValidatePassword(Password, data.Password))
                {
                    ResponseLogin responseLogin = new ResponseLogin()
                    {
                        FullName = data.Employee.FullName,
                        Email = data.Employee.Email,
                        Role = data.Role.Name


                    };
                    return responseLogin;
                }
                return null;
               
            }
            return null;
        }





        public int Register(string FullName, string Email, DateTime BirthDate, string Password)
        {
            var data = _context.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(Email));
            if (data == null)
            {
                Employee employee = new Employee()
                {
                    Email = Email,
                    FullName = FullName,
                    BirthDate = BirthDate
                };
                _context.Employees.Add(employee);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    var id = _context.Employees.SingleOrDefault(x => x.Email.Equals(Email)).Id;
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(Password),
                        RoleId = 2
                    };
                    _context.Users.Add(user);
                    var resultUser = _context.SaveChanges();
                    if (resultUser > 0)
                    {
                        return resultUser;
                    }
                    return result;
                }
                return 0;
            }
            return 0;
        }



        public int ChangePassword(string Email,  string Password, string Passnew)
        {
            var data = _context.Users
               .Include(x => x.Employee)
               .SingleOrDefault(x => x.Employee.Email.Equals(Email));
            if (data != null)
            {

                if (Hashing.ValidatePassword(Password,data.Password))
                {
                    data.Password = Hashing.HashPassword(Passnew);
                    _context.Entry(data).State = EntityState.Modified;
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return result;
                    }
                    return 0;
                }
                return 0;
            }

            return 0;
        }
        public int ForgotPassword(string Fullname, string Email, string NewPass)
        {

            var data = _context.Users
            .Include(x => x.Employee)

            .SingleOrDefault(x => x.Employee.Email.Equals(Email) && x.Employee.FullName.Equals(Fullname));

            if (data != null)
            {

                data.Password = Hashing.HashPassword(NewPass);

                _context.Entry(data).State = EntityState.Modified;

                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return result;
                }
                return 0;
            }
            return 0;
        }
    }
}
        
        
    

