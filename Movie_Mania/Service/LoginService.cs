using System;
using System.Linq;
using System.Web;
using Movie_Mania.Models;
using Movie_Mania.IService;

namespace Movie_Mania.Service
{
    public class LoginService : ILoginService
    {
        private readonly MovieContext movieContext;
        public LoginService()
        {
            movieContext = new MovieContext();
        }
        public Logins CheckUser(Logins login)
        {
            var registerduser = movieContext.registers.FirstOrDefault(r => r.Email == login.Email && r.Password == login.Password);
            if (registerduser == null)
            {
                throw new Exception("Invalid Email or Password");
            }
            LoginMemo loginMemo = new LoginMemo
            {
                Name = registerduser.UserName,
                Role = registerduser.Role,
            };
            StoreLoginName(loginMemo.Name);
            StoreRole(loginMemo.Role);  

            login.Role = registerduser.Role;

            movieContext.logins.Add(login);
            movieContext.SaveChanges();
            return login;
        }

        public void StoreLoginName(string name)
        {
            HttpContext.Current.Session["LoggedInUserName"] = name;
            


        }
        public void StoreRole(string role)
        {
            HttpContext.Current.Session["UserRole"] = role;
        }
        public void clearLoginName()
        {
            StoreLoginName(null);
            StoreRole(null);
        }
    }
}