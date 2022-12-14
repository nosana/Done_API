using API.Repository.Data;
using API.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository _accountRepository;
        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        public ActionResult Register(string FullName, string Email, DateTime BirthDate, string Password)
        {
            try
            {
                var result = _accountRepository.Register(FullName, Email, BirthDate, Password);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Register Tidak Berhasil"

                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Register Berhasil"
                    });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpPut("ChangePassword")]

        public ActionResult ChangePassword(string Email,string Password, string Passnew )
        {
            try
            {
                var result = _accountRepository.ChangePassword(Email , Password, Passnew);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Ganti password tidak berhasil"

                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Ganti Password Baru Sudah Berhasil"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut ("ForgotPassword")]
        public ActionResult ForgotPassword(string Fullname, string Email, string NewPass)
        {
            try
            {
                var result = _accountRepository.ForgotPassword(Fullname, Email, NewPass);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode =200,
                        Message = "lupa password gagal"
                    });
                   
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Password lama telah dii ganti"
                    });
                    }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpPost("Login")]
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                var result = _accountRepository.Login(Email,Password);
                if (result == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login Tidak Berhasil"

                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login Berhasil",
                        

                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }


    }
}
