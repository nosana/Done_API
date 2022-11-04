using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.ViewModel;
using WebApp.Models;
using WebApp.Handlers;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //-- Login --//
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var data = myContext.Users
                  .Include(x => x.Employee)
                  .Include(x => x.Role)
                  .SingleOrDefault(x => x.Employee.Email.Equals(email));
            //&& x.Password.Equals(Hashing.ValidatePassword(password))
            if (data != null)
            {
                var result = Hashing.ValidatePassword(password, data.Password);
                if (result)
                

                    HttpContext.Session.SetInt32("Id", data.Id);
                    HttpContext.Session.SetString("FullName", data.Employee.FullName);
                    HttpContext.Session.SetString("Email", data.Employee.Email);
                    HttpContext.Session.SetString("Role", data.Role.Name);

                    return RedirectToAction("Index", "Home");
                
              
                /*User user =w Use ner()
                {
                    Password = data.Password
                };*/
                
             
                ///respone login di tambah di view home @model dan controller home

            }
            return View();

        }

        ///-- Register --///
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, string email, DateTime birthDate, string password)
        {
            /// jika memakai where
          /*  /// jika emloyee di email sama dengan email nya 0 masuk ke retrun view 
            if (myContext.Employees.Where(e => e.Email == email).Count()>0)
            {
                return View();
            }*/

            //// untuk banyak 
            if (myContext.Employees.Any(e => e.Email == email))
            {
                return View();
            }

            {
                
                Employee employee = new Employee()
                {
                    FullName = fullname,
                    Email = email,
                    BirthDate = birthDate

                };
                             
                myContext.Employees.Add(employee);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                   

                    var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                    User user = new User()

                    {
                        Id = id,
                        Password = Hashing.HashPassword(password),
                        RoleId = 2
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)

                        return RedirectToAction("Login", "Account");


                }
                return View();
            }
          
        }

        /// -- Change Password -- ///

        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        /* public IActionResult ChangePassword(string Password,User user)
         {

             var data = myContext.Users
                 .Include(x => x.Employee)
                 .SingleOrDefault(x => x.Employee.Email.Equals(user));

             if (data != null)
             {
                 var data1 = Hashing.ValidatePassword(Password, data.Password);
                 if (data1)
                 {


                     data.Password = Hashing.HashPassword(Password);
                     myContext.Entry(data).State = EntityState.Modified;
                     var result = myContext.SaveChanges();
                     if (result > 0)
                         return RedirectToAction("Login");

                 }

             }

             return View();
         }*/
        public IActionResult ChangePassword(string PasswordLama,User user)
        {

            var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(PasswordLama));

            if (data != null)
            {


                if (data.Password == PasswordLama)
                {
                    data.Password = Hashing.HashPassword(PasswordLama);
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return RedirectToAction("Login");
                }

                    

                

            }

            return View();
        }

        /// -- Forgot Passsword --///

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ForgotPassword(string FullName, string Email, string NewPass)
        {
            // from join
             var data = myContext.Users
             .Include(x => x.Employee)
            /// select
             .SingleOrDefault(x => x.Employee.Email.Equals(Email) && x.Employee.FullName.Equals(FullName));
            // jika data tidak sama dengan null maka masuk ke step slanjutnya  
            if (data != null)
              {
                
                data.Password = Hashing.HashPassword(NewPass);
                // data di modif jika di edit
                myContext.Entry(data).State = EntityState.Modified;
                // data di save di database
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Login", "Account");
                }
           }


              return View();

            /*public IActionResult ForgotPassword(string FullName, string Email)
            {
                // mencari email seusai id
                var dataId = myContext.Employees.SingleOrDefault(x => x.Email.Equals(Email)).Id;
                var data = myContext.Employees.Find(dataId);

                if ((data.FullName == FullName))
                {
                    var users = myContext.Users.Find(dataId);
                    return View(users);
                }
                return View();
*/

            }

        /// --- New Password --

        public IActionResult NewPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPass(string passBaru, Employee employee, User user)
        {
            var data = myContext.Users.Find(employee.Email);
            if (data != null)
            {
                data.Password = user.Password;
                
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
    

}

