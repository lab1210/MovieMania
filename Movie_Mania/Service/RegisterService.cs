using System;
using System.Collections.Generic;
using System.Linq;
using Movie_Mania.Models;
using Movie_Mania.IService;

namespace Movie_Mania.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly MovieContext _movieContext;
        public RegisterService()
        {
            _movieContext = new MovieContext();
        }
        public Registers AddUser(Registers register)
        {

            if (_movieContext.registers.Any(r => r.UserName == register.UserName) || _movieContext.registers.Any(r => r.Email == register.Email))
            {
                throw new Exception("User already exists");
            }


            _movieContext.registers.Add(register);
            _movieContext.SaveChanges();

            return register;
        }

        public IEnumerable<Registers> AdminList()
        {
            var admins = _movieContext.registers
                .Where(c => c.Role == "Admin");

            return admins.ToList();
        }

    }
}